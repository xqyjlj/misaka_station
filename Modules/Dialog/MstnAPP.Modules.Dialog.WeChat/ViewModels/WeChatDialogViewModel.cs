using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace MstnAPP.Modules.Dialog.WeChat.ViewModels
{
    public class WeChatDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = "微信";

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
    }
}