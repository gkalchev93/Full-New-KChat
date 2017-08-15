using Microsoft.Win32;
using System;
using System.Windows;

namespace KChatClient.Services
{
    public class DialogService : IDialogService
    {
        public string OpenFile(string caption, string filter = "All files (*.*)|*.*")
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            diag.Title = caption;
            diag.Filter = filter;
            diag.CheckFileExists = true;
            diag.CheckPathExists = true;
            diag.RestoreDirectory = true;

            if (diag.ShowDialog() == true) return diag.FileName;
            return string.Empty;
        }

        public bool ShowConfirmationRequest(string message, string caption = "")
        {
            var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            return result.HasFlag(MessageBoxResult.OK);
        }

        public void ShowNotification(string message, string caption = "")
        {
            MessageBox.Show(message, caption); ;
        }

        public void ShowToastNotification(string desc)
        {
            Services.ToastNotificationModel.ShowToastNotification(desc);
        }
    }
}
