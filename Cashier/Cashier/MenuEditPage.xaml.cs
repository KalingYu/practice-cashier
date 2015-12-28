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
using System.Data.SqlClient;
using System.Data;
namespace Cashier
{
    /// <summary>
    /// MenuEditPage.xaml 的交互逻辑
    /// </summary>
    public partial class MenuEditPage : Page
    {

        List<string> foodList = new List<string>();
        public MenuEditPage()
        {
            InitializeComponent();
            ShowMenu();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CommonValue.EDIT_FOOD_ID = int.Parse(idChangeBox.Text.ToString());
            OpenFoodEditPage();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowMenu()
        {
            try
            {
                //获取表格
                DataTable data = new DataTable("food");
                DataColumn food_id = new DataColumn("food_id");
                data.Columns.Add(new DataColumn("food_id", typeof(string)));
                data.Columns.Add(new DataColumn("name", typeof(string)));
                data.Columns.Add(new DataColumn("price", typeof(string)));
                data.Columns.Add(new DataColumn("category", typeof(string)));

                MySqlConnection conn = new MySqlConnection(MysqlDatabaseConn.connString);
                conn.Open();
                string cmd = "select * from food";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                while (reader.Read())
                {

                    string id = reader["food_id"].ToString();
                    string name = reader["name"].ToString();
                    string price = reader["price"].ToString();
                    string category = reader["category"].ToString();
                    data.Rows.Add(id, name, price, category);
                }
                listView.DataContext = data.DefaultView;
              
                conn.Close();
                
            }
            catch(Exception e)
            {
                MessageBox.Show("查询菜单失败:" + e.Message);
            }

        }

        private void OpenFoodEditPage()
        {
            NavigationWindow window = new NavigationWindow();

            window.Source = new Uri("FoodEditPage.xaml", UriKind.Relative);
            window.Show();
        }
    }
}
