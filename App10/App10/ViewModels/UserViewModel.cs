using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App10.ViewModels
{
    class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string _value;
        public string _valueText;
        public static string valueText;
        public string Class;

        public UserViewModel()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
             {
                 Value = valueText;
                 ValueText = ValueToName(Convert.ToInt32(Value));
                 return true;
             });
        }

        public string ValueText
        {
            private set
            {

                if( _valueText!=value )
                {
                    _valueText = value;
                }
                OnPropertyChanged();
            }
            get
            {
                return _valueText;
            }
        }
        public string Value
        {
            private set
            {
                if (_value != value)
                {
                    _value = value;
                }
                OnPropertyChanged();
            }
            get
            {
                return _value;
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string ValueToName(int value)
        {

            string[] _Class = Preferences.Get("Classes", "").Split('\n');
            try
            {
                switch (value)
                {
                    case 1:
                        Class = _Class[0];
                        break;
                    case 2:
                        Class = _Class[1];
                        break;
                    case 3:
                        Class = _Class[2];
                        break;
                    case 4:
                        Class = _Class[3];
                        break;
                    case 5:
                        Class = _Class[4];
                        break;
                    case 6:
                        Class = _Class[5];
                        break;
                    case 7:
                        Class = _Class[6];
                        break;
                    case 8:
                        Class = _Class[7];
                        break;
                    case 9:
                        Class = _Class[8];
                        break;
                    case 10:
                        Class = _Class[9];
                        break;
                    case 11:
                        Class = _Class[10];
                        break;
                    case 12:
                        Class = _Class[11];
                        break;
                    case 13:
                        Class = _Class[12];
                        break;
                    case 14:
                        Class = _Class[13];
                        break;
                    case 15:
                        Class = _Class[14];
                        break;
                    case 16:
                        Class = _Class[15];
                        break;
                    case 17:
                        Class = _Class[16];
                        break;
                    case 18:
                        Class = _Class[17];
                        break;
                    case 19:
                        Class = _Class[18];
                        break;
                    case 20:
                        Class = _Class[19];
                        break;
                }
            }
            catch (Exception)
            {
            }
            
            return Class;
        }
    }
}
