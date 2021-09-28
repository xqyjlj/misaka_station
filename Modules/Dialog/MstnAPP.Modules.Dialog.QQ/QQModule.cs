using MstnAPP.Modules.Dialog.QQ.ViewModels;
using MstnAPP.Modules.Dialog.QQ.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MstnAPP.Modules.Dialog.QQ
{
    public class QQModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<QQDialog, QQDialogViewModel>("QQDialog");
        }
    }
}