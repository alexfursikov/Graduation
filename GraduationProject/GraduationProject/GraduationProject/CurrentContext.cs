using System.Collections.Generic;
using System.Device.Location;
using System.Net.NetworkInformation;
using GraduationProject.Models;
using InTheHand.Net.Sockets;
using System.Threading.Tasks;

namespace GraduationProject
{
    public static class CurrentContext
    {
        public static GeoCoordinateWatcher Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
        public static List<DataModel> DataList = new List<DataModel>();
        public static List<BluetoothDeviceInfo> Devices = new List<BluetoothDeviceInfo>();
        public static int GlobalId;
        
        public static void LocationMessage()
        {
            var whereat = Watcher.Position.Location;

            var lat = whereat.Latitude.ToString("0.000000");
            var lon = whereat.Longitude.ToString("0.000000");
            //MessageBox.Show($"Lat: {lat}\nLon: {lon}");
        }

        public static async Task UpdateDevices()
        {
            var client = new BluetoothClient();
            var devices =  client.DiscoverDevices();
            Devices = new List<BluetoothDeviceInfo>(devices);
        }

        public static string GetMacAddress()
        {
            string mac = "";
            string result = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Down && !nic.Description.Contains("Virtual") &&
                    !nic.Description.Contains("Pseudo") && !nic.Description.Contains("Wireless"))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            for (int i = 0; i < mac.Length; i++)
            {
                result += mac[i];
                if (i % 2 != 0 && mac.Length - 1 != i)
                {
                    result += ":";
                }
            }
            return result;
        }

        public static double? ToDoubleParse(string variable)
        {
            double result;
            if (double.TryParse(variable, out result))
            {
                return result;
            }
            return null;
        }
    }
}