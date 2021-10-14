﻿using MstnAPP.Modules.Dialog.Feedback;
using MstnAPP.Modules.Dialog.QQ;
using MstnAPP.Modules.Dialog.WeChat;
using MstnAPP.Modules.Page.Home;
using MstnAPP.Modules.Page.RTThread;
using MstnAPP.Services.Driver;
using MstnAPP.Services.Sys.IniFile;
using MstnAPP.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using MstnAPP.Services.Driver.Serial;

namespace MstnAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
#if DEBUG

#else
            System.Diagnostics.Trace.Listeners.Clear();
            System.Diagnostics.Trace.Listeners.Add(new Services.Sys.Debug.LogListener());
#endif
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _ = containerRegistry.RegisterSingleton<IIniFile, IniFile>();
            _ = containerRegistry.RegisterSingleton<ISerial, Serial>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            _ = moduleCatalog.AddModule<HomeModule>();
            _ = moduleCatalog.AddModule<FeedbackModule>();
            _ = moduleCatalog.AddModule<WeChatModule>();
            _ = moduleCatalog.AddModule<QQModule>();
            _ = moduleCatalog.AddModule<RTThreadModule>();
        }
    }
}