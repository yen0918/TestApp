using System;
using System.Collections.Generic;
using System.Text;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;

namespace App10.ViewModels
{
    public class UserModel
    {
        public static String DeviceName { get; set; }
        public static String DeviceAddress { get; set; }
        public static String DeviceStatus { get; set; }
        public static String getValue { get; set; }
        public static IDevice getDevice { get; set; }
    }
}
