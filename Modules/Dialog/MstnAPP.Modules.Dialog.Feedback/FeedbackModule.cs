using MstnAPP.Modules.Dialog.Feedback.ViewModels;
using MstnAPP.Modules.Dialog.Feedback.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MstnAPP.Modules.Dialog.Feedback
{
    public class FeedbackModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<FeedbackDialog, FeedbackDialogViewModel>("FeedbackDialog");
        }
    }
}