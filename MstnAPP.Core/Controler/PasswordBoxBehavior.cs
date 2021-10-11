using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace MstnAPP.Core.Controler
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            var password = PasswordBoxHelper.GetPassword(passwordBox);

            if (passwordBox != null && passwordBox.Password != password)
            {
                PasswordBoxHelper.SetPassword(passwordBox, passwordBox.Password);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PasswordChanged -= OnPasswordChanged;
        }
    }
}