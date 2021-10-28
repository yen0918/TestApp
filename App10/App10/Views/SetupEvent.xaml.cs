using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using SensiML_Test_App.Models;
namespace App10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupEvent : ContentPage
    {
        public SetupEvent()
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListView_class.ItemsSource = ImportPage.List_Class;
        }

        private async void btn_Import(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new ImportPage());
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            ListView_class.ItemsSource = ImportPage.List_Class;
        }
    }
}