using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelDevice : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _type;

        public string Type
        {
            get => _type;
            set => _ = SetProperty(ref _type, value);
        }

        private string _refCount;

        public string RefCount
        {
            get => _refCount;
            set => _ = SetProperty(ref _refCount, value);
        }
    }
}