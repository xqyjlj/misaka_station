using MaterialDesignThemes.Wpf;
using MstnApp.Event.Core;
using MstnAPP.Models;
using MstnAPP.Modules.Page.Home.Views;
using MstnAPP.Modules.Page.RTThread.Views;
using MstnAPP.Services.Sys.DataFlie;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace MstnAPP.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IIniFile _iniFile;

        public MainWindowViewModel(IRegionManager region, IDialogService dialog, IIniFile iniFile, IEventAggregator eventAggregator)
        {
            _regionManager = region;
            _dialogService = dialog;
            _eventAggregator = eventAggregator;
            _iniFile = iniFile;

            foreach (ModelItem item in GenerateModelItems())
            {
                ListBoxModelItems.Add(item);
            }

            _listBoxModelItemsView = CollectionViewSource.GetDefaultView(ListBoxModelItems);
            _listBoxModelItemsView.Filter = ModelItemsFilter;

            int index = _iniFile.GetMianWindowFunctionListIndex();
            if (index < ListBoxModelItems.Count && index >= 0)
            {
                _ = _regionManager.RegisterViewWithRegion("MainContentRegion", ListBoxModelItems[index].ViewType);
            }
            else
            {
                _ = _regionManager.RegisterViewWithRegion("MainContentRegion", ListBoxModelItems[0].ViewType);
            }
        }

        private static void ModifyTheme(bool isDarkTheme)
        {
            PaletteHelper paletteHelper = new();
            ITheme theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private static IEnumerable<ModelItem> GenerateModelItems()
        {
            yield return new("主页", "主页", "HomePage", typeof(HomePage));
            yield return new("RT-Thread", "RT-Thread", "RTThreadPage", typeof(RTThreadPage));
        }

        private readonly ICollectionView _listBoxModelItemsView;

        private bool ModelItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_textBoxSearchItemsKeyword))
            {
                return true;
            }

            return obj is ModelItem item
                   && item.Name.ToLower(new CultureInfo("zh-CN", false)).Contains(_textBoxSearchItemsKeyword.ToLower(new CultureInfo("zh-CN", false)));
        }

        #region 绑定

        #region 其他绑定

        #region Title

        private string _title = "Misaka-Station";

        public string Title
        {
            get => _title;
            set => _ = SetProperty(ref _title, value);
        }

        #endregion Title

        #region TextBoxSearchItemsKeyword

        private string _textBoxSearchItemsKeyword;

        public string TextBoxSearchItemsKeyword
        {
            get => _textBoxSearchItemsKeyword;
            set
            {
                if (SetProperty(ref _textBoxSearchItemsKeyword, value))
                {
                    _listBoxModelItemsView.Refresh();
                }
            }
        }

        #endregion TextBoxSearchItemsKeyword

        #region ListBoxModelSelectedIndex

        private int _listBoxModelSelectedIndex;

        public int ListBoxModelSelectedIndex
        {
            get => _listBoxModelSelectedIndex;
            set => _ = SetProperty(ref _listBoxModelSelectedIndex, value);
        }

        #endregion ListBoxModelSelectedIndex

        #region ListBoxModelSelectedItem

        private ModelItem _listBoxModelSelectedItem;

        public ModelItem ListBoxModelSelectedItem
        {
            get => _listBoxModelSelectedItem;
            set
            {
                if (SetProperty(ref _listBoxModelSelectedItem, value))
                {
                    _regionManager.RequestNavigate("MainContentRegion", _listBoxModelSelectedItem.ViewName);
                }
            }
        }

        #endregion ListBoxModelSelectedItem

        #region TextBoxItemsFocus

        private bool _textBoxItemsFocus;

        public bool TextBoxItemsFocus
        {
            get => _textBoxItemsFocus;
            set => _ = SetProperty(ref _textBoxItemsFocus, value);
        }

        #endregion TextBoxItemsFocus

        #region ListBoxModelItems

        private ObservableCollection<ModelItem> _listBoxModelItems = new();

        public ObservableCollection<ModelItem> ListBoxModelItems
        {
            get => _listBoxModelItems;
            set => _ = SetProperty(ref _listBoxModelItems, value);
        }

        #endregion ListBoxModelItems

        #region MainSnackbarMessageQueue

        private SnackbarMessageQueue _mainSnackbarMessageQueue = new(TimeSpan.FromMilliseconds(3000));

        public SnackbarMessageQueue MainSnackbarMessageQueue
        {
            get => _mainSnackbarMessageQueue;
            set => _ = SetProperty(ref _mainSnackbarMessageQueue, value);
        }

        #endregion MainSnackbarMessageQueue

        #region ClosingCommand

        private DelegateCommand _closingCommand;

        public DelegateCommand ClosingCommand =>
            _closingCommand ??= new DelegateCommand(ExecuteClosingCommand);

        private void ExecuteClosingCommand()
        {
            if (ListBoxModelSelectedIndex >= 0 && ListBoxModelSelectedIndex < ListBoxModelItems.Count)
            {
                _iniFile.SetMianWindowFunctionListIndex(ListBoxModelSelectedIndex);
            }
            else if (ListBoxModelSelectedIndex != -1)
            {
                _iniFile.SetMianWindowFunctionListIndex(0);
            }

            _eventAggregator.GetEvent<CloseEvent>().Publish("MainWindow");
        }

        #endregion ClosingCommand

        #endregion 其他绑定

        #region 按钮绑定

        #region ButtonGithubCommand

        private DelegateCommand _buttonGithubCommand;

        public DelegateCommand ButtonGithubCommand =>
            _buttonGithubCommand ??= new DelegateCommand(ExecuteButtonGithubCommand);

        private void ExecuteButtonGithubCommand()
        {
            Services.Sys.Process.StartProcess.OpenGithub();
        }

        #endregion ButtonGithubCommand

        #region ButtonBlogCommand

        private DelegateCommand _buttonBlogCommand;

        public DelegateCommand ButtonBlogCommand =>
            _buttonBlogCommand ??= new DelegateCommand(ExecuteButtonBlogCommand);

        private void ExecuteButtonBlogCommand()
        {
            Services.Sys.Process.StartProcess.OpenBlog();
        }

        #endregion ButtonBlogCommand

        #region ToggleButtonThemeModeCommand

        private DelegateCommand<object> _toggleButtonThemeModeCommand;

        public DelegateCommand<object> ToggleButtonThemeModeCommand =>
            _toggleButtonThemeModeCommand ??= new DelegateCommand<object>(ExecuteToggleButtonThemeModeCommand);

        private void ExecuteToggleButtonThemeModeCommand(object parameter)
        {
            if (parameter is bool b)
            {
                ModifyTheme(b);
            }
        }

        #endregion ToggleButtonThemeModeCommand

        #region ToggleButtonMenuCommand

        private DelegateCommand _toggleButtonMenuCommand;

        public DelegateCommand ToggleButtonMenuCommand =>
            _toggleButtonMenuCommand ??= new DelegateCommand(ExecuteToggleButtonMenuCommand);

        private void ExecuteToggleButtonMenuCommand()
        {
            TextBoxItemsFocus = false;
            TextBoxItemsFocus = true;
        }

        #endregion ToggleButtonMenuCommand

        #region ButtonHomeCommand

        private DelegateCommand _buttonHomeCommand;

        public DelegateCommand ButtonHomeCommand =>
            _buttonHomeCommand ??= new DelegateCommand(ExecuteButtonHomeCommand);

        private void ExecuteButtonHomeCommand()
        {
            ListBoxModelSelectedItem = ListBoxModelItems[0];
        }

        #endregion ButtonHomeCommand

        #region ButtonFeedbackCommand

        private DelegateCommand _buttonFeedbackCommand;

        public DelegateCommand ButtonFeedbackCommand =>
            _buttonFeedbackCommand ??= new DelegateCommand(ExecuteButtonFeedbackCommand);

        private void ExecuteButtonFeedbackCommand()
        {
            _dialogService.ShowDialog("FeedbackDialog");
        }

        #endregion ButtonFeedbackCommand

        #region ButtonJoinUsCommand

        private DelegateCommand _buttonJoinUsCommand;

        public DelegateCommand ButtonJoinUsCommand =>
            _buttonJoinUsCommand ??= new DelegateCommand(ExecuteButtonJoinUsCommand);

        private void ExecuteButtonJoinUsCommand()
        {
            Services.Sys.Process.StartProcess.OpenGithub();
        }

        #endregion ButtonJoinUsCommand

        #region ButtonUpgradeCommand

        private DelegateCommand _buttonUpgradeCommand;

        public DelegateCommand ButtonUpgradeCommand =>
            _buttonUpgradeCommand ??= new DelegateCommand(ExecuteButtonUpgradeCommand);

        private void ExecuteButtonUpgradeCommand()
        {
            Services.Sys.Process.StartProcess.OpenGithubReleases();
        }

        #endregion ButtonUpgradeCommand

        #region ButtonReleaseNoteCommand

        private DelegateCommand _buttonReleaseNoteCommand;

        public DelegateCommand ButtonReleaseNoteCommand =>
            _buttonReleaseNoteCommand ??= new DelegateCommand(ExecuteButtonReleaseNoteCommand);

        private void ExecuteButtonReleaseNoteCommand()
        {
            Services.Sys.Process.StartProcess.OpenGithubReleasesNote();
        }

        #endregion ButtonReleaseNoteCommand

        #region ButtonPrivacyCommand

        private DelegateCommand _buttonPrivacyCommand;

        public DelegateCommand ButtonPrivacyCommand =>
            _buttonPrivacyCommand ??= new DelegateCommand(ExecuteButtonPrivacyCommand);

        private void ExecuteButtonPrivacyCommand()
        {
            MainSnackbarMessageQueue.Enqueue("暂时没有任何涉及隐私的功能");
        }

        #endregion ButtonPrivacyCommand

        #region ButtonAboutCommand

        private DelegateCommand _buttonAboutCommand;

        public DelegateCommand ButtonAboutCommand =>
            _buttonAboutCommand ??= new DelegateCommand(ExecuteButtonAboutCommand);

        private void ExecuteButtonAboutCommand()
        {
            Services.Sys.Process.StartProcess.OpenAbout();
        }

        #endregion ButtonAboutCommand

        #endregion 按钮绑定

        #endregion 绑定
    }
}