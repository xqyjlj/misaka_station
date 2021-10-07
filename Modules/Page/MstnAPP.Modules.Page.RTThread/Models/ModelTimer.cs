using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelTimer : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _periodic;

        public string Periodic
        {
            get => _periodic;
            set => _ = SetProperty(ref _periodic, value);
        }

        private string _timeout;

        public string Timeout
        {
            get => _timeout;
            set => _ = SetProperty(ref _timeout, value);
        }

        private string _flag;

        public string Flag
        {
            get => _flag;
            set => _ = SetProperty(ref _flag, value);
        }
    }
}
