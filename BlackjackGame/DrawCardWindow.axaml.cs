using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace BlackjackGame
{
    public partial class DrawCardWindow : Window
    {
        public DrawCardWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            this.Close(true);
        }
    }
}
