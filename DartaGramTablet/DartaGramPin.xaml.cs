using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DartaGramTablet
{
    public sealed partial class DartaGramPin : UserControl
    {
        public DartaGramPin(int count)
        {
            this.InitializeComponent();
            infobox.Visibility = Visibility.Collapsed;
            if(count==3)
            {
                img4.Visibility = Visibility.Collapsed;
            }
            else
            {
                img4.Visibility = Visibility.Visible;
            }
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            infobox.Visibility = Visibility.Collapsed;
        }
        private void red_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            infobox.Visibility = Visibility.Visible;

        }
    }
}
