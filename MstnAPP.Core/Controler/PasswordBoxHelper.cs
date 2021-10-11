using System.Windows;
using System.Windows.Controls;

namespace MstnAPP.Core.Controler
{
    public class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty =
                DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var password = (string)e.NewValue;

            if (sender is PasswordBox passwordBox && passwordBox.Password != password)
            {
                passwordBox.Password = password;
            }
        }

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }
    }
}