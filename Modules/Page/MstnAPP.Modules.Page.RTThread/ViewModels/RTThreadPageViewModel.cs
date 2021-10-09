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
                if (_tabControlSelectedIndex == 1)
                {
                    _eventAggregator.GetEvent<EventTask>().Publish(true);
                }
                else
                {
                    _eventAggregator.GetEvent<EventTask>().Publish(false);
                }
            }
        }

        #endregion TabControlSelectedIndex

        #endregion 绑定
    }
}