using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelMutex : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _owner;

        public string Owner
        {
            get => _owner;
            set => _ = SetProperty(ref _owner, value);
        }

        private string _hold;

        public string Hold
        {
            get => _hold;
            set => _ = SetProperty(ref _hold, value);
        }

        private string _suspend;

        public string Suspend
        {
            get => _suspend;
            set => _ = SetProperty(ref _suspend, value);
        }
    }
}