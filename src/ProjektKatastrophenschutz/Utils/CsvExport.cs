using BSA.Core.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjektKatastrophenschutz.Utils
{
    /// <summary>
    ///     <see cref="PrintHelper"/>
    /// </summary>
    [Obsolete("There's a new class for this purpose")]
    public class CsvExport
    {
        static string[] headerPersonArray = { "Nachname", "Vorname", "Geburtsdatum", "Telefonnummer", "Straße", "PLZ", "Stadt", "Infos", "Arbeitgeber", "Zusatzinfo" };
        static string[] headerVehicleArray = { "#Personen", "Name", "Rufname", "Gruppenfuehrer", "Fahrzeug", "Anmerkungen" };
        static string[] headerJobArray = { "Prioriät", "Typ", "Beschreibung","Addresse", "erstellt am" };
        static SaveFileDialog savedialog = new SaveFileDialog();
        static string PrintFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //Fahrzeuge
        public static void csvVehicleWriter(IEnumerable<Force> liste)
        {
            try
            {
                savedialog.ShowDialog();
                var wfile = System.IO.Path.Combine(PrintFolder,savedialog.FileName );
                if (File.Exists(wfile))
                {
                    File.WriteAllText(wfile, String.Empty);
                }
                using (StreamWriter sw = new StreamWriter(wfile, true))
                {
                    //write to the file
                    string strHeader = "";
                    for (int i = 0; i <= headerVehicleArray.Length - 1; i++)
                    {
                        strHeader = strHeader + headerVehicleArray[i].ToString();
                        strHeader += ";";
                    }
                    strHeader = strHeader.Trim(';');
                    sw.WriteLine(strHeader.ToString());
                    foreach (Force vehicle in liste)
                    {
                        string strRowValue = "";
                        strRowValue += vehicle.Persons.Count + ";" + vehicle.Name + ";" + vehicle.RadioName.ToString() + ";" + vehicle.Name + ";" + vehicle.Vehicle + ";" + vehicle.Notes + ";";
                        strRowValue = strRowValue.Trim(';');
                        sw.WriteLine(strRowValue);
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        //Aufträge
        public static void csvJobWriter(IEnumerable<Job> liste)
        {
            try
            {
                savedialog.ShowDialog();
                var wfile = System.IO.Path.Combine(PrintFolder, savedialog.FileName);
                if (File.Exists(wfile))
                { 
                    File.WriteAllText(wfile, String.Empty);
                }
                using (StreamWriter sw = new StreamWriter(wfile, true))
                {

                    //write to the file
                    string strHeader = "";

                    for (int i = 0; i <= headerJobArray.Length - 1; i++)
                    {
                        strHeader = strHeader + headerJobArray[i].ToString();
                        strHeader += ";";
                    }
                    strHeader = strHeader.Trim(';');
                    sw.WriteLine(strHeader.ToString());
                    foreach (var job in liste)
                    {
                        string strRowValue = "";
                        strRowValue += job.JobPriority + ";" + job.JobType + ";" + job.Description + ";" + job.Location + ";" + job.Date + ";";
                        strRowValue = strRowValue.Trim(';');
                        sw.WriteLine(strRowValue);
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }  
        }
        //Personen
        public static void csvPersonWriter(IEnumerable<Person> liste,Force force )
        {
            try
            {
                savedialog.ShowDialog();
                var wfile = System.IO.Path.Combine(PrintFolder, savedialog.FileName);

                if (File.Exists(wfile ))
                    File.WriteAllText(wfile , String.Empty);
                using (StreamWriter sw = new StreamWriter(wfile, true))
                {
                    //write to the file
                    string strHeader = "";
                    for (int i = 0; i <= headerVehicleArray.Length - 1; i++)
                    {
                        strHeader = strHeader + headerVehicleArray[i].ToString();
                        strHeader += ";";
                    }
                    strHeader = strHeader.Trim(';');
                    sw.WriteLine(strHeader.ToString());

                    string strValue = force.Persons.Count + ";" + force.Name + ";" + force.Vehicle + ";" + force.RadioName + ";" + force.Name + ";" + force.Vehicle + ";" + force.Notes + ";";
                    strValue = strValue.Trim(';');
                    sw.WriteLine(strValue);
                    
                    strHeader = "";
                    for (int i = 0; i <= headerPersonArray.Length - 1; i++)
                    {
                        strHeader = strHeader + headerPersonArray[i].ToString();
                        strHeader += ";";
                    }
                    strHeader = strHeader.Trim(';');
                    sw.WriteLine(strHeader.ToString());
                    //todo!!!!!!!!!!!
                    foreach (var person in liste)
                    {
                        string strRowValue = "";
                        strRowValue += person.Surname + ";" + person.Prename + ";" + person.BirthDate + ";" + person.PhoneNumber + ";" + person.Street + ";" + person.ZipCode + ";" + person.Place + ";" + person.RelativesDetails  + ";" + person.Employer + ";" + person.Additional + ";";
                        strRowValue = strRowValue.Trim(';');
                        sw.WriteLine(strRowValue);
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }    
        }
    }
}


