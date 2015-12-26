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
using MySql.Data.MySqlClient;

namespace Cashier
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //用户账户
        private String user;
        private String password;
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
            Button btn = sender as Button;
            String choice = btn.Content.ToString();
            switch (choice)
            {
                case "登录":
                    UserLogin();
                    break;
                case "注册":
                    UserRegister();
                    break;
            }
            
        
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void UserLogin()
        {
            //用户登录的逻辑代码
            user = userBox.Text.ToString();
            password = passwordBox.Text.ToString();
            if(user.Equals(""))
            {
                MessageBox.Show("请输入用户名");
            }
            else if(password.Equals(""))
            {
                MessageBox.Show("请输入密码");
            }
            else
            {
                CheckInfoAndLogin();
            }
        }

        private void UserRegister()
        {
            //跳转到登陆界面
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }

        //检查用户的数据，如果查询失败则返回密码错误
        private void CheckInfoAndLogin()
        {
            
            try
            {
                String conStr = "server=localhost;User Id=root;password=;Database=canyin";
                MySqlConnection conn = new MySqlConnection(conStr);
                conn.Open();
                string cmd = "select * from user where user=" + user;
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                reader.Read();
                string dbUser = reader["user"].ToString();
                string dbPassword = reader["password"].ToString();
                if (password.Equals(password) && dbUser.Equals("admin"))
                {
                    OpenAdminWindow();
                }
                else if (password.Equals(password))
                {
                    OpenWaiterWindow();
                }
                conn.Close();
            }
        
            catch(Exception e)
            {
                String msg = e.Message;
                MessageBox.Show("数据库连接错误！" + msg);
            }
          
        }

        //打开管理员窗口
        private void OpenAdminWindow()
        {

        }

        //打开服务员窗口
        private void OpenWaiterWindow()
        {

        }

    }
}
