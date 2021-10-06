using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelSyncEvent : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _current;

        public string Current
        {
            get => _current;
            set => _ = SetProperty(ref _current, value);
        }

        private string _suspend;

        public string Suspend
        {
            get => _suspend;
            set => _ = SetProperty(ref _suspend, value);
        }
    }
}