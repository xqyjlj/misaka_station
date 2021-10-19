using MstnAPP.Services.Driver.ICanBus;
using MstnAPP.Services.Sys.Debug;
using MstnAPP.Services.Sys.Util;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Globalization;

namespace MstnAPP.Modules.Page.CanHelper.Dialog.ViewModels
{
    public class FrameWriteDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = "Can单帧发送窗口";

        public string Title
        {
            get => _title;
            set => _ = SetProperty(ref _title, value);
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        private readonly ICan _can;

        public FrameWriteDialogViewModel(ICan can)
        {
            _can = can;
        }

        private void WriteCan()
        {
            var msg = new[] { _byte0, _byte1, _byte2, _byte3, _byte4, _byte5, _byte6, _byte7 };
            _can.Write(msg, _id, _dlc, CheckBoxIsExtIdIsChecked ? CanBusEnum.Ext : CanBusEnum.Std);
        }

        #region 数据绑定

        #region TextBoxId

        private int _id;
        private string _textBoxId = "0";

        public string TextBoxId
        {
            get => _textBoxId;
            set
            {
                _id = Convert.ToInt32(Str.ToUInt(value));
                if (_id == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                if (_checkBoxIsExtIdIsChecked)
                {
                    if (_id > 0x1FFFFFFF)
                    {
                        _id = 0;
                        LogBox.W("请输入合法数据");
                        value = "0";
                    }
                }
                else
                {
                    if (_id > 0x7FF)
                    {
                        _id = 0;
                        LogBox.W("请输入合法数据");
                        value = "0";
                    }
                }

                _ = SetProperty(ref _textBoxId, value);
            }
        }

        #endregion TextBoxId

        #region TextBoxDlc

        private byte _dlc;
        private string _textBoxDlc = "0";

        public string TextBoxDlc
        {
            get => _textBoxDlc;
            set
            {
                _dlc = Str.ToByte(value);
                switch (_dlc)
                {
                    case 0 when value.ToLower(new CultureInfo("zh-CN", false)) != "0x":
                        value = "0";
                        break;

                    case > 8:
                        LogBox.W("请输入合法数据");
                        value = "0";
                        break;
                }

                _ = SetProperty(ref _textBoxDlc, value);
            }
        }

        #endregion TextBoxDlc

        #region TextBoxByte0

        private byte _byte0;
        private string _textBoxByte0 = "0";

        public string TextBoxByte0
        {
            get => _textBoxByte0;
            set
            {
                _byte0 = Str.ToByte(value);
                if (_byte0 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte0, value);
            }
        }

        #endregion TextBoxByte0

        #region TextBoxByte1

        private byte _byte1;
        private string _textBoxByte1 = "0";

        public string TextBoxByte1
        {
            get => _textBoxByte1;
            set
            {
                _byte1 = Str.ToByte(value);
                if (_byte1 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte1, value);
            }
        }

        #endregion TextBoxByte1

        #region TextBoxByte2

        private byte _byte2;
        private string _textBoxByte2 = "0";

        public string TextBoxByte2
        {
            get => _textBoxByte2;
            set
            {
                _byte2 = Str.ToByte(value);
                if (_byte2 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte2, value);
            }
        }

        #endregion TextBoxByte2

        #region TextBoxByte3

        private byte _byte3;
        private string _textBoxByte3 = "0";

        public string TextBoxByte3
        {
            get => _textBoxByte3;
            set
            {
                _byte3 = Str.ToByte(value);
                if (_byte3 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte3, value);
            }
        }

        #endregion TextBoxByte3

        #region TextBoxByte4

        private byte _byte4;
        private string _textBoxByte4 = "0";

        public string TextBoxByte4
        {
            get => _textBoxByte4;
            set
            {
                _byte4 = Str.ToByte(value);
                if (_byte4 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte4, value);
            }
        }

        #endregion TextBoxByte4

        #region TextBoxByte5

        private byte _byte5;
        private string _textBoxByte5 = "0";

        public string TextBoxByte5
        {
            get => _textBoxByte5;
            set
            {
                _byte5 = Str.ToByte(value);
                if (_byte5 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte5, value);
            }
        }

        #endregion TextBoxByte5

        #region TextBoxByte6

        private byte _byte6;
        private string _textBoxByte6 = "0";

        public string TextBoxByte6
        {
            get => _textBoxByte6;
            set
            {
                _byte6 = Str.ToByte(value);
                if (_byte6 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte6, value);
            }
        }

        #endregion TextBoxByte6

        #region TextBoxByte7

        private byte _byte7;
        private string _textBoxByte7 = "0";

        public string TextBoxByte7
        {
            get => _textBoxByte7;
            set
            {
                _byte7 = Str.ToByte(value);
                if (_byte7 == 0 && value.ToLower(new CultureInfo("zh-CN", false)) != "0x")
                {
                    value = "0";
                }
                _ = SetProperty(ref _textBoxByte7, value);
            }
        }

        #endregion TextBoxByte7

        #region CheckBoxIsExtIdIsChecked

        private bool _checkBoxIsExtIdIsChecked;

        public bool CheckBoxIsExtIdIsChecked
        {
            get => _checkBoxIsExtIdIsChecked;
            set => SetProperty(ref _checkBoxIsExtIdIsChecked, value);
        }

        #endregion CheckBoxIsExtIdIsChecked

        #endregion 数据绑定

        #region 按钮绑定

        #region ButtonWriteCommand

        private DelegateCommand _buttonWriteCommand;

        public DelegateCommand ButtonWriteCommand =>
            _buttonWriteCommand ??= new DelegateCommand(ExecuteButtonWriteCommand);

        private void ExecuteButtonWriteCommand()
        {
            WriteCan();
        }

        #endregion ButtonWriteCommand

        #region ButtonClearCommand

        private DelegateCommand _buttonClearCommand;

        public DelegateCommand ButtonClearCommand =>
            _buttonClearCommand ??= new DelegateCommand(ExecuteButtonClearCommand);

        private void ExecuteButtonClearCommand()
        {
            TextBoxId = "0";
            TextBoxDlc = "0";
            TextBoxByte0 = "0";
            TextBoxByte1 = "0";
            TextBoxByte2 = "0";
            TextBoxByte3 = "0";
            TextBoxByte4 = "0";
            TextBoxByte5 = "0";
            TextBoxByte6 = "0";
            TextBoxByte7 = "0";
        }

        #endregion ButtonClearCommand

        #endregion 按钮绑定
    }
}