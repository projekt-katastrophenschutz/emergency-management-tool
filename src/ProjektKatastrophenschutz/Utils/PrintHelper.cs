using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BSA.Core.Models;
using BSA.Core.Printing.Csv.Maps;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Owin.Security.Provider;
using static System.Console;

namespace ProjektKatastrophenschutz.Utils
{
    public static class PrintHelper
    {
        /// <summary>
        ///     Saves a collection to a given file, in csv format
        /// </summary>
        /// <typeparam name="TCsvClassMap">
        ///     The csv mapper class type.
        ///     Example: <see cref="ForceCsvMap"/>
        /// </typeparam>
        /// 
        /// <param name="collection">
        ///     Collection that should be saved.
        /// </param>
        /// 
        /// <param name="filepath">
        ///     The path of the csv file.
        /// </param>
        /// 
        /// <param name="delimiter">
        ///     The csv delimiter (usually "," or ";").
        /// </param>
        /// 
        /// <param name="shouldOverwrite">
        ///     If the file should be overwritten or not.
        /// </param>
        public static void SaveToCsv<TCsvClassMap>(IEnumerable collection, string filepath,
            bool shouldOverwrite, string delimiter = ";")
            where TCsvClassMap : CsvClassMap          
        {
            // If no or a corrupted directory path was specified, just return
            var directory = Path.GetDirectoryName(filepath);
            if (directory == null)
                return;

            // Create directory. Has no effect, if the directory already exists.
            Directory.CreateDirectory(directory);

            // If the file exists and we are not allowed to overwrite, just return
            if (File.Exists(filepath) && !shouldOverwrite)
                return;

            try
            {
                // write collection to csv, using the specified delimiter and CsvMapper
                using (var csv = new CsvWriter(new StreamWriter(filepath)))
                {
                    csv.Configuration.Delimiter = delimiter;
                    csv.Configuration.RegisterClassMap<TCsvClassMap>();
                    csv.WriteRecords(collection);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            } 
        }
    }
}
