using MstnAPP.Modules.Dialog.WeChat.ViewModels;
using MstnAPP.Modules.Dialog.WeChat.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MstnAPP.Modules.Dialog.WeChat
{
    public class WeChatModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<WeChatDialog, WeChatDialogViewModel>("WeChatDialog");
        }
    }
}