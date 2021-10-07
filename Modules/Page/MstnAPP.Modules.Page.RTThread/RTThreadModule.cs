using MstnAPP.Modules.Page.RTThread.Views;
using MstnAPP.Services.Sys.Debug;
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
            _ = regionManager.RegisterViewWithRegion("RTThreadPageSettingContentRegion", typeof(Setting));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskContentRegion", typeof(Task));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskThreadContentRegion", typeof(TaskThread));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskMemPoolContentRegion", typeof(TaskMemPool));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskMemHeapContentRegion", typeof(TaskMemHeap));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskFreeContentRegion", typeof(TaskFree));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskSemContentRegion", typeof(TaskSem));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskMutexContentRegion", typeof(TaskMutex));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskEventContentRegion", typeof(TaskEvent));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskMailboxContentRegion", typeof(TaskMailbox));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskMsgQueueContentRegion", typeof(TaskMsgQueue));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskDeviceContentRegion", typeof(TaskDevice));
            _ = regionManager.RegisterViewWithRegion("RTThreadPageTaskTimerContentRegion", typeof(TaskTimer));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RTThreadPage>("RTThreadPage");
        }
    }
}