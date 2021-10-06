using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class RTThreadPageTaskSyncMutexViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
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

        public RTThreadPageTaskSyncMutexViewModel()
        {
        }

        private ObservableCollection<ModelSyncMutex> _dataGridTaskItems = new();

        public ObservableCollection<ModelSyncMutex> DataGridTaskItems
        {
            get => _dataGridTaskItems;
            set => _ = SetProperty(ref _dataGridTaskItems, value);
        }
    }
}