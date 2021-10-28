using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using SensiML_Test_App.Models;

namespace App10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static  List<Status> List_Class;
        public Status status = new Status();
        public ImportPage()
        {
            InitializeComponent();
        }
        private async void btn_Done(object sender, EventArgs e)
        {
            try
            {
                List_Class = new List<Status>();
                string JsonData = Import_JSON.Text;
                var myDeserializedClass = JsonConvert.DeserializeObject<dynamic>(JsonData);
                string[] Maps = $"{myDeserializedClass.ModelDescriptions[0].ClassMaps}".Split('\n');
                JsonData = "";
                for (int i = 1; i < Maps.Length - 2; i++)
                {
                    List_Class.Add(new Status() { Class = $"{myDeserializedClass.ModelDescriptions[0].ClassMaps[i.ToString()]}",Number=i });
                    JsonData += $"{myDeserializedClass.ModelDescriptions[0].ClassMaps[i.ToString()]}\n";
                }
                Preferences.Set("Classes", JsonData);
            }
            catch (Exception )
            {
            }          
            //Preferences.Set("sta_Forgot_isVisible", true);
            await Navigation.PopAllPopupAsync();
        }
    }
}