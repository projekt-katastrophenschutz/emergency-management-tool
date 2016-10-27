using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjektKatastrophenschutz.Utils.Windows
{
    class WinUtils
    {
        /// <summary>
        ///     Flash window, for example to notify that changes were made
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="count"></param>
        /// <param name="timeout"></param>
        public static void FlashWindow(IntPtr hWnd, uint count, uint timeout)
        {
            var fInfo = new WinApi.FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = WinConstants.FLASHW_ALL;
            fInfo.uCount = count;
            fInfo.dwTimeout = timeout;

            WinApi.FlashWindowEx(ref fInfo);
        }

        /// <summary>
        /// Returns a list of child windows (window handles) of a given parent window
        /// </summary>
        /// <param name="parent">The window handle of the parent window</param>
        /// <returns>The list with child window handles</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            var result = new List<IntPtr>();
            var listHandle = GCHandle.Alloc(result);
            try
            {
                var childProc = new WinApi.EnumWindowProc(EnumWindow);
                WinApi.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            return result;
        }

        /// <summary>
        /// Helper for GetChildWindows method of this class.
        /// </summary>
        /// <param name="handle">The window handle of the parent window</param>
        /// <param name="pointer">?</param>
        /// <returns></returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            var gcHandle = GCHandle.FromIntPtr(pointer);
            var list = gcHandle.Target as List<IntPtr>;

            if (list == null)
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");

            list.Add(handle);
            return true;
        }

        /// <summary>
        /// Returns all matching windows with a given window filter.
        /// </summary>
        /// <param name="filter">The window filter</param>
        /// <returns>A generic collection with IntPtr objects (The window handles)</returns>
        private static IEnumerable<IntPtr> FindWindows(WinApi.EnumWindowProc filter)
        {
            var windowHandles = new List<IntPtr>();

            WinApi.EnumWindows(delegate (IntPtr hWnd, IntPtr param)
            {
                if (filter(hWnd, param))
                    windowHandles.Add(hWnd);

                return true;
            }, IntPtr.Zero);

            return windowHandles;
        }

        /// <summary>
        /// Returns all matching windows with a given window title.
        /// </summary>
        /// <param name="windowTitle">The window title</param>
        /// <returns>A generic collection with IntPtr objects (The window handles)</returns>
        public static IEnumerable<IntPtr> GetWindowHandles(string windowTitle)
        {
            return FindWindows
            (
                (hWnd, param) => GetWindowText(hWnd).Contains(windowTitle)
            );
        }

        /// <summary>
        /// Returns the first matching window with a given window title.
        /// You could use "FindWindow" WinApi instead.
        /// <see cref="WinApi.FindWindow"> 
        /// For example: 
        /// var handle = WinApi.FindWindow("Untitled - Notepad");
        /// var theSameHandle = WinUtils.GetFirstWindow("Untitled - Notepad");
        /// </see>
        /// </summary>
        /// <param name="windowTitle">The window title</param>
        /// <returns>The handle to the first matching window (IntPtr.Zero if there are no matches)</returns>
        public static IntPtr GetFirstWindow(string windowTitle)
        {
            var handles = GetWindowHandles(windowTitle);
            var intPtrs = handles as IntPtr[] ?? handles.ToArray();

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!intPtrs.Any())
                return IntPtr.Zero;

            return intPtrs[0];
        }

        /// <summary>
        /// Returns the text of a given main window handle. 
        /// The text can be a window title, the text an editor contains, ...
        /// </summary>
        /// <param name="hWnd">The window handle</param>
        /// <returns>The text of the window</returns>
        public static string GetWindowText(IntPtr hWnd)
        {
            var length = WinApi.GetWindowTextLength(hWnd);
            var stringBuilder = new StringBuilder(length + 1);
            WinApi.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Just a little help function for reading the text of a given window handle
        /// </summary>
        /// <param name="hWNd">The window handle</param>
        /// <returns>The length of the window text</returns>
        private static int GetWindowTextLength(IntPtr hWNd)
        {
            // helper for GetWindowText
            var length = WinApi.SendMessage4(hWNd, WinConstants.WM_GETTEXTLENGTH, 0, 0);
            return length;
        }

        /// <summary>
        /// Returns the text of a given control handle (A window handle)
        /// </summary>
        /// <param name="control">The window handle of the control</param>
        /// <returns>The text of the control</returns>
        public static string GetControlText(IntPtr control)
        {
            var controlTextLength = GetWindowTextLength(control);
            if (controlTextLength <= 0) // no text
                return null;

            var stringBuilder = new StringBuilder(controlTextLength + 1);
            WinApi.SendMessage3(control, WinConstants.WM_GETTEXT, controlTextLength + 1, stringBuilder);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the window from the given point (Except hidden / disabled windows).
        /// </summary>
        /// <param name="point">The point (x, y)</param>
        /// <returns>The handle to the window beneath the point</returns>    
        public static IntPtr LowestLevelWindowFromPoint(WinApi.POINT point)
        {
            var hWnd = WinApi.WindowFromPoint(point);
            //WinApi.ScreenToClient(hWnd, ref cursorPosition);

            var child = WinApi.ChildWindowFromPoint(hWnd, point);
            // Is there any child window?
            if (child != IntPtr.Zero)
                hWnd = child;

            // No more child windows? Just return the current window handle
            if ((WinApi.GetWindowLong(hWnd, (WinConstants.GWL_STYLE))
                 & WinConstants.WS_POPUP) != 0)
            {
                return hWnd;
            }

            hWnd = GetChildMost(point, hWnd);
            return hWnd;
        }

        /// <summary>
        /// Return the window handle to the "lowest" child window of a given window handle.
        /// This is necessary, for example, for group boxes or something. 
        /// WindowFromPoint will just return a handle to the group box, 
        /// instead of returning the handle to one of its child windows (a text box or something).
        /// </summary>
        /// <param name="point">The point (x, y)</param>
        /// <param name="hWnd">The window handle (top window)</param>
        /// <returns>The handle to the child window</returns>
        private static IntPtr GetChildMost(WinApi.POINT point, IntPtr hWnd)
        {
            // Max size of a window
            var dwWndArea = WinConstants.MAXDWORD;
            // size of the parent window
            var hWndParent = WinApi.GetParent(hWnd);
            // first child window handle
            var hWndChild = WinApi.GetWindow(hWndParent, WinConstants.GW_CHILD);

            // Iterate child windows
            while (hWndChild != IntPtr.Zero)
            {
                // ignore hidden windows
                if (WinApi.IsWindowVisible(hWndChild))
                {
                    // window beneath the point
                    WinApi.RECT rect;
                    WinApi.GetWindowRect(hWndChild, out rect);
                    // Is the given point within the rectangle of the current child window?
                    if (WinApi.PtInRect(ref rect, point))
                    {
                        // find smallest
                        var dwChildArea = (uint)(rect.Width * rect.Height);
                        // Is the current child window size smaller than the one of the last child window?
                        if (dwChildArea < dwWndArea)
                        {
                            dwWndArea = dwChildArea;
                            hWnd = hWndChild;
                        }
                    }
                }
                // retrieving next child window handle
                hWndChild = WinApi.GetWindow(hWndChild, WinConstants.GW_HWNDNEXT);
            }

            return hWnd;
        }

        /// <summary>
        /// Gets an image from a given filepath.
        /// </summary>
        /// <param name="filename">The filepath of the image</param>
        /// <returns>A pointer to the image</returns>
        public static IntPtr GetImageFromFile(string filename)
        {
            var cursorBitmap = (Bitmap)Image.FromFile(filename);
            Graphics.FromImage(cursorBitmap);
            return cursorBitmap.GetHicon();
        }

        /// <summary>
        /// Closes a window, using "SendMessage" the API call of windows.
        /// </summary>
        /// <param name="hWnd">The window handle of the window that has to be closes</param>
        public static void CloseWindowViaSendMessage(IntPtr hWnd)
        {
            WinApi.SendMessage(hWnd, (int)WinConstants.WM_SYSCOMMAND, (IntPtr)WinConstants.SC_CLOSE, IntPtr.Zero);
        }

        /// <summary>
        /// Places a keystroke in the message queue of a window. You can't simulate 
        /// keystrokes together with special keys (alt, shift, etc.) with this method.
        /// You have to run this program as administrator. Otherwise you can't send
        /// messages to other programs running as administrator.
        /// </summary>
        /// <param name="hWnd">The window handle the message has to be sent to</param>
        /// <param name="virtualKey">The virtual key code of the key. See: 
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
        /// <see cref="WinConstants.VirtualKey"> 
        /// Use this class when calling this method. 
        /// </see>
        /// </param>
        public static void PostMessageKeyDownAndUp(IntPtr hWnd, uint virtualKey)
        {
            WinApi.PostMessage(hWnd, WinConstants.WM_KEYDOWN, (int)virtualKey, 0);
            WinApi.PostMessage(hWnd, WinConstants.WM_KEYUP, (int)virtualKey, 0);
        }

        /// <summary>
        /// Sends a left click to a given window handle.
        /// You have to run this program as administrator. Otherwise you can't send
        /// messages to other programs running as administrator.
        /// </summary>
        /// <param name="hWnd">The window handle the click has to be sent to.</param>
        public static void SendLeftClick(IntPtr hWnd)
        {
            WinApi.PostMessage(hWnd, WinConstants.BM_CLICK, 0, 0);
        }

        /// <summary>
        /// Sends a right click to a given window handle.
        /// You have to run this program as administrator. Otherwise you can't send
        /// messages to other programs running as administrator.
        /// </summary>
        /// <param name="hWnd">The window handle the click has to be sent to.</param>
        public static void SendRightClick(IntPtr hWnd)
        {
            WinApi.PostMessage(hWnd, WinConstants.WM_KEYDOWN, (int)WinConstants.VirtualKey.VK_RBUTTON, 0);
            WinApi.PostMessage(hWnd, WinConstants.WM_KEYUP, (int)WinConstants.VirtualKey.VK_RBUTTON, 0);
        }
    }
}
