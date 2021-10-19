using MstnApp.Event.Core;
using MstnAPP.Services.Driver.ICanBus;
using MstnAPP.Services.Sys.IniFile;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace MstnAPP.Modules.Page.CanHelper.ViewModels
{
    public class SettingViewModel : BindableBase
    {
        private readonly ICan _can; //Can接口对象

        private readonly IIniFile _iniFile; //INI文件对象

        public SettingViewModel(ICan can, IIniFile iniFile, IEventAggregator eventAggregator)
        {
            _can = can;
            _iniFile = iniFile;
            _ = eventAggregator.GetEvent<EventClose>().Subscribe(EventCloseReceived);

            _can.PortNameChanged += CanPortNameChanged;

            InitListComboBox();
        }

        /// <summary>
        /// 窗口关闭事件回调函数
        /// </summary>
        /// <param name="message"></param>
        private void EventCloseReceived(string message)
        {
            _can.Destroy();
        }

        /// <summary>
        /// 初始化下拉框列表
        /// </summary>
        private void InitListComboBox()
        {
            ListComboBoxPort = _can.GetPortNames();

            foreach (var item in GenerateBaudRateItems())
            {
                ListComboBoxBaudRate.Add(item);
            }
        }

        /// <summary>
        /// 刷新Can接口按钮状态
        /// </summary>
        private void RefreshToggleButtonCanIsEnabled()
        {
            ToggleButtonCanIsEnabled = _listComboBoxPortSelectedItem != null
                                       && _listComboBoxBaudRateSelectedItem != null;
        }

        private bool _isHasBeenSetSerial;//是否已经过设置Can接口

        /// <summary>
        /// 打开Can接口
        /// </summary>
        private void OpenCan()
        {
            if (_isHasBeenSetSerial)
            {
                if (_can.Connected)
                {
                    _can.Close();
                }
            }
            _isHasBeenSetSerial = true;
            _ = _can.Open(ListComboBoxPortSelectedItem, ListComboBoxBaudRateSelectedItem);
        }

        /// <summary>
        /// 关闭Can接口
        /// </summary>
        private void CloseCan()
        {
            _can.Close();
        }

        /// <summary>
        /// Can接口名改变事件回调函数
        /// </summary>
        /// <param name="portNames">Can接口名列表</param>
        private void CanPortNameChanged(List<string> portNames)
        {
            ListComboBoxPort = portNames;
        }

        #region 创建item

        private static IEnumerable<string> GenerateBaudRateItems()
        {
            yield return "10K";
            yield return "50K";
            yield return "62K";
            yield return "83K";
            yield return "100K";
            yield return "125K";
            yield return "250K";
            yield return "500K";
            yield return "1000K";
        }

        #endregion 创建item

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
                RefreshToggleButtonCanIsEnabled();
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
                RefreshToggleButtonCanIsEnabled();
            }
        }

        #endregion ListComboBoxBaudRateSelectedItem

        #region ToggleButtonCanIsChecked

        private bool _toggleButtonCanIsChecked;

        public bool ToggleButtonCanIsChecked
        {
            get => _toggleButtonCanIsChecked;
            set
            {
                if (value)
                {
                    OpenCan();
                }
                else
                {
                    CloseCan();
                }
                _ = SetProperty(ref _toggleButtonCanIsChecked, _can.Connected);
            }
        }

        #endregion ToggleButtonCanIsChecked

        #region ToggleButtonCanIsEnabled

        private bool _toggleButtonCanIsEnabled;

        public bool ToggleButtonCanIsEnabled
        {
            get => _toggleButtonCanIsEnabled;
            set => _ = SetProperty(ref _toggleButtonCanIsEnabled, value);
        }

        #endregion ToggleButtonCanIsEnabled

        #endregion 数据绑定

        #region 按钮绑定

        #region ButtonPortCommand

        private DelegateCommand _buttonPortCommand;

        public DelegateCommand ButtonPortCommand =>
            _buttonPortCommand ??= new DelegateCommand(ExecuteButtonPortCommand);

        private void ExecuteButtonPortCommand()
        {
            _can.FlushPorts();
        }

        #endregion ButtonPortCommand

        #endregion 按钮绑定
    }
}