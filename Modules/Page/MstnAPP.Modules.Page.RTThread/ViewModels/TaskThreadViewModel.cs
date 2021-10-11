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
    public class TaskThreadViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        public TaskThreadViewModel(IEventAggregator eventAggregator)
        {
            _ = eventAggregator.GetEvent<EventThread>().Subscribe(EventThreadReceived);
        }

        private ObservableCollection<ModelThread> _dataGridItems = new();

        public ObservableCollection<ModelThread> DataGridItems
        {
            get => _dataGridItems;
            set => _ = SetProperty(ref _dataGridItems, value);
        }

        private void EventThreadReceived(List<string> list)
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
                if (subs.Length != 8) continue;
                ModelThread model = new()
                {
                    Name = subs[0],
                    Pri = subs[1],
                    Status = subs[2],
                    Sp = subs[3],
                    StackSize = subs[4],
                    MaxUsed = subs[5],
                    LeftTick = subs[6],
                    Error = subs[7]
                };
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    DataGridItems.Add(model);
                });
            }
        }
    }
}