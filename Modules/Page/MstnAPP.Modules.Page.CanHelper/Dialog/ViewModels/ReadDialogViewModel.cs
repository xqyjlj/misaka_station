using MstnAPP.Modules.Page.CanHelper.Models;
using MstnAPP.Services.Driver.ICanBus;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MstnAPP.Modules.Page.CanHelper.Dialog.ViewModels
{
    public class ReadDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = "Can接收窗口";

        public string Title
        {
            get => _title;
            set => _ = SetProperty(ref _title, value);
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        public ReadDialogViewModel(ICan can)
        {
            can.DataReceived += OnDataReceived;
        }

        private void OnDataReceived(byte[] message, int id, int dlc, CanBusEnum flag)
        {
            var dest = new string[8];

            for (var i = 0; i < dlc; i++)
            {
                dest[i] = message[i].ToString("D", CultureInfo.InvariantCulture);
            }

            var model = new ModelRead
            {
                Id = id.ToString("D", CultureInfo.InvariantCulture),
                Dlc = dlc.ToString("D", CultureInfo.InvariantCulture),
                D0 = dest[0],
                D1 = dest[1],
                D2 = dest[2],
                D3 = dest[3],
                D4 = dest[4],
                D5 = dest[5],
                D6 = dest[6],
                D7 = dest[7],
                Time = DateTime.Now.TimeOfDay.ToString()
            };

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                DataGridItems.Add(model);
            });
        }

        #region DataGridItems

        private ObservableCollection<ModelRead> _dataGridItems = new();

        public ObservableCollection<ModelRead> DataGridItems
        {
            get => _dataGridItems;
            set => _ = SetProperty(ref _dataGridItems, value);
        }

        #endregion DataGridItems
    }
}