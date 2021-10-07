using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelSem : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _value;

        public string Value
        {
            get => _value;
            set => _ = SetProperty(ref _value, value);
        }

        private string _suspend;

        public string Suspend
        {
            get => _suspend;
            set => _ = SetProperty(ref _suspend, value);
        }
    }
}