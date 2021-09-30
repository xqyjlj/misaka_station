using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelThread : BindableBase
    {
        private string _thread;

        public string Thread
        {
            get => _thread;
            set => _ = SetProperty(ref _thread, value);
        }

        private string _pri;

        public string Pri
        {
            get => _pri;
            set => _ = SetProperty(ref _pri, value);
        }

        private string _status;

        public string Status
        {
            get => _status;
            set => _ = SetProperty(ref _status, value);
        }

        private string _sp;

        public string Sp
        {
            get => _sp;
            set => _ = SetProperty(ref _sp, value);
        }

        private string _stackSize;

        public string StackSize
        {
            get => _stackSize;
            set => _ = SetProperty(ref _stackSize, value);
        }

        private string _maxUsed;

        public string MaxUsed
        {
            get => _maxUsed;
            set => _ = SetProperty(ref _maxUsed, value);
        }

        private string _leftTick;

        public string LeftTick
        {
            get => _leftTick;
            set => _ = SetProperty(ref _leftTick, value);
        }

        private string _error;

        public string Error
        {
            get => _error;
            set => _ = SetProperty(ref _error, value);
        }
    }
}