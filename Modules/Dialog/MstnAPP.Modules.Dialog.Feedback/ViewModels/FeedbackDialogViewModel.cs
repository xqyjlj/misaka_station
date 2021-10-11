using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace MstnAPP.Modules.Dialog.Feedback.ViewModels
{
    public class FeedbackDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IDialogService _dialogService;
        private string _title = "发送反馈";

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

        public FeedbackDialogViewModel(IDialogService dialog)
        {
            _dialogService = dialog;
        }

        private DelegateCommand _buttonWeChatCommand;

        public DelegateCommand ButtonWeChatCommand =>
            _buttonWeChatCommand ??= new DelegateCommand(ExecuteButtonWeChatCommand);

        private void ExecuteButtonWeChatCommand()
        {
            _dialogService.ShowDialog("WeChatDialog");
        }

        private DelegateCommand _buttonQQCommand;

        public DelegateCommand ButtonQQCommand =>
            _buttonQQCommand ??= new DelegateCommand(ExecuteButtonQQCommand);

        private void ExecuteButtonQQCommand()
        {
            _dialogService.ShowDialog("QQDialog");
        }

        private DelegateCommand _buttonGithubCommand;

        public DelegateCommand ButtonGithubCommand =>
            _buttonGithubCommand ??= new DelegateCommand(ExecuteButtonGithubCommand);

        private static void ExecuteButtonGithubCommand()
        {
            Services.Sys.Process.StartProcess.OpenGithub();
        }

        private DelegateCommand _buttonEmailCommand;

        public DelegateCommand ButtonEmailCommand =>
            _buttonEmailCommand ??= new DelegateCommand(ExecuteButtonEmailCommand);

        private static void ExecuteButtonEmailCommand()
        {
            Services.Sys.Process.StartProcess.OpenEmail();
        }

        private DelegateCommand _buttonBlogCommand;

        public DelegateCommand ButtonBlogCommand =>
            _buttonBlogCommand ??= new DelegateCommand(ExecuteButtonBlogCommand);

        private static void ExecuteButtonBlogCommand()
        {
            Services.Sys.Process.StartProcess.OpenBlog();
        }
    }
}