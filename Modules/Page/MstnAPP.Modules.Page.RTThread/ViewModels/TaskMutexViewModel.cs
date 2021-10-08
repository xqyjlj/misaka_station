using MstnAPP.Modules.Page.RTThread.Events;
using MstnAPP.Modules.Page.RTThread.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskMutexViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        private readonly IEventAggregator _eventAggregator;

        public TaskMutexViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _ = _eventAggregator.GetEvent<EventMutex>().Subscribe(EventMutexReceived);
        }

        private ObservableCollection<ModelMutex> _DataGridItems = new();

        public ObservableCollection<ModelMutex> DataGridItems
        {
            get => _DataGridItems;
            set => _ = SetProperty(ref _DataGridItems, value);
        }

        private void EventMutexReceived(List<string> list)
        {
            ParseData(list);
        }

        private void ParseData(List<string> list)
        {
            string head, msg;

            msg = list[0];
            head = msg[0..^10];//"list_mutex".Length
            msg = list[^1]; //列表中的最后一个字符串

            if (msg == head)  //第一个和最后一个 相同表示报文接收完毕
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    DataGridItems.Clear();
                });

                int count = list.Count - 4;
                for (int i = 3; i < 3 + count; i++)
                {
                    msg = list[i];

                    string[] subs = msg.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (subs.Length == 4)
                    {
                        ModelMutex model = new();
                        model.Name = subs[0];
                        model.Owner = subs[1];
                        model.Hold = subs[2];
                        model.Suspend = subs[3];
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            DataGridItems.Add(model);
                        });
                    }
                }
            }
        }
    }
}