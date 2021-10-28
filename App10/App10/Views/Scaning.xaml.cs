using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Bluetooth;
using XamarinEssentials = Xamarin.Essentials;
using App10.ViewModels;
using Xamarin.Essentials;
using System.Linq;
using System.Collections.ObjectModel;
using Java.Lang;
using Android.Content;
using System.Text;
using System.Threading;

namespace App10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scaning : ContentPage
    {
        public readonly IAdapter _bluetoothAdapter;
        public List<IDevice> _gattDevices = new List<IDevice>();
        public List<object> _gattDevices_noname = new List<object>();
        //UserViewModel user = new UserViewModel();
        public Scaning()
        {
            InitializeComponent();
            _bluetoothAdapter = CrossBluetoothLE.Current.Adapter;
            _bluetoothAdapter.DeviceDiscovered += (sender, foundBleDevice) =>
            {
                if (foundBleDevice.Device != null && !string.IsNullOrEmpty(foundBleDevice.Device.Name))//
                {
                    _gattDevices.Add(foundBleDevice.Device);
                    
                } else if (foundBleDevice.Device!=null&&string.IsNullOrEmpty(foundBleDevice.Device.Name)) 
                { 
                    _gattDevices_noname.Add(foundBleDevice.Device.NativeDevice); 
                }
            };
        }

        public async Task<bool> PermissionsGrantedAsync()
        {
            var locationPermissionStatus = await XamarinEssentials.Permissions.CheckStatusAsync<XamarinEssentials.Permissions.LocationAlways>();

            if (locationPermissionStatus != XamarinEssentials.PermissionStatus.Granted)
            {
                var status = await XamarinEssentials.Permissions.RequestAsync<XamarinEssentials.Permissions.LocationAlways>();
                return status == XamarinEssentials.PermissionStatus.Granted;
            }
            return true;
        }

        public async void StartScanning(object sender, EventArgs e)
        {
            AI.IsVisible = AI.IsRunning = !(ControlButton.IsEnabled = false);
            foundBleDevicesListView.ItemsSource = null;
            native.ItemsSource = null;
            con_scan.Text = "Scanning";
            
            if (!await PermissionsGrantedAsync())
            {
                await DisplayAlert("Permission required", "Application needs location permission", "OK");
                AI.IsVisible = AI.IsRunning = !(ControlButton.IsEnabled = true);
                return;
            }
            
            _gattDevices.Clear();
            _gattDevices_noname.Clear();

            foreach (var device in _bluetoothAdapter.ConnectedDevices)
            { 
                _gattDevices.Add(device);
                _gattDevices_noname.Add(device.NativeDevice);
            }
            
            await _bluetoothAdapter.StartScanningForDevicesAsync();


            native.ItemsSource = _gattDevices_noname.ToArray();
            foundBleDevicesListView.ItemsSource = _gattDevices.ToArray();
            
            lab_Scan.IsVisible = true;
            lab_Scan_1.IsVisible = true;
            AI.IsVisible = AI.IsRunning = !(ControlButton.IsEnabled = true);
        }

        IDevice selectedItem;
        

        public async void FoundBluetoothDevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AI.IsVisible = AI.IsRunning = !(ControlButton.IsEnabled = false);
            AI.Margin = 20;
            con_scan.Text = "Connecting...";
            selectedItem = e.Item as IDevice;
            Preferences.Set("sta_Forgot_isVisible", true);
            UserModel.getDevice = selectedItem;

            if (selectedItem.State == DeviceState.Connected)
            {
                UserModel.DeviceName = selectedItem.Name;
                UserModel.DeviceAddress = selectedItem.NativeDevice.ToString();
                UserModel.DeviceStatus = selectedItem.State.ToString();

                Preferences.Set("btn_Disconnect_isEnabled", true);
                Preferences.Set("btn_Forgot_isEnabled", true);

                var services = await selectedItem.GetServicesAsync();
                
                var service_UUID = await selectedItem.GetServiceAsync(GattIdentifiers.SENSIML_EVENT_SERVICE_UUID);

                var characteristics = await service_UUID.GetCharacteristicsAsync();

                foreach (var service in services)
                {
                    if (service.Id == service_UUID.Id)
                    {
                        var _characteristic = await service_UUID.GetCharacteristicAsync(GattIdentifiers.SENSIML_EVENT_CHARACTERISTIC_UUID);
                        
                        foreach (var characteristic in characteristics) 
                        {
                            if (characteristic.Uuid == _characteristic.Uuid)
                            {
                                var _descriptor = await _characteristic.GetDescriptorAsync(GattIdentifiers.SENSIML_EVENT_DESCRIPTOR_UUID);
                                var descriptors = await characteristic.GetDescriptorsAsync();
                                
                                foreach (var descriptor in descriptors)
                                {
                                    if (descriptor.Id==_descriptor.Id) 
                                    {
                                        await characteristic.StopUpdatesAsync();
                                        try
                                        {
                                            await Task.Run(async () =>
                                            {
                                                var byt = characteristic.Value;
                                                    characteristic.ValueUpdated += (o, args) =>
                                                    {
                                                        byt = args.Characteristic.Value;
                                                        MainThread.BeginInvokeOnMainThread(() =>
                                                        {
                                                            UserViewModel.valueText = null;
                                                            //for (int i = 0; i < byt.Length; i++)
                                                            //{
                                                            //UserViewModel.valueText += byt.GetValue(i).ToString();
                                                            //}
                                                            //因Feather依上面迴圈取出的值為"X,0,0,0"，而Merced取出的值為"0,0,X,0"
                                                            //為了在這兩塊板內只取X寫了以下判斷式，判斷了兩字串內的第一個數字為不為0
                                                            //若為0，則為Merced之值，因此取之X為引數2；若不為0，則為Feather之值，因此取之X為引數0
                                                            int j=0;
                                                            for (int i = 0;  i<3;i++)
                                                            {
                                                                j += int.Parse(byt.GetValue(i).ToString());
                                                            }                                                                                                                     
                                                                UserViewModel.valueText = j.ToString();
                                                        }); 
                                                    }; 
                                                await characteristic.StartUpdatesAsync();
                                            });
                                        }
                                        catch 
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                await Navigation.PopAsync();
            }
            else
            {
                try
                {
                    var connectParameters = new ConnectParameters(false, true);
                    await _bluetoothAdapter.ConnectToDeviceAsync(selectedItem, connectParameters);
                }
                catch
                {
                    if (selectedItem.Name == null)
                    {
                        await DisplayAlert("Error connecting", $"Error connecting to BLE device: {selectedItem.NativeDevice ?? "N/A"}", "Retry");
                    }
                    else 
                    {
                        await DisplayAlert("Error connecting", $"Error connecting to BLE device: {selectedItem.Name ?? "N/A"}", "Retry");
                    }
                }
            }

            AI.IsVisible = AI.IsRunning = !(AI.IsEnabled = true);
        }


        private async void FoundNativeDevice(object sender, ItemTappedEventArgs e)
        {
            AI.IsVisible = AI.IsRunning = !(ControlButton.IsEnabled = false);
            AI.Margin = 20;
            con_scan.Text = "Connecting...";
            object selectItem_1 = e.Item as object;
            await DisplayAlert("Error connecting", $"Error connecting to BLE device: {selectItem_1.ToString() ?? "N/A"}", "Retry");
        }
    }
}