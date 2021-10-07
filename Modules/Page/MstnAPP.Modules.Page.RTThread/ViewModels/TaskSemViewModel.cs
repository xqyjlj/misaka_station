using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskSemViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
        public TaskSemViewModel()
        {

        }

        private ObservableCollection<ModelSem> _DataGridItems = new();

        public ObservableCollection<ModelSem> DataGridItems
        {
            get => _DataGridItems;
            set => _ = SetProperty(ref _DataGridItems, value);
        }
    }
}