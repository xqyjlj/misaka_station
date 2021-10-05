using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.Models
{
    public class ModelMemoryHeap : BindableBase
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

        private string _maxUsedSize;

        public string MaxUsedSize
        {
            get => _maxUsedSize;
            set => _ = SetProperty(ref _maxUsedSize, value);
        }

        private string _availableSize;

        public string AvailableSize
        {
            get => _availableSize;
            set => _ = SetProperty(ref _availableSize, value);
        }
    }
}