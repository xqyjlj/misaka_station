using MstnAPP.Modules.Page.RTThread.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MstnAPP.Modules.Page.RTThread
{
    public class RTThreadModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
            _ = regionManager.RegisterViewWithRegion("RTThreadPageSettingContentRegion", typeof(RTThreadPageSetting));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskContentRegion", typeof(RTThreadPageTask));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskThreadContentRegion", typeof(RTThreadPageTaskThread));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerformanceContentRegion", typeof(RTThreadPageTaskPerformance));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerformanceMemoryPoolContentRegion", typeof(RTThreadPageTaskPerformanceMemoryPool));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerformanceMemoryHeapContentRegion", typeof(RTThreadPageTaskPerformanceMemoryHeap));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerformanceMemoryFreeContentRegion", typeof(RTThreadPageTaskPerformanceMemoryFree));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RTThreadPage>("RTThreadPage");
        }
    }
}