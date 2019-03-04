using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dingus.Models
{
    public class ObservableModel : INotifyPropertyChanged
    {
        private Dictionary<string, object> _variables;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableModel()
        {
            _variables = new Dictionary<string, object>();
        }

        public void Set<T>(T value, [CallerMemberName]string var = null)
        {
            _variables[var] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(var));
        }

        public T Get<T>([CallerMemberName]string var = null)
        {
            if(!_variables.ContainsKey(var))
            {
                _variables.Add(var, default(T));
            }
            return (T)_variables[var];
        }
    }
}
