using MstnAPP.Modules.Page.RTThread.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    class RTThreadPageTaskViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
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

        public RTThreadPageTaskViewModel(IRegionManager regionManager)
        {
            
        }
    }
}