using Prism.Mvvm;
using Prism.Regions;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
        public TaskViewModel()
        {
        }
    }
}