using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Bookit.Data.Admin.WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEnterMapNode_Click(object sender, RoutedEventArgs e)
        {
            MapNodeWnd wnd = new MapNodeWnd();
            wnd.ShowDialog();
        }

        private void btnPathEnter_Click(object sender, RoutedEventArgs e)
        {
            PathWnd pathWnd = new PathWnd();
            pathWnd.ShowDialog();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            CubeWnd cubeWnd = new CubeWnd();
            cubeWnd.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MeetingRoomWnd mrWnd = new MeetingRoomWnd();
            mrWnd.ShowDialog();
        }
    }
}
