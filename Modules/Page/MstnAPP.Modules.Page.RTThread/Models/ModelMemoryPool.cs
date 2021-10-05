using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelMemoryPool : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _size;

        public string Size
        {
            get => _size;
            set => _ = SetProperty(ref _size, value);
        }

        private string _total;

        public string Total
        {
            get => _total;
            set => _ = SetProperty(ref _total, value);
        }

        private string _free;

        public string Free
        {
            get => _free;
            set => _ = SetProperty(ref _free, value);
        }

        private string _suspendThread;

        public string SuspendThread
        {
            get => _suspendThread;
            set => _ = SetProperty(ref _suspendThread, value);
        }
    }
}