using MstnAPP.Modules.Page.Home.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MstnAPP.Modules.Page.Home
{
    public class HomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomePage>("Home.Page");
        }
    }
}