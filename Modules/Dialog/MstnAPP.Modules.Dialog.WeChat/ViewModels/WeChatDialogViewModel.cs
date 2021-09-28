using Prism.Services.Dialogs;
using System;

namespace MstnAPP.Modules.Dialog.WeChat.ViewModels
{
    public class WeChatDialogViewModel : IDialogAware
    {
        private string _title = "微信";

        public string Title
        {
            get => _title;
            set => _title = Title;
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