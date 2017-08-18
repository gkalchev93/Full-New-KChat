using Microsoft.Win32;
using System;
using System.IO;
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

        public void SaveFile(string caption, string fileName, byte[] file)
        {
            SaveFileDialog diag = new SaveFileDialog();
            diag.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            diag.FileName = fileName;
            diag.CheckFileExists = false;

            Stream myStream;
            if (diag.ShowDialog() == true)
            {
                if ((myStream = diag.OpenFile()) != null)
                {
                    myStream.Write(file, 0, file.Length);
                    myStream.Close();
                }
            }
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
