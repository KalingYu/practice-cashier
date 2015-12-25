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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cashier
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void richTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //监听注册按钮
        private void button1_Click(object sender, RoutedEventArgs e)
        {

            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
