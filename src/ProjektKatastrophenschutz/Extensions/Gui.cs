using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjektKatastrophenschutz.Extensions
{
    public static class Gui
    {
        public static void SynchronizedInvoke(this ISynchronizeInvoke sync, Action action)
        {
            // If the invoke is not required, then invoke here and get out.
            if (!sync.InvokeRequired)
            {
                // Execute action.
                action();

                // Get out.
                return;
            }

            // Marshal to the required context.
            sync.Invoke(action, new object[] { });
        }

        public delegate void AppendTextCallback(TextBox control, Window window, string text);

        // Thread safe updating of control's text property
        public static void AppendTextThreadSafe(this TextBox control, Window window, string text)
        {
            if (window.Dispatcher.CheckAccess())
                control.AppendText(text);
            else
            {
                var callback = new AppendTextCallback(AppendTextThreadSafe);
                window.Dispatcher.Invoke(callback, control, window, text);
            }
        }

        public delegate void SetTextCallback(TextBox control, Window window, string text);

        // Thread safe updating of control's text property
        public static void SetTextThreadSafe(this TextBox control, Window window, string text)
        {
            if (window.Dispatcher.CheckAccess())
                control.Text = text;
            else
            {
                var callback = new SetTextCallback(SetTextThreadSafe);
                window.Dispatcher.Invoke(callback, control, window, text);
            }
        }

        private delegate string GetTextCallback();

        public static string GetTextThreadSafe(this TextBox textBox)
        {
            var o = textBox.Dispatcher.Invoke(new GetTextCallback(() => textBox.Text));
            var text = o.ToString();
            return text;
        }

        public static bool CheckedIsTrue(this CheckBox checkBox)
        {
            return checkBox.IsChecked.HasValue && checkBox.IsChecked.Value;
        }
    }
}
