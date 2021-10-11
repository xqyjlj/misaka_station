using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace MstnAPP.Modules.Dialog.QQ.ViewModels
{
    public class QQDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = "QQ";

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