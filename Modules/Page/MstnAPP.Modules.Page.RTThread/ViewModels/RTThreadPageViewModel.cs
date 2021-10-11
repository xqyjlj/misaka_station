using MstnAPP.Modules.Page.RTThread.Event;
using Prism.Events;
using Prism.Mvvm;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class RTThreadPageViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;//事件聚合器

        public RTThreadPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        #region 绑定

        #region TabControlSelectedIndex

        private int _tabControlSelectedIndex;

        public int TabControlSelectedIndex
        {
            get => _tabControlSelectedIndex;
            set
            {
                _ = SetProperty(ref _tabControlSelectedIndex, value);
                _eventAggregator.GetEvent<EventTask>().Publish(_tabControlSelectedIndex == 1);
            }
        }

        #endregion TabControlSelectedIndex

        #endregion 绑定
    }
}