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
            var regionManager = containerProvider.Resolve<IRegionManager>();
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Setting.ContentRegion", typeof(Setting));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.ContentRegion", typeof(Task));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Thread.ContentRegion", typeof(TaskThread));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.MemPool.ContentRegion", typeof(TaskMemPool));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.MemHeap.ContentRegion", typeof(TaskMemHeap));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Free.ContentRegion", typeof(TaskFree));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Sem.ContentRegion", typeof(TaskSem));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Mutex.ContentRegion", typeof(TaskMutex));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Event.ContentRegion", typeof(TaskEvent));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Mailbox.ContentRegion", typeof(TaskMailbox));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.MsgQueue.ContentRegion", typeof(TaskMsgQueue));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Device.ContentRegion", typeof(TaskDevice));
            _ = regionManager.RegisterViewWithRegion("RTThread.Page.Task.Timer.ContentRegion", typeof(TaskTimer));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RTThreadPage>("RTThread.Page");
        }
    }
}