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
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace Cashier
{
    /// <summary>
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {

        private String user;
        private String password;
        private String passwordAgain;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            user = userBox.Text.ToString();
            password = passwordBox.Text.ToString();
            passwordAgain = passwordAgainBox.Text.ToString();
            Button btn = sender as Button;
            String choice = btn.Content.ToString();
            switch (choice)
            {
                case "注册":
                    UserRegister();
                    break;
                case "登录":
                    UserLogin();
                    break;
            }
        }


        private void UserRegister()
        {

            if (user.Equals(""))
            {
                MessageBox.Show("请输入用户名");
            }
            else if (password.Equals(""))
            {
                MessageBox.Show("请输入密码");
            }
            else if (!password.Equals(passwordAgain))
            {
                MessageBox.Show("请检查两次输入的密码是否正确");
            }
            else
            {
                CheckAndRegister();
            }
        }

        //转到用户登录界面
        private void UserLogin()
        {
            MainWindow register = new MainWindow();
            register.Show();
            this.Close();
        }

        //验证用户信息， 如果不存在用户就并注册用户信息
        private void CheckAndRegister()
        {
            try
            {
                String conStr = "server=localhost;User Id=root;password=;Database=canyin";
                MySqlConnection conn = new MySqlConnection(conStr);
                conn.Open();
                String cmd = "insert into user( user, password) values(user, password)";
                MySqlCommand mycmd = new MySqlCommand(cmd, conn);
                if (mycmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("注册成功！");
                    conn.Close();
                    OpenWaiterWindow();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("对不起，用户注册失败，请再次尝试！" + e.Message + "您输入的用户名" + user + "您输入的密码" + password + "不合规范");
            }
            
      
        }

        //打开管理员窗口
        private void OpenAdminWindow()
        {
            AdminWindow aw = new AdminWindow();
            aw.Show();
            this.Close();
        }

        //打开服务员窗口
        private void OpenWaiterWindow()
        {
            WaiterWindow ww = new WaiterWindow();
            ww.Show();
            this.Close();
        }

    }
}
