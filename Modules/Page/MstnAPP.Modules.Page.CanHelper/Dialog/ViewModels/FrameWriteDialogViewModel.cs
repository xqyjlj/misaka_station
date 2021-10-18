using MstnAPP.Services.Driver.ICanBus;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

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
    }
}