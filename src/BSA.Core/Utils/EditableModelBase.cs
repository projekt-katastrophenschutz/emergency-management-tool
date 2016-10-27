// <copyright file="EditableModelBase.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using BSA.Core.Models;
    using GalaSoft.MvvmLight.Command;

    public abstract class EditableModelBase<T> : ModelBase, IEditableObject
    {
        private T Cache { get; set; }

        private object CurrentModel => this;

        public RelayCommand CancelEditCommand => new RelayCommand(this.CancelEdit);

        public EditableModelBase(bool isNew)
            : base(isNew)
        { }

        public void BeginEdit()
        {
            this.Cache = Activator.CreateInstance<T>();

            //Set Properties of Cache
            foreach (var info in this.CurrentModel.GetType().GetProperties())
            {
                if (!info.CanRead || !info.CanWrite)
                {
                    continue;
                }

                var oldValue = info.GetValue(this.CurrentModel, null);
                var editableObject = oldValue as IEditableObject;
                editableObject?.BeginEdit();

                this.Cache.GetType().GetProperty(info.Name).SetValue(this.Cache, oldValue, null);
            }
        }

        public void EndEdit()
        {
            this.Cache = default(T);
        }

        public void CancelEdit()
        {
            foreach (var info in this.CurrentModel.GetType().GetProperties())
            {
                if (!info.CanRead || !info.CanWrite)
                {
                    continue;
                }

                var oldValue = info.GetValue(this.Cache, null);
                var editableObject = oldValue as IEditableObject;
                editableObject?.CancelEdit();

                this.CurrentModel.GetType().GetProperty(info.Name).SetValue(this.CurrentModel, oldValue, null);
            }
        }

        public List<ChangeEntry> GetChanges()
        {
            var changes =
                (
                    from info in this.CurrentModel.GetType().GetProperties()
                    where info.CanRead
                    let oldValue = info.GetValue(this.Cache, null)
                    let propertyInfo = this.CurrentModel.GetType().GetProperty(info.Name)
                    let newValue = propertyInfo.GetValue(this.CurrentModel, null)
                    where !newValue.Equals(oldValue) && !(oldValue is RelayCommand)
                    select new ChangeEntry
                    {
                        PropertyInfo = propertyInfo,
                        OldValue = oldValue,
                        NewValue = newValue
                    }
                    ).ToList();

            return changes;
        }
    }
}