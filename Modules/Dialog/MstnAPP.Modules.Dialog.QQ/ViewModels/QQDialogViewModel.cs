using Prism.Services.Dialogs;
using System;

namespace MstnAPP.Modules.Dialog.QQ.ViewModels
{
    public class QQDialogViewModel : IDialogAware
    {
        private string _title = "QQ";

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