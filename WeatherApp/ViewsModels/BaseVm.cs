using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.ViewsModels
{
    public class BaseVm : INotifyPropertyChanged
    {
        private bool _Isbusy;

        public bool IsBusy
        {
            get => _Isbusy;
            set {

                if (_Isbusy == value)
                    return;
               
                _Isbusy = value;
                OnPropertyChanged();
                
            }
        }
       
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
