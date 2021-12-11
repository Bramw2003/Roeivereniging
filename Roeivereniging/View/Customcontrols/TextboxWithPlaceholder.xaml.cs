using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for TextboxWithPlaceholder.xaml
    /// </summary>
    public partial class TextboxWithPlaceholder : UserControl
    {
        public TextboxWithPlaceholder()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string PlaceholderText { get; set; }
        private void Textbox_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Textbox.IsKeyboardFocused)
            {
                Placeholder.Visibility = Visibility.Hidden;
            }
            else if (Textbox.Text == "")
            {
                Placeholder.Visibility = Visibility.Visible;
            }
        }
    }
}
