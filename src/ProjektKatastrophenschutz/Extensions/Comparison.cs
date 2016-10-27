using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BSA.Core.Utils;
using GalaSoft.MvvmLight.Command;

namespace ProjektKatastrophenschutz.Extensions
{
    public static class Comparison
    {
        public static List<ChangeEntry> GetChanges<T>(T oldObject, T newObject)
        {
            var changes = new List<ChangeEntry>();

            foreach (var info in oldObject.GetType().GetProperties())
            {
                if (!info.CanRead)
                    continue;

                var oldValue = info.GetValue(oldObject, null);
                var propertyInfo = newObject.GetType().GetProperty(info.Name);
                var newValue = propertyInfo.GetValue(newObject, null);

                if (oldValue == null && newValue == null)
                    continue;

                if (oldValue == null || newValue == null)
                {
                    if (!(oldValue is RelayCommand))
                    {
                        changes.Add(new ChangeEntry
                        {
                            PropertyInfo = propertyInfo,
                            OldValue = oldValue,
                            NewValue = newValue
                        });

                        continue;
                    }
                    
                }

                if (oldValue.Equals(newValue) || oldValue == newValue)
                    continue;

                if (!(oldValue is RelayCommand))
                {
                    changes.Add(new ChangeEntry
                    {
                        PropertyInfo = propertyInfo,
                        OldValue = oldValue,
                        NewValue = newValue
                    });
                }
            }

            return changes;
        }
    }
}
