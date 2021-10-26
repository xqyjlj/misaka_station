using Microsoft.Win32;
using MstnAPP.Services.Driver.CanProtocol;
using MstnAPP.Services.Driver.ICanBus;
using MstnAPP.Services.Sys.Debug;
using MstnAPP.Services.Sys.Util;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MstnAPP.Modules.Page.CanHelper.Dialog.ViewModels
{
    public class FileWriteDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = "Can文件发送窗口";

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

        public FileWriteDialogViewModel(ICan can)
        {
            _can = can;
            InitListComboBox();
        }

        /// <summary>
        /// 初始化下拉框列表
        /// </summary>
        private void InitListComboBox()
        {
            foreach (var item in GenerateProtocolItems())
            {
                ListComboBoxProtocol.Add(item);
            }

            ListComboBoxProtocolSelectedItem = ListComboBoxProtocol[0];
        }

        private static IEnumerable<string> GenerateProtocolItems()
        {
            yield return "SLIP";
        }

        private void WriteSlip()
        {
            var slip = new CanSlip(_can, _id, CheckBoxIsExtIdIsChecked ? CanBusEnum.Ext : CanBusEnum.Std);
            slip.SendFile(TextBoxSaveDataPathText);
        }

        private void RefreshButtonWriteIsEnabled()
        {
            ButtonWriteIsEnabled = TextBoxSaveDataPathText != null;
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

        #region CheckBoxIsExtIdIsChecked

        private bool _checkBoxIsExtIdIsChecked;

        public bool CheckBoxIsExtIdIsChecked
        {
            get => _checkBoxIsExtIdIsChecked;
            set => SetProperty(ref _checkBoxIsExtIdIsChecked, value);
        }

        #endregion CheckBoxIsExtIdIsChecked

        #region ListComboBoxProtocol

        private IList<string> _listComboBoxProtocol = new List<string>();

        public IList<string> ListComboBoxProtocol
        {
            get => _listComboBoxProtocol;
            set => _ = SetProperty(ref _listComboBoxProtocol, value);
        }

        #endregion ListComboBoxProtocol

        #region ListComboBoxProtocolSelectedItem

        private string _listComboBoxProtocolSelectedItem;

        public string ListComboBoxProtocolSelectedItem
        {
            get => _listComboBoxProtocolSelectedItem;
            set => _ = SetProperty(ref _listComboBoxProtocolSelectedItem, value);
        }

        #endregion ListComboBoxProtocolSelectedItem

        #region ButtonWriteIsEnabled

        private bool _buttonWriteIsEnabled;

        public bool ButtonWriteIsEnabled
        {
            get => _buttonWriteIsEnabled;
            set => _ = SetProperty(ref _buttonWriteIsEnabled, value);
        }

        #endregion ButtonWriteIsEnabled

        #region TextBoxSaveDataPathText

        private string _textBoxSaveDataPathText;

        public string TextBoxSaveDataPathText
        {
            get => _textBoxSaveDataPathText;
            set
            {
                _ = SetProperty(ref _textBoxSaveDataPathText, value);
                RefreshButtonWriteIsEnabled();
            }
        }

        #endregion TextBoxSaveDataPathText

        #endregion 数据绑定

        #region 按钮绑定

        #region ButtonWriteCommand

        private DelegateCommand _buttonWriteCommand;

        public DelegateCommand ButtonWriteCommand =>
            _buttonWriteCommand ??= new DelegateCommand(ExecuteButtonWriteCommand);

        private void ExecuteButtonWriteCommand()
        {
            if (ListComboBoxProtocolSelectedItem == "SLIP")
            {
                WriteSlip();
            }
        }

        private DelegateCommand _buttonSaveDataPathChooseCommand;

        public DelegateCommand ButtonSaveDataPathChooseCommand =>
            _buttonSaveDataPathChooseCommand ??= new DelegateCommand(ExecuteButtonSaveDataPathChooseCommand);

        private void ExecuteButtonSaveDataPathChooseCommand()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "任意文件|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                TextBoxSaveDataPathText = openFileDialog.FileName;
            }
        }

        #endregion ButtonWriteCommand

        #endregion 按钮绑定
    }
}