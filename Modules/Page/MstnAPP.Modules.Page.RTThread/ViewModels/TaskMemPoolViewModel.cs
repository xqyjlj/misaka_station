using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskMemPoolViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
        public TaskMemPoolViewModel()
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