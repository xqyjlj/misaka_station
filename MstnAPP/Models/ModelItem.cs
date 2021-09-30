using Prism.Mvvm;
using System;

namespace MstnAPP.Models
{
    public class ModelItem : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _ = SetProperty(ref _name, value);
        }

        private string _toolTip;

        public string ToolTip
        {
            get => _toolTip;
            set => _ = SetProperty(ref _toolTip, value);
        }

        private string _viewName;

        public string ViewName
        {
            get => _viewName;
            set => _ = SetProperty(ref _viewName, value);
        }

        private Type _viewType;

        public Type ViewType
        {
            get => _viewType;
            set => _ = SetProperty(ref _viewType, value);
        }

        public ModelItem(string name, string toolTip, string viewName, Type viewType)
        {
            Name = name;
            ToolTip = toolTip;
            ViewName = viewName;
            ViewType = viewType;
        }
    }
}