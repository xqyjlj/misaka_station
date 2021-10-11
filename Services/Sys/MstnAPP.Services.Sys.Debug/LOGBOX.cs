using System.Windows;

namespace MstnAPP.Services.Sys.Debug
{
    public class LogBox
    {
        public static void W(string messageBoxText)
        {
            Log.W(messageBoxText);
            _ = MessageBox.Show(messageBoxText, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void W(string messageBoxText, string caption)
        {
            Log.W(caption + "：" + messageBoxText);
            _ = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void E(string messageBoxText)
        {
            Log.E(messageBoxText);
            _ = MessageBox.Show(messageBoxText, "警告", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void E(string messageBoxText, string caption)
        {
            Log.E(caption + "：" + messageBoxText);
            _ = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void I(string messageBoxText)
        {
            Log.I(messageBoxText);
            _ = MessageBox.Show(messageBoxText, "警告", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void I(string messageBoxText, string caption)
        {
            Log.I(caption + "：" + messageBoxText);
            _ = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}