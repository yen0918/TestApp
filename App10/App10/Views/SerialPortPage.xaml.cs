using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SerialPortPage : ContentPage
    {

        public SerialPortPage()
        {
            InitializeComponent();
        }

        private void btn_SerialScan(object sender, EventArgs e)
        {

        }

        private void foundSerialDevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}