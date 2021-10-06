using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    internal class RTThreadPageTaskPerformanceMemPoolViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
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

        public RTThreadPageTaskPerformanceMemPoolViewModel()
        {
        }

        private ObservableCollection<ModelMemPool> _DataGridItems = new();

        public ObservableCollection<ModelMemPool> DataGridItems
        {
            get => _DataGridItems;
            set => _ = SetProperty(ref _DataGridItems, value);
        }
    }
}