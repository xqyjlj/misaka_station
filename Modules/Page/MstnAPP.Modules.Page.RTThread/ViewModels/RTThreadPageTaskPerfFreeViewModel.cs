using Prism.Mvvm;
using Prism.Regions;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class RTThreadPageTaskPerfFreeViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public RTThreadPageTaskPerfFreeViewModel()
        {
        }

        private string _textBlockTotalText;

        public string TextBlockTotalText
        {
            get => _textBlockTotalText;
            set => _ = SetProperty(ref _textBlockTotalText, value);
        }

        private string _textBlockUsedText;

        public string TextBlockUsedText
        {
            get => _textBlockUsedText;
            set => _ = SetProperty(ref _textBlockUsedText, value);
        }

        private string _textBlockRemainderText;

        public string TextBlockRemainderText
        {
            get => _textBlockRemainderText;
            set => _ = SetProperty(ref _textBlockRemainderText, value);
        }

        private string _textBlockDegreeText;

        public string TextBlockDegreeText
        {
            get => _textBlockDegreeText;
            set => _ = SetProperty(ref _textBlockDegreeText, value);
        }
    }
}