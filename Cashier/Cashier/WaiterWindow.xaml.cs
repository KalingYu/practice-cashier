using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cashier
{
    /// <summary>
    /// WaiterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WaiterWindow : Window
    {
        public WaiterWindow()
        {
            InitializeComponent();
            frame.Source = new Uri("WaiterMenuPage.xaml", UriKind.Relative);
        }

        private void textBlock2_Copy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String choice = btn.Content.ToString();
            switch (choice)
            {
                case "菜单浏览":
                    LoadWaiterMenuPage();
                    break;
                case "已完成订单":
                    LoadOderCompletedPage();
                    break;
                case "未完成订单":
                    LoadOderNotPage();
                    break;
            }
        }

        private void LoadWaiterMenuPage()
        {
            frame.Source = new Uri("WaiterMenuPage.xaml", UriKind.Relative);
        }

        private void LoadOderNotPage()
        {
            frame.Source = new Uri("OderNotPage.xaml", UriKind.Relative);
        }

        private void LoadOderCompletedPage()
        {
            frame.Source = new Uri("OderCompletedPage.xaml", UriKind.Relative);
        }
    }
}
