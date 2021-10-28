using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Bluetooth;
using Android.Service;
using Android.Content;
using System.Diagnostics;
using XamarinEssentials = Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.Generic;
using App10.ViewModels;
using Xamarin.Essentials;

namespace App10.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lab_name.Text = UserModel.DeviceName;
            lab_adress.Text = UserModel.DeviceAddress;
            lab_status.Text = UserModel.DeviceStatus;
            //device_value.Text = UserModel.getValue;
            btn_Disconnect1.IsEnabled = Preferences.Get("btn_Disconnect_isEnabled", btn_Disconnect1.IsEnabled = true);
            btn_forgot1.IsEnabled = Preferences.Get("btn_Forgot_isEnabled", btn_forgot1.IsEnabled = true);
            sta_forgot.IsVisible = Preferences.Get("sta_Forgot_isVisible", sta_forgot.IsVisible = true);
            ListView_class.ItemsSource = ImportPage.List_Class;
        }

        private async void FindButton(object sender, EventArgs e)//Find device按鈕
        {
            //string ConnectWay = await DisplayActionSheet("Choose how to connect", null, null, "Bluetooth", "Serial port");
            //if (ConnectWay == "Bluetooth")
            //{
                await Navigation.PushAsync(new Scaning());//強制堆疊頁面
            //}
            //else 
            //{
            //    await Navigation.PushAsync(new SerialPortPage());
            //}
        }

        private async void btn_Disconnect(object sender, EventArgs e)
        {
            await CrossBluetoothLE.Current.Adapter.DisconnectDeviceAsync(UserModel.getDevice);
            UserModel.DeviceStatus = DeviceState.Disconnected.ToString();
            lab_status.Text = UserModel.DeviceStatus;
            btn_Disconnect1.IsEnabled = false;
        }

        private void btn_forgot(object sender, EventArgs e)//forgot按鈕
        {
            btn_Disconnect1.IsEnabled = false;//使connect按鈕關閉
            btn_forgot1.IsEnabled = false;
            lab_name.Text = "";//清除連接資訊
            lab_adress.Text = "";
            lab_status.Text = "";
            sta_forgot.IsVisible = false;//隱藏按鈕以下的介面
            Preferences.Set("Classes", "");
        }
    }
}