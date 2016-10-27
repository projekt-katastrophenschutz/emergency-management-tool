using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ProjektKatastrophenschutz.Utils
{
    /// <summary>
    /// A class that helps when dealing will files.
    /// </summary>
    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    internal class FileUtils
    {
        /// <summary>
        /// Gets a file from an open file dialog (The user can choose a file).
        /// </summary>
        /// <param name="initialDirectory">The directory the file dialog should start with</param>
        /// <param name="filter">A file filter, for example "Executable Files|*.exe" or "Text files|*.txt"</param>
        /// <returns>A path to the file, if a file was chosen. Otherwise null.</returns>
        public static string GetFileFromOpenFileDialog(string initialDirectory, string filter = null)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = initialDirectory,
                Filter = filter,
                CheckFileExists = true,
                CheckPathExists = true
            };

            var result = openFileDialog.ShowDialog();
            if (result == false)
                return null;

            return openFileDialog.FileName;
        }

        /// <summary>
        /// Gets a file from an save file dialog. The user can specifiy a file to save data to.
        /// </summary>
        /// <param name="initialDirectory">The directory the file dialog should start with</param>
        /// <param name="defaultFilename">the default file name of the file</param>
        /// <param name="fileExtension">the default file extension of the file</param>
        /// <returns>A path to the file, if a file was chosen. Otherwise null.</returns>
        public static string GetFileFromSaveFileDialog(string initialDirectory, string defaultFilename = null,
            string fileExtension = null)
        {
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory,
                FileName = defaultFilename,
                DefaultExt = fileExtension,
            };

            var result = saveFileDialog.ShowDialog();
            if (result == false)
                return null;

            return saveFileDialog.FileName;
        }

        /// <summary>
        /// Gets all files located at a specific directory and matching the given defaultFilename.
        /// For including subdirectories, pass SearchOption.AllDirectories as searchOption argument.
        /// </summary>
        /// <param name="parentDirectory">The directory to look at</param>
        /// <param name="filename">The file to search for</param>
        /// <param name="searchOption">The Searchoption. Use SearchOption.AllDirectories to include subfolders, too</param>
        /// <returns>An array of the matching files</returns>
        public static string[] FindAllFiles(string parentDirectory, string filename,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var files = Directory.GetFiles(parentDirectory, filename, searchOption);
            return files;
        }

        /// <summary>
        /// Gets the first file matching the arguments.
        /// </summary>
        /// <param name="parentDirectory">The directory to look at</param>
        /// <param name="validFilenames">All valid file names</param>
        /// <param name="searchOption">The Searchoption. Use SearchOption.AllDirectories to include subfolders, too</param>
        /// <returns>The first matching file. Otherwise null.</returns>
        public static async Task<string> FindFirstFile(string parentDirectory, string[] validFilenames,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var filename in validFilenames)
            {
                var files = await Task.Run(() => FindAllFiles(parentDirectory, filename, searchOption));
                if (files.Length > 0)
                    return files[0];
            }
            return null;
        }
    }
}
