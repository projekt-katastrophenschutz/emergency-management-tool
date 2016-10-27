using System;
using System.Windows.Input;
using System.Windows.Controls;

namespace ProjektKatastrophenschutz.ViewModels
{
    public static class CalendarCommands
    {
        private static readonly ResetCommand ResetDateCommand = new ResetCommand();

        public static ICommand ResetDate => ResetDateCommand;

        private sealed class ResetCommand : ICommand
        {
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                return parameter is Calendar;
            }

            public void Execute(object parameter)
            {
                var calendar = parameter as Calendar;
                if (calendar == null)
                    return;

                calendar.SelectedDate = null;
            }
        }
    }
}
