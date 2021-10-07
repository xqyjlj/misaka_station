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
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerfContentRegion", typeof(RTThreadPageTaskPerf));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerfMemPoolContentRegion", typeof(RTThreadPageTaskPerfMemPool));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerfMemHeapContentRegion", typeof(RTThreadPageTaskPerfMemHeap));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskPerfFreeContentRegion", typeof(RTThreadPageTaskPerfFree));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskSyncContentRegion", typeof(RTThreadPageTaskSync));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskSyncSemContentRegion", typeof(RTThreadPageTaskSyncSem));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskSyncMutexContentRegion", typeof(RTThreadPageTaskSyncMutex));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskSyncEventContentRegion", typeof(RTThreadPageTaskSyncEvent));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RTThreadPage>("RTThreadPage");
        }
    }
}