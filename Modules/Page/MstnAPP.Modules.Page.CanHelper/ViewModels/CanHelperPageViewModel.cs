using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace MstnAPP.Modules.Page.CanHelper.ViewModels
{
    public class CanHelperPageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        public bool KeepAlive => false; //是否保存缓存

        /// <summary>
        /// 导航到此窗口前触发的回调函数
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// 是否创建新示例。
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        /// <returns>为true，表示不创建新示例，页面还是之前的；为false，则创建新的页面。</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        /// <summary>
        /// 导航离开此窗口前触发的回调函数
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private readonly IDialogService _dialogService;

        public CanHelperPageViewModel(IDialogService dialog)
        {
            _dialogService = dialog;
        }

        #region ButtonReadMonitorCommand

        private DelegateCommand _buttonReadMonitorCommand;

        public DelegateCommand ButtonReadMonitorCommand =>
            _buttonReadMonitorCommand ??= new DelegateCommand(ExecuteButtonReadMonitorCommand);

        private void ExecuteButtonReadMonitorCommand()
        {
            _dialogService.Show("CanHelper.ReadDialog");
        }

        #endregion ButtonReadMonitorCommand

        #region ButtonFrameWriteCommand

        private DelegateCommand _buttonFrameWriteCommand;

        public DelegateCommand ButtonFrameWriteCommand =>
            _buttonFrameWriteCommand ??= new DelegateCommand(ExecuteButtonFrameWriteCommand);

        private void ExecuteButtonFrameWriteCommand()
        {
            _dialogService.Show("CanHelper.FrameWriteDialog");
        }

        #endregion ButtonFrameWriteCommand

        #region ButtonFileWriteCommand

        private DelegateCommand _buttonFileWriteCommand;

        public DelegateCommand ButtonFileWriteCommand =>
            _buttonFileWriteCommand ??= new DelegateCommand(ExecuteButtonFileWriteCommand);

        private void ExecuteButtonFileWriteCommand()
        {
            _dialogService.Show("CanHelper.FileWriteDialog");
        }

        #endregion ButtonFileWriteCommand
    }
}