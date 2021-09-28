using System.Windows;

namespace MstnAPP.Services.Sys.Debug
{
    public class LOGBOX
    {
        public static void W(string messageBoxText)
        {
            LOG.W(messageBoxText);
            _ = MessageBox.Show(messageBoxText, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void W(string messageBoxText, string caption)
        {
            LOG.W(caption + "：" + messageBoxText);
            _ = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void E(string messageBoxText)
        {
            LOG.E(messageBoxText);
            _ = MessageBox.Show(messageBoxText, "警告", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void E(string messageBoxText, string caption)
        {
            LOG.E(caption + "：" + messageBoxText);
            _ = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void I(string messageBoxText)
        {
            LOG.I(messageBoxText);
            _ = MessageBox.Show(messageBoxText, "警告", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void I(string messageBoxText, string caption)
        {
            LOG.I(caption + "：" + messageBoxText);
            _ = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}