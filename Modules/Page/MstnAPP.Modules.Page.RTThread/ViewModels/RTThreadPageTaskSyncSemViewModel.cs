using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class RTThreadPageTaskSyncSemViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
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

        public RTThreadPageTaskSyncSemViewModel()
        {

        }

        private ObservableCollection<ModelSyncSem> _DataGridItems = new();

        public ObservableCollection<ModelSyncSem> DataGridItems
        {
            get => _DataGridItems;
            set => _ = SetProperty(ref _DataGridItems, value);
        }
    }
}