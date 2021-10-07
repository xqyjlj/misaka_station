using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskDeviceViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
        public TaskDeviceViewModel()
        {
        }

        private ObservableCollection<ModelDevice> _DataGridItems = new();

        public ObservableCollection<ModelDevice> DataGridItems
        {
            get => _DataGridItems;
            set => _ = SetProperty(ref _DataGridItems, value);
        }
    }
}
