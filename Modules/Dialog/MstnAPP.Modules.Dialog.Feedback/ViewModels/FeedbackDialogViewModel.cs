using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace MstnAPP.Modules.Dialog.Feedback.ViewModels
{
    public class FeedbackDialogViewModel : IDialogAware
    {
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;
        private string _title = "发送反馈";

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

        public FeedbackDialogViewModel(IRegionManager region, IDialogService dialog)
        {
            regionManager = region;
            dialogService = dialog;
        }

        private DelegateCommand _buttonWeChatCommand;

        public DelegateCommand ButtonWeChatCommand =>
            _buttonWeChatCommand ??= new DelegateCommand(ExecuteButtonWeChatCommand);

        private void ExecuteButtonWeChatCommand()
        {
            dialogService.ShowDialog("WeChatDialog");
        }

        private DelegateCommand _buttonQQCommand;

        public DelegateCommand ButtonQQCommand =>
            _buttonQQCommand ??= new DelegateCommand(ExecuteButtonQQCommand);

        private void ExecuteButtonQQCommand()
        {
            dialogService.ShowDialog("QQDialog");
        }

        private DelegateCommand _buttonGithubCommand;

        public DelegateCommand ButtonGithubCommand =>
            _buttonGithubCommand ??= new DelegateCommand(ExecuteButtonGithubCommand);

        private void ExecuteButtonGithubCommand()
        {
            Services.Sys.Process.StartProcess.OpenGithub();
        }

        private DelegateCommand _buttonEmailCommand;

        public DelegateCommand ButtonEmailCommand =>
            _buttonEmailCommand ??= new DelegateCommand(ExecuteButtonEmailCommand);

        private void ExecuteButtonEmailCommand()
        {
            Services.Sys.Process.StartProcess.OpenEmail();
        }

        private DelegateCommand _buttonBlogCommand;

        public DelegateCommand ButtonBlogCommand =>
            _buttonBlogCommand ??= new DelegateCommand(ExecuteButtonBlogCommand);

        private void ExecuteButtonBlogCommand()
        {
            Services.Sys.Process.StartProcess.OpenBlog();
        }
    }
}