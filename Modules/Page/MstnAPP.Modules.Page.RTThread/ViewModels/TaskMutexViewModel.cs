using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskMutexViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
        public TaskMutexViewModel()
        {
        }

        private ObservableCollection<ModelMutex> _DataGridItems = new();

        public ObservableCollection<ModelMutex> DataGridItems
        {
            get => _DataGridItems;
            set => _ = SetProperty(ref _DataGridItems, value);
        }
    }
}