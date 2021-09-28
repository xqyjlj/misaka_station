using Prism.Mvvm;
using System;

namespace MstnAPP.ViewModels
{
    public class ModelItem : BindableBase
    {
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public string ViewName { get; set; }
        public Type ViewType { get; set; }

        public ModelItem(string name, string toolTip, string viewName, Type viewType)
        {
            Name = name;
            ToolTip = toolTip;
            ViewName = viewName;
            ViewType = viewType;
        }
    }
}