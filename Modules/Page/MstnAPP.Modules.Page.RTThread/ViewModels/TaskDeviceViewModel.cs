using MstnAPP.Modules.Page.RTThread.Event;
using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskDeviceViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        public TaskDeviceViewModel(IEventAggregator eventAggregator)
        {
            _ = eventAggregator.GetEvent<EventDevice>().Subscribe(EventDeviceReceived);
        }

        private ObservableCollection<ModelDevice> _dataGridItems = new();

        public ObservableCollection<ModelDevice> DataGridItems
        {
            get => _dataGridItems;
            set => _ = SetProperty(ref _dataGridItems, value);
        }

        private void EventDeviceReceived(List<string> list)
        {
            ParseData(list);
        }

        private void ParseData(List<string> list)
        {
            var msg = list[0];
            var head = msg[0..^11];
            msg = list[^1]; //列表中的最后一个字符串

            if (msg != head) return;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                DataGridItems.Clear();
            });

            var count = list.Count - 4;
            for (var i = 3; i < 3 + count; i++)
            {
                msg = list[i];

                var subs = msg.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (subs.Length != 3) continue;
                ModelDevice model = new()
                {
                    Name = subs[0],
                    Type = subs[1],
                    RefCount = subs[2]
                };
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    DataGridItems.Add(model);
                });
            }
        }
    }
}