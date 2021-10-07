using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelMsgQueue : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _entry;

        public string Entry
        {
            get => _entry;
            set => _ = SetProperty(ref _entry, value);
        }

        private string _suspend;

        public string Suspend
        {
            get => _suspend;
            set => _ = SetProperty(ref _suspend, value);
        }
    }
}