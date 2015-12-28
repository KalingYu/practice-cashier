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
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
namespace Cashier
{
    /// <summary>
    /// OderDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class OderDetailPage : Page
    {
        private double sum = 0;//总价
        public OderDetailPage()
        {
            InitializeComponent();
            
            ShowMenu();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Pay();
        }

        private void ShowMenu()
        {
            try
            {
                //获取表格
                DataTable data = new DataTable("food");
                //DataColumn food_id = new DataColumn("food_id");
                data.Columns.Add(new DataColumn("food_id", typeof(string)));
                data.Columns.Add(new DataColumn("name", typeof(string)));
                data.Columns.Add(new DataColumn("price", typeof(string)));
                data.Columns.Add(new DataColumn("number", typeof(string)));
                data.Columns.Add(new DataColumn("total_price", typeof(string)));

                MySqlConnection conn = new MySqlConnection(MysqlDatabaseConn.connString);
                conn.Open();
                string cmd = "select * from food_odered where oder_id='" + CommonValue.FOOD_PAY_ID + "'";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                ObservableCollection<Food> memberData = new ObservableCollection<Food>();
                while (reader.Read())
                {

                    string food_id = reader["food_id"].ToString();
                    string name = reader["food_name"].ToString();
                    string price = reader["price"].ToString();
                    string number = reader["number"].ToString();
                    string total_price = reader["total_price"].ToString();
                    sum += Convert.ToDouble(total_price);
                    memberData.Add(new Food()
                    {
                        food_id = food_id,
                        name = name,
                        price = price,
                        number = number,
                        total_price = total_price
                    });

                }
                dataGrid.DataContext = memberData;
                sumBlock.Text = sum.ToString();
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("查询菜单失败:" + e.Message);
            }

        }

        public void Pay()
        {
            try
            {
                double paid = Convert.ToDouble(paidBox.Text.ToString());
                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                String cmd = "update oder set complete=1, paid ='" + paid + "'  where oder_id='" + CommonValue.FOOD_PAY_ID + "'";
                MySqlCommand mycmd = new MySqlCommand(cmd, conn);
                if (mycmd.ExecuteNonQuery() > 0)
                {
                    double change = Convert.ToDouble(paidBox.Text.ToString()) - sum;
                    changeBox.Text = "支付成功！找零" + change;            
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                changeBox.Text = "对不起，支付失败，请再次尝试！" + e.Message;
            }

        }

        public class Food
        {
            public string food_id { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public string category { get; set; }
            public string number { get; set; }
            public string total_price { get; set; }

        }
    }
}
