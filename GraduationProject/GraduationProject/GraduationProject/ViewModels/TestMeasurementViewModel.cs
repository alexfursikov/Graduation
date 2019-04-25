using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraduationProject.Models;
using InTheHand.Net.Sockets;

namespace GraduationProject.ViewModels
{
    public class TestMeasurementViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<string> Modes { get; set; }

        private ObservableCollection<BluetoothDeviceInfo> _devices;
        public ObservableCollection<BluetoothDeviceInfo> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                OnPropertyChanged("Devices");
            }
        }

        private ObservableCollection<DataModel> _measurements;
        public ObservableCollection<DataModel> Measurements
        {
            get { return _measurements; }
            set
            {
                _measurements = value;
                OnPropertyChanged("Measurements");
            }
        }

        private BluetoothDeviceInfo _bluetoothDeviceInfo;
        public BluetoothDeviceInfo BluetoothDeviceInfo
        {
            get { return _bluetoothDeviceInfo; }
            set
            {
                _bluetoothDeviceInfo = value;
                OnPropertyChanged("BluetoothDeviceInfo");
            }
        }

        private BluetoothDeviceInfo _forkDeviceInfo;
        public BluetoothDeviceInfo ForkDeviceInfo
        {
            get { return _forkDeviceInfo; }
            set
            {
                _forkDeviceInfo = value;
                OnPropertyChanged("ForkDeviceInfo");
            }
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                OnPropertyChanged("Mode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public TestMeasurementViewModel()
        {
            CurrentContext.UpdateDevices();
            Devices = new ObservableCollection<BluetoothDeviceInfo>(CurrentContext.Devices);
            Measurements = new ObservableCollection<DataModel>(CurrentContext.DataList);
            Modes = new ObservableCollection<string>
            {
                "Тестовый режим", "Режим №1", "Режим №2", "Режим №3", "Режим №4"
            };
            Mode = Modes[0];
        }
    }
}
