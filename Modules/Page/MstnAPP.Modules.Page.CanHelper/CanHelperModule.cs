using MstnAPP.Modules.Page.CanHelper.Dialog.ViewModels;
using MstnAPP.Modules.Page.CanHelper.Dialog.Views;
using MstnAPP.Modules.Page.CanHelper.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MstnAPP.Modules.Page.CanHelper
{
    public class CanHelperModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            _ = regionManager.RegisterViewWithRegion("CanHelper.Page.Setting.ContentRegion", typeof(Setting));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CanHelperPage>("CanHelper.Page");
            containerRegistry.RegisterDialog<ReadDialog, ReadDialogViewModel>("CanHelper.ReadDialog");
            containerRegistry.RegisterDialog<FrameWriteDialog, FrameWriteDialogViewModel>("CanHelper.FrameWriteDialog");
            containerRegistry.RegisterDialog<FileWriteDialog, FileWriteDialogViewModel>("CanHelper.FileWriteDialog");
        }
    }
}