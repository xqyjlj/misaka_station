using Microsoft.Win32;
using MstnApp.Event.Core;
using MstnAPP.Modules.Page.RTThread.Services;
using MstnAPP.Services.Driver;
using MstnAPP.Services.Sys.DataFlie;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Threading;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class SettingViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly ISerial _serial;

        private readonly IEventAggregator _eventAggregator;

        private readonly IIniFile _iniFile;

        private readonly Thread _thread;

        private readonly ServicesSerialData _serialData = new();

        public bool KeepAlive => false;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ReadParameters();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            SaveParameters();
        }

        public SettingViewModel(ISerial serial, IIniFile iniFile, IEventAggregator eventAggregator)
        {
            _serial = serial;
            _iniFile = iniFile;

           
            _serial.PortNamesChanged += new EPortNameChanged(SerialPortNameChanged);
            _serial.ConnectChanged += new EConnectChanged(SerialConnectChanged);
            _serial.DataReceived += new EDataReceived(SerialDataReceived);

            ListComboBoxPort = _serial.GetPortNames();
            _eventAggregator = eventAggregator;
            _ = _eventAggregator.GetEvent<EventClose>().Subscribe(CloseEventReceived);

            _thread = new Thread(_serialData.ParsedData);
            InitListComboBox();
            ReadParameters();
        }

        private void CloseEventReceived(string message)
        {
            SaveParameters();
        }

        private void SerialConnectChanged(bool isConnect)
        {
            _ = SetProperty(ref _toggleButtonSerialIsChecked, isConnect);
        }

        private void SerialPortNameChanged(List<string> portNames)
        {
            ListComboBoxPort = portNames;
        }

        private void SerialDataReceived(string data)
        {
            _serialData.AddBuffer(data);
            _thread.Start();
        }

        private void InitListComboBox()
        {
            foreach (string item in GenerateBaudRateItems())
            {
                ListComboBoxBaudRate.Add(item);
            }

            foreach (string item in GenerateParityItems())
            {
                ListComboBoxParity.Add(item);
            }

            foreach (string item in GenerateDataBitsItems())
            {
                ListComboBoxDataBits.Add(item);
            }

            foreach (string item in GenerateStopBitsItems())
            {
                ListComboBoxStopBits.Add(item);
            }

            foreach (string item in GenerateHandshakeItems())
            {
                ListComboBoxHandshake.Add(item);
            }
        }

        private bool _isHasBeenSetSerial;

        private void OpenSerial()
        {
            if (_isHasBeenSetSerial)
            {
                if (_serial.Connected())
                {
                    _serial.Close();
                }
            }
            _ = _serial.SetPortName(ListComboBoxPortSelectedItem);
            _serial.SetBaudRate(ListComboBoxBaudRateSelectedItem);
            _ = _serial.SetParity(ListComboBoxParitySelectedItem);
            _ = _serial.SetDataBits(ListComboBoxDataBitsSelectedItem);
            _ = _serial.SetStopBits(ListComboBoxStopBitsSelectedItem);
            _ = _serial.SetHandshake(ListComboBoxHandshakeSelectedItem);
            _isHasBeenSetSerial = true;
            _serial.Open();
        }

        private void CloseSerial()
        {
            _serial.Close();
        }

        private void RefreshToggleButtonSerialIsEnabled()
        {
            ToggleButtonSerialIsEnabled = _listComboBoxPortSelectedItem != null
                && _listComboBoxBaudRateSelectedItem != null
                && _listComboBoxParitySelectedItem != null
                && _listComboBoxDataBitsSelectedItem != null
                && _listComboBoxStopBitsSelectedItem != null
                && _listComboBoxHandshakeSelectedItem != null;
        }

        private void SaveParameters()
        {
            if (ListComboBoxPortSelectedItem != null)
            {
                _iniFile.SetRTThreadPort(ListComboBoxPortSelectedItem);
            }
            if (ListComboBoxBaudRateSelectedItem != null)
            {
                _iniFile.SetRTThreadBaudRate(ListComboBoxBaudRate.IndexOf(ListComboBoxBaudRateSelectedItem));
            }
            if (ListComboBoxParitySelectedItem != null)
            {
                _iniFile.SetRTThreadParity(ListComboBoxParity.IndexOf(ListComboBoxParitySelectedItem));
            }
            if (ListComboBoxDataBitsSelectedItem != null)
            {
                _iniFile.SetRTThreadDataBits(ListComboBoxDataBits.IndexOf(ListComboBoxDataBitsSelectedItem));
            }
            if (ListComboBoxStopBitsSelectedItem != null)
            {
                _iniFile.SetRTThreadStopBits(ListComboBoxStopBits.IndexOf(ListComboBoxStopBitsSelectedItem));
            }
            if (ListComboBoxHandshakeSelectedItem != null)
            {
                _iniFile.SetRTThreadHandshake(ListComboBoxHandshake.IndexOf(ListComboBoxHandshakeSelectedItem));
            }
            _iniFile.SetRTThreadIsSaveData(CheckBoxIsSaveDataIsChecked);
            if (TextBoxSaveDataPathText != null)
            {
                _iniFile.SetRTThreadSaveDataPath(TextBoxSaveDataPathText);
            }
            _iniFile.SetRTThreadIsExistPassword(CheckBoxIsExistPasswordIsChecked);
            if (PasswordBoxPasswordPassword != null)
            {
                _iniFile.SetRTThreadPassword(PasswordBoxPasswordPassword);
            }
            _iniFile.SetRTThreadFlushTime(SliderFlushTimeValue);
        }

        private void ReadParameters()
        {
            string port = _iniFile.GetRTThreadPort();
            if (ListComboBoxPort.Contains(port))
            {
                ListComboBoxPortSelectedItem = port;
            }
            ListComboBoxBaudRateSelectedItem = ListComboBoxBaudRate[_iniFile.GetRTThreadBaudRate()];
            ListComboBoxParitySelectedItem = ListComboBoxParity[_iniFile.GetRTThreadParity()];
            ListComboBoxDataBitsSelectedItem = ListComboBoxDataBits[_iniFile.GetRTThreadDataBits()];
            ListComboBoxStopBitsSelectedItem = ListComboBoxStopBits[_iniFile.GetRTThreadStopBits()];
            ListComboBoxHandshakeSelectedItem = ListComboBoxHandshake[_iniFile.GetRTThreadHandshake()];
            CheckBoxIsSaveDataIsChecked = _iniFile.GetRTThreadIsSaveData();
            TextBoxSaveDataPathText = _iniFile.GetRTThreadSaveDataPath();
            SliderFlushTimeValue = _iniFile.GetRTThreadFlushTime();
            CheckBoxIsExistPasswordIsChecked = _iniFile.GetRTThreadIsExistPassword();
            PasswordBoxPasswordPassword = _iniFile.GetRTThreadPassword();
        }

        #region 创建Items

        private static IEnumerable<string> GenerateBaudRateItems()
        {
            yield return "1200";
            yield return "2400";
            yield return "4800";
            yield return "9600";
            yield return "14400";
            yield return "19200";
            yield return "38400";
            yield return "43000";
            yield return "57600";
            yield return "115200";
            yield return "128000";
            yield return "230400";
            yield return "256000";
            yield return "460800";
            yield return "921600";
            yield return "1382400";
            yield return "1500000";
        }

        private static IEnumerable<string> GenerateParityItems()
        {
            yield return "无校验";
            yield return "奇校验";
            yield return "偶校验";
            yield return "一校验";
            yield return "零校验";
        }

        private static IEnumerable<string> GenerateDataBitsItems()
        {
            yield return "5";
            yield return "6";
            yield return "7";
            yield return "8";
        }

        private static IEnumerable<string> GenerateStopBitsItems()
        {
            yield return "1";
            yield return "1.5";
            yield return "2";
        }

        private static IEnumerable<string> GenerateHandshakeItems()
        {
            yield return "无流控";
            yield return "软件流控";
            yield return "硬件流控";
            yield return "软硬件流控";
        }

        #endregion 创建Items

        #region 绑定

        #region 数据绑定

        #region ListComboBoxPort

        private IList<string> _listComboBoxPort;

        public IList<string> ListComboBoxPort
        {
            get => _listComboBoxPort;
            set => _ = SetProperty(ref _listComboBoxPort, value);
        }

        #endregion ListComboBoxPort

        #region ListComboBoxPortSelectedItem

        private string _listComboBoxPortSelectedItem;

        public string ListComboBoxPortSelectedItem
        {
            get => _listComboBoxPortSelectedItem;
            set
            {
                _ = SetProperty(ref _listComboBoxPortSelectedItem, value);
                RefreshToggleButtonSerialIsEnabled();
            }
        }

        #endregion ListComboBoxPortSelectedItem

        #region ListComboBoxBaudRate

        private IList<string> _listComboBoxBaudRate = new List<string>();

        public IList<string> ListComboBoxBaudRate
        {
            get => _listComboBoxBaudRate;
            set => _ = SetProperty(ref _listComboBoxBaudRate, value);
        }

        #endregion ListComboBoxBaudRate

        #region ListComboBoxBaudRateSelectedItem

        private string _listComboBoxBaudRateSelectedItem;

        public string ListComboBoxBaudRateSelectedItem
        {
            get => _listComboBoxBaudRateSelectedItem;
            set
            {
                _ = SetProperty(ref _listComboBoxBaudRateSelectedItem, value);
                RefreshToggleButtonSerialIsEnabled();
            }
        }

        #endregion ListComboBoxBaudRateSelectedItem

        #region ListComboBoxParity

        private IList<string> _listComboBoxParity = new List<string>();

        public IList<string> ListComboBoxParity
        {
            get => _listComboBoxParity;
            set => _ = SetProperty(ref _listComboBoxParity, value);
        }

        #endregion ListComboBoxParity

        #region ListComboBoxParitySelectedItem

        private string _listComboBoxParitySelectedItem;

        public string ListComboBoxParitySelectedItem
        {
            get => _listComboBoxParitySelectedItem;
            set
            {
                _ = SetProperty(ref _listComboBoxParitySelectedItem, value);
                RefreshToggleButtonSerialIsEnabled();
            }
        }

        #endregion ListComboBoxParitySelectedItem

        #region ListComboBoxDataBits

        private IList<string> _listComboBoxDataBits = new List<string>();

        public IList<string> ListComboBoxDataBits
        {
            get => _listComboBoxDataBits;
            set => _ = SetProperty(ref _listComboBoxDataBits, value);
        }

        #endregion ListComboBoxDataBits

        #region ListComboBoxDataBitsSelectedItem

        private string _listComboBoxDataBitsSelectedItem;

        public string ListComboBoxDataBitsSelectedItem
        {
            get => _listComboBoxDataBitsSelectedItem;
            set
            {
                _ = SetProperty(ref _listComboBoxDataBitsSelectedItem, value);
                RefreshToggleButtonSerialIsEnabled();
            }
        }

        #endregion ListComboBoxDataBitsSelectedItem

        #region ListComboBoxStopBits

        private IList<string> _listComboBoxStopBits = new List<string>();

        public IList<string> ListComboBoxStopBits
        {
            get => _listComboBoxStopBits;
            set => _ = SetProperty(ref _listComboBoxStopBits, value);
        }

        #endregion ListComboBoxStopBits

        #region ListComboBoxStopBitsSelectedItem

        private string _listComboBoxStopBitsSelectedItem;

        public string ListComboBoxStopBitsSelectedItem
        {
            get => _listComboBoxStopBitsSelectedItem;
            set
            {
                _ = SetProperty(ref _listComboBoxStopBitsSelectedItem, value);
                RefreshToggleButtonSerialIsEnabled();
            }
        }

        #endregion ListComboBoxStopBitsSelectedItem

        #region ListComboBoxHandshake

        private IList<string> _listComboBoxHandshake = new List<string>();

        public IList<string> ListComboBoxHandshake
        {
            get => _listComboBoxHandshake;
            set => _ = SetProperty(ref _listComboBoxHandshake, value);
        }

        #endregion ListComboBoxHandshake

        #region ListComboBoxHandshakeSelectedItem

        private string _listComboBoxHandshakeSelectedItem;

        public string ListComboBoxHandshakeSelectedItem
        {
            get => _listComboBoxHandshakeSelectedItem;
            set
            {
                _ = SetProperty(ref _listComboBoxHandshakeSelectedItem, value);
                RefreshToggleButtonSerialIsEnabled();
            }
        }

        #endregion ListComboBoxHandshakeSelectedItem

        #region SliderFlushTimeValue

        private int _sliderFlushTimeValue;

        public int SliderFlushTimeValue
        {
            get => _sliderFlushTimeValue;
            set => _ = SetProperty(ref _sliderFlushTimeValue, value);
        }

        #endregion SliderFlushTimeValue

        #region ToggleButtonSerialIsChecked

        private bool _toggleButtonSerialIsChecked;

        public bool ToggleButtonSerialIsChecked
        {
            get => _toggleButtonSerialIsChecked;
            set
            {
                if (value)
                {
                    OpenSerial();
                }
                else
                {
                    CloseSerial();
                }
                _ = SetProperty(ref _toggleButtonSerialIsChecked, _serial.Connected());
            }
        }

        #endregion ToggleButtonSerialIsChecked

        #region ToggleButtonSerialIsEnabled

        private bool _toggleButtonSerialIsEnabled;

        public bool ToggleButtonSerialIsEnabled
        {
            get => _toggleButtonSerialIsEnabled;
            set => _ = SetProperty(ref _toggleButtonSerialIsEnabled, value);
        }

        #endregion ToggleButtonSerialIsEnabled

        #region CheckBoxIsSaveDataIsChecked

        private bool _checkBoxIsSaveDataIsChecked;

        public bool CheckBoxIsSaveDataIsChecked
        {
            get => _checkBoxIsSaveDataIsChecked;
            set => _ = SetProperty(ref _checkBoxIsSaveDataIsChecked, value);
        }

        #endregion CheckBoxIsSaveDataIsChecked

        #region CheckBoxIsExistPasswordIsChecked

        private bool _checkBoxIsExistPasswordIsChecked;

        public bool CheckBoxIsExistPasswordIsChecked
        {
            get => _checkBoxIsExistPasswordIsChecked;
            set => _ = SetProperty(ref _checkBoxIsExistPasswordIsChecked, value);
        }

        #endregion CheckBoxIsExistPasswordIsChecked

        #region TextBoxSaveDataPathText

        private string _textBoxSaveDataPathText;

        public string TextBoxSaveDataPathText
        {
            get => _textBoxSaveDataPathText;
            set => _ = SetProperty(ref _textBoxSaveDataPathText, value);
        }

        #endregion TextBoxSaveDataPathText

        #region PasswordBoxPasswordPassword

        private string _passwordBoxPasswordPassword;

        public string PasswordBoxPasswordPassword
        {
            get => _passwordBoxPasswordPassword;
            set => _ = SetProperty(ref _passwordBoxPasswordPassword, value);
        }

        #endregion PasswordBoxPasswordPassword

        #endregion 数据绑定

        #region 按钮绑定

        #region ButtonPortCommand

        private DelegateCommand _buttonPortCommand;

        public DelegateCommand ButtonPortCommand =>
            _buttonPortCommand ??= new DelegateCommand(ExecuteButtonPortCommand);

        private void ExecuteButtonPortCommand()
        {
            _serial.FlushPorts();
        }

        #endregion ButtonPortCommand

        private DelegateCommand _buttonSaveDataPathChooseCommand;

        public DelegateCommand ButtonSaveDataPathChooseCommand =>
            _buttonSaveDataPathChooseCommand ??= new DelegateCommand(ExecuteButtonSaveDataPathChooseCommand);

        private void ExecuteButtonSaveDataPathChooseCommand()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件|*.txt";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                TextBoxSaveDataPathText = saveFileDialog.FileName;
            }
        }

        #endregion 按钮绑定

        #endregion 绑定
    }
}