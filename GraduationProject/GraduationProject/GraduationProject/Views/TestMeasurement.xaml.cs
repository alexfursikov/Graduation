using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using GraduationProject.Models;

namespace GraduationProject.Views
{
    /// <summary>
    /// Interaction logic for TestMeasurement.xaml
    /// </summary>
    public partial class TestMeasurement
    {
        private static BluetoothEndPoint LocalEndpoint { get; set; }
        private static BluetoothClient BluetoothClient { get; set; }
        private static BluetoothClient BluetoothForkClient { get; set; }
        private static BluetoothDeviceInfo BtDevice { get; set; }
        private static BluetoothDeviceInfo ForkBtDevice { get; set; }
        private static NetworkStream Stream { get; set; }
        private static NetworkStream ForkStream { get; set; }
        private static DispatcherTimer Timer { get; set; }

        private bool isHasDiameterTwo;

        private DataModel dataModel;

        public TestMeasurement()
        {
            InitializeComponent();
            SetStartupSettings();
            DataContext = ViewModel;
        }

        private void Mode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Device_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtDevice = (sender as ComboBox)?.SelectedItem as BluetoothDeviceInfo;
            ParseStringToObject("");
            //if (BtDevice != null)
            //{
            //    if (BluetoothSecurity.PairRequest(BtDevice.DeviceAddress, "1111"))
            //    {
            //        if (BtDevice.Authenticated)
            //        {
            //            ViewModel.BluetoothDeviceInfo = BtDevice;

            //            if(ViewModel.ForkDeviceInfo != null)
            //            {
            //            Ellipse.Fill = Brushes.DarkGreen;
            //            }

            //            BluetoothClient.SetPin("1111");
            //            BluetoothClient.BeginConnect(BtDevice.DeviceAddress, BluetoothService.SerialPort, Connect,
            //                BtDevice);
            //        }
            //        else
            //        {
            //            ViewModel.BluetoothDeviceInfo = null;
            //            MessageBox.Show("Аутентификация не пройдена. Попробуйте еще раз.");
            //        }
            //    }
            //    else
            //    {
            //        ViewModel.BluetoothDeviceInfo = null;
            //        MessageBox.Show("Сопряжение с устройством не установлено.");
            //    }
            //}
        }

        private void Fork_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ForkBtDevice = (sender as ComboBox)?.SelectedItem as BluetoothDeviceInfo;
            ParseForkStringToObject("");
            //if (ForkBtDevice != null)
            //{
            //    if (BluetoothSecurity.PairRequest(ForkBtDevice.DeviceAddress, "1234"))
            //    {
            //        if (ForkBtDevice.Authenticated)
            //        {
            //            ViewModel.ForkDeviceInfo = ForkBtDevice;

            //            if (ViewModel.BluetoothDeviceInfo != null)
            //            {
            //                Ellipse.Fill = Brushes.DarkGreen;
            //            }

            //            BluetoothForkClient.SetPin("1234");
            //            BluetoothForkClient.BeginConnect(ForkBtDevice.DeviceAddress, BluetoothService.SerialPort,
            //                ConnectToFork,
            //                ForkBtDevice);
            //        }
            //    }
            //}
        }

        private void Connect(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                for (;;)
                {
                    Stream = BluetoothClient.GetStream();
                    if (Stream.CanRead)
                    {
                        byte[] myReadBuffer = new byte[1024];
                        string myCompleteMessage = "";
                        do
                        {
                            Thread.Sleep(1000);
                            Stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                            myCompleteMessage += Encoding.ASCII.GetString(myReadBuffer).Replace("\0", "");
                        } while (Stream.DataAvailable);

                        Application.Current.Dispatcher.Invoke(
                            new ThreadStart(() => ParseStringToObject(myCompleteMessage)));
                    }
                    else
                    {
                        MessageBox.Show("Не удалось прочитать данные.");
                    }
                }
            }
        }

        private void ConnectToFork(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                for (;;)
                {
                    ForkStream = BluetoothForkClient.GetStream();
                    if (ForkStream.CanRead)
                    {
                        var myReadBuffer = new byte[1024];
                        var myCompleteMessage = "";

                        while (ForkStream.DataAvailable)
                        {
                            Thread.Sleep(1000);
                            ForkStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                            myCompleteMessage += Encoding.ASCII.GetString(myReadBuffer).Replace("\0", "");
                        } 

                        if(!string.IsNullOrWhiteSpace(myCompleteMessage))
                        {
                            Application.Current.Dispatcher.Invoke(
                            new ThreadStart(() => ParseForkStringToObject(myCompleteMessage)));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось прочитать данные.");
                    }
                }
            }
        }

        private void ParseStringToObject(string message)
        {
            //if (!string.IsNullOrWhiteSpace(message) && message != "$")
            {
                SystemSounds.Beep.Play();
                //MessageBox.Show(message);
                //var arrayData = message.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                //var indexHv = Array.IndexOf(arrayData, "HV") + 1;
                //var indexM = Array.IndexOf(arrayData, "M") + 1;
                //var indexD = Array.IndexOf(arrayData, "D") + 1;
                //var indexMl = Array.IndexOf(arrayData, "ML") + 1;
                //var indexHt = Array.IndexOf(arrayData, "HT") + 1;
                //var indexD1 = indexD == 0 ? 0 : indexD + 2;
                if (dataModel == null)
                {
                    dataModel = new DataModel();
                }

                dataModel.HorizontalDistance = 5; // == 0 ? null : CurrentContext.ToDoubleParse(arrayData[indexHv]);
                dataModel.Azimuth = 5; // == 0 ? null : CurrentContext.ToDoubleParse(arrayData[indexM]);
                dataModel.Bias = 5; // == 0 ? null : CurrentContext.ToDoubleParse(arrayData[indexD]);
                dataModel.SlopeDistance = 5; // == 0 ? null : CurrentContext.ToDoubleParse(arrayData[indexD1]);
                dataModel.Hight = 5; // == 0 ? null : CurrentContext.ToDoubleParse(arrayData[indexHt]);
                dataModel.NotAvailableDinstance = 5; // == 0 ? null : CurrentContext.ToDoubleParse(arrayData[indexMl]);
                ViewModel.Measurements.Add(dataModel);
                CurrentContext.DataList.Add(dataModel);
            }
        }

        private void ParseForkStringToObject(string message)
        {
            SystemSounds.Beep.Play();

            //string[] temp = message.Split(new[] { ',' });
            string species;
            int dia;
            species = "Bereza";//temp[3];
            dia = int.Parse("150"/*temp[7]*/);

            if (dataModel == null)
            {
                dataModel = new DataModel();
            }

            dataModel.Id = Interlocked.Increment(ref CurrentContext.GlobalId);
            dataModel.Species = species;
            dataModel.DiameterOne = dia;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("ID,HV,Meter,Fut,D,D1,F,ML,HT,ControlSumm");
            foreach (var item in ViewModel.Measurements)
            {
                stringBuilder.AppendLine(item.ToString());
            }

            System.IO.File.WriteAllText(
                System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Measurements.csv"),
                stringBuilder.ToString());
        }

        private void SetStartupSettings()
        {
            try
            {
                LocalEndpoint = new BluetoothEndPoint(BluetoothAddress.Parse(CurrentContext.GetMacAddress()),
                    BluetoothService.SerialPort);
                BluetoothClient = new BluetoothClient(LocalEndpoint);
                BluetoothForkClient = new BluetoothClient(LocalEndpoint);
                Timer = new DispatcherTimer();
                Timer.Tick += UpdateBluetoothDevices;
                Timer.Interval = new TimeSpan(0, 0, 10);
                Timer.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Bluetooth не включен.");
                throw;
            }
        }

        private async void UpdateBluetoothDevices(object sender, EventArgs e)
        {
            //await Task.Run(() => CurrentContext.UpdateDevices());
            ViewModel.Devices = new ObservableCollection<BluetoothDeviceInfo>(CurrentContext.Devices);
            if (!IsConnectToDevice(BtDevice))
            {
                Ellipse.Fill = Brushes.Red;
            }
        }

        private void ButtonDeleteRow_OnClick(object sender, RoutedEventArgs e)
        {
            var measurement = DataGrid.SelectedItem as DataModel;
            var maxId = CurrentContext.DataList.Max(x => x.Id);
            if (maxId == measurement?.Id)
            {
                CurrentContext.GlobalId = maxId - 1;
            }

            ViewModel.Measurements.Remove(measurement);
            CurrentContext.DataList.RemoveAll(model => model.Id == measurement?.Id);
        }

        private bool IsConnectToDevice(BluetoothDeviceInfo bluetoothDevice)
        {
            if (bluetoothDevice != null && bluetoothDevice.Authenticated)
            {
                return true;
            }
            ViewModel.BluetoothDeviceInfo = null;
            return false;
        }
    }
}