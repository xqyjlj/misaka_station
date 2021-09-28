using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace MstnAPP.Modules.Page.Home.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;

        public bool KeepAlive => false;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public HomePageViewModel(IRegionManager region, IDialogService dialog)
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

        private DelegateCommand _buttonDonateCommand;

        public DelegateCommand ButtonDonateCommand =>
            _buttonDonateCommand ??= new DelegateCommand(ExecuteButtonDonateCommand);

        private void ExecuteButtonDonateCommand()
        {
            Services.Sys.Process.StartProcess.OpenDonate();
        }
    }
}