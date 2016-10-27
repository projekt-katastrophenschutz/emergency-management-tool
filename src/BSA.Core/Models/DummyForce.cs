using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Device.Location;
using AutoMapper;
using BSA.Core.Utils;
namespace BSA.Core.Models
{
    public class DummyForce : EditableModelBase<DummyForce>
    {
        private int count;
        private string unitname;
        private string organisationname;
        private string radioname;

        public int Count
        {
            get{ return this.count; }
            set { this.Set(ref this.count, value); }
        }
        public string Unitname
        {
            get { return this.unitname; }
            set { this.Set(ref this.unitname, value); }
        }
        public string Organisation
        {
            get { return this.organisationname; }
            set { this.Set(ref this.organisationname, value); }
        }
        public string Radioname
        {
            get { return this.radioname; }
            set { this.Set(ref this.radioname, value); }
        }
    }
}
