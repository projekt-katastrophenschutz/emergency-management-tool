// <copyright file="Playground.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;

    [TestClass]
    public class Playground
    {
        [TestMethod]
        public void MockupTests()
        {
            ////var einsatzliste = new List<Jobs>();

            //////anlegen neuer Job
            ////var Job = new Jobs();

            ////Job.Name = "Test 2";
            ////Job.Date = new DateTime(2016,5,7,4,20,46);
            ////Job.Messenger = "Heinz Klaus";
            ////Job.Organisation = "Test Organisation 1";
            ////Job.Location = new JobLocation();
            ////Job.Location.Street = "Maxstraße" ;
            ////Job.Location.HouseNumber = 5;
            ////Job.Location.ZipCode = 87210;
            ////Job.Location.FloorLevel = 4;
            ////Job.Location.City = "Augsburg";
            ////Job.Location.AdditionalDescription = "Links neben der Bäckerei";
            ////Job.Type = JobType.Rescue;
            ////Job.InjuredPerson = true;
            ////Job.NumberInjuredPerson = 5;
            ////Job.Communicationtool = "Funk";
            ////Job.Description = "Dies ist ein Beispielfall...";
            ////Job.Priority = JobPriority.High;
            ////Job.Status = JobState.NewCreated;
            ////Job.Forces = new List<Forces>();

            ////var Job2 = new Jobs();

            ////Job2.Name = "Test 2";
            ////Job2.Date = new DateTime(2016, 5, 7, 5, 24, 59);
            ////Job2.Messenger = "Hans Müller";
            ////Job2.Organisation = "Test Organisation 1";
            ////Job2.Location = new JobLocation();
            ////Job2.Location.Street = "Paracelsusstraße";
            ////Job2.Location.HouseNumber = 842;
            ////Job2.Location.ZipCode = 87256;
            ////Job2.Location.FloorLevel = 9;
            ////Job2.Location.City = "München";
            ////Job2.Location.AdditionalDescription = "Rechts neben der Kaserne";
            ////Job2.Type = JobType.Flooding;
            ////Job2.InjuredPerson = true;
            ////Job2.NumberInjuredPerson = 5;
            ////Job2.Communicationtool = "2 Meter Funk";
            ////Job2.Description = "Dies ist ein Beispielfall...... Test 2";
            ////Job2.Priority = JobPriority.Low;
            ////Job2.Status = JobState.NewCreated;
            ////Job2.Forces = new List<Forces>();


            ////// Job in Liste einfügen und sortieren
            ////einsatzliste.Add(Job);
            ////var sortEinsätze = einsatzliste.OrderBy(einsatz => einsatz.Priority);
            ////einsatzliste.Add(Job2);
            ////sortEinsätze = einsatzliste.OrderBy(einsatz => einsatz.Priority);


            ////// anlegen neuer Einsatzkräfte
            ////Forces force1 = new Forces();

            ////force1.Name = "TestForce 1";
            ////force1.RadioName = "TestRadio 1";

            ////// anlegen neuer Personen
            ////Person person1 = new Person();

            ////person1.Prename = "Stefan";
            ////person1.Surname = "bla";
            ////person1.BirthDate = new DateTime(1990, 5, 4);
            ////person1.Street = "Teststraße 1";
            ////person1.ZipCode = 84215;
            ////person1.Place = "Augsburg";

            ////// neue Person als Einsatzkraftührer festlegen und der Liste der Personen Hinzufügen
            ////force1.Leader = person1;
            ////force1.Vehicle = "Feuerwehrauto 1";
            ////force1.Note = "erste Testeinsatzkraft...";
            ////force1.Persons = new List<Person>();
            ////force1.Persons.Add(person1);

            ////Forces force2 = new Forces();

            ////force2.Name = "TestForce 213";
            ////force2.RadioName = "TestRadio 241";

            ////Person person2 = new Person();

            ////person2.Prename = "Stefan gwgwregw";
            ////person2.Surname = "bla gweghw";
            ////person2.BirthDate = new DateTime(2002, 5, 4);
            ////person2.Street = "Teststraße 262352";
            ////person2.ZipCode = 89541;
            ////person2.Place = "Augsburg135";


            ////// neue Person als Einsatzkraftührer festlegen und der Liste der Personen Hinzufügen
            ////force2.Leader = person2;
            ////force2.Vehicle = "Boot test";
            ////force2.Note = "erste Testeinsatzkraft... fewgwgweghwe";
            ////force2.Persons = new List<Person>();
            ////force2.Persons.Add(person2);

            ////Person person3 = new Person();

            ////person3.Prename = "Hans";
            ////person3.Surname = "Tischler";
            ////person3.BirthDate = new DateTime(2000, 4, 7);
            ////person3.Street = "Teststraße 4894526489";
            ////person3.ZipCode = 89540;
            ////person3.Place = "München";

            ////Person person4 = new Person();

            ////person4.Prename = "Josef";
            ////person4.Surname = "Maurer";
            ////person4.BirthDate = new DateTime(1995, 2, 1);
            ////person4.Street = "Teststraße bla bla bla";
            ////person4.ZipCode = 89596;
            ////person4.Place = "Nürnberg";


            ////// Person einer Einsatzkraft zuteilen
            ////force1.Persons.Add(person3);
            ////force2.Persons.Add(person4);
         

            ////// Job Status ändern und Einsatzkraft zuteilen
            ////Job2.Status = JobState.InEdit;
            ////Job2.Forces.Add(force1);
            ////Job2.Forces.Add(force2);

            ////Job.Status = JobState.InEdit;
            ////// test ob Einsatzkraft enthalten ist
            ////Assert.IsTrue(Job2.Forces.Contains(force1));

            ////// test ob Status der beiden Jobs gleich sind 
            ////Assert.IsTrue(Job2.Status.Equals(Job.Status));

            ////// test auf Namensgleichheit bei Aufträgen
            ////Assert.AreEqual(Job.Name, Job2.Name);

            ////// test ob Property gesetzt wurde
            ////Assert.IsTrue(Job.Location.Street == "Maxstraße");

            ////// test ob richtiges Eingangsdatum hinzugefügt wurde
            ////Assert.AreEqual(Job.Date, new DateTime(2016, 5, 7, 4, 20, 46));

            ////// test ob Sortierung funktioniert hat
            ////foreach (var elemente in sortEinsätze)
            ////{
            ////    Console.WriteLine(elemente.Name + " " + elemente.Priority.ToString() + " " + elemente.Date.ToString() );
            ////}

            ////Job2.Status = JobState.Closed;
            ////Job2.Forces.Remove(force1);

            ////// test ob Einsatzkraft noch enthalten ist
            ////Assert.IsFalse(Job2.Forces.Contains(force1));


            ////// test wieviele Personen in der Einsatzkraft enthalten sind
            ////Assert.IsTrue(force2.Persons.Count == 2);
        }
        
    }
}