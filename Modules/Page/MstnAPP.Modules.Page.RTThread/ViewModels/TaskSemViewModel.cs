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
    public class TaskSemViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        public TaskSemViewModel(IEventAggregator eventAggregator)
        {
            _ = eventAggregator.GetEvent<EventSem>().Subscribe(EventSemReceived);
        }

        private ObservableCollection<ModelSem> _dataGridItems = new();

        public ObservableCollection<ModelSem> DataGridItems
        {
            get => _dataGridItems;
            set => _ = SetProperty(ref _dataGridItems, value);
        }

        private void EventSemReceived(List<string> list)
        {
            ParseData(list);
        }

        private void ParseData(List<string> list)
        {
            var msg = list[0];
            var head = msg[0..^8];
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
                ModelSem model = new()
                {
                    Name = subs[0],
                    Value = subs[1],
                    Suspend = subs[2]
                };
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    DataGridItems.Add(model);
                });
            }
        }
    }
}