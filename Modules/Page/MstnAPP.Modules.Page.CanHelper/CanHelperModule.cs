using MstnAPP.Modules.Page.CanHelper.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MstnAPP.Modules.Page.CanHelper
{
    public class CanHelperModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CanHelperPage>("CanHelperPage");
        }
    }
}