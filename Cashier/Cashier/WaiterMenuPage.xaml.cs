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
    /// WaiterMenuPage.xaml 的交互逻辑
    /// </summary>
    public partial class WaiterMenuPage : Page
    {
        public WaiterMenuPage()
        {
            InitializeComponent();
            ShowMenu();
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
                data.Columns.Add(new DataColumn("category", typeof(string)));

                MySqlConnection conn = new MySqlConnection(MysqlDatabaseConn.connString);
                conn.Open();
                string cmd = "select * from food";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                ObservableCollection<Food> memberData = new ObservableCollection<Food>();
                while (reader.Read())
                {

                    string food_id = reader["food_id"].ToString();
                    string name = reader["name"].ToString();
                    string price = reader["price"].ToString();
                    string category = reader["category"].ToString();

                   
                    memberData.Add(new Food()
                    {
                        food_id = food_id,
                        name = name,
                        price = price,
                        category = category,
                        number = ""
                    });

                }
                dataGrid.DataContext = memberData;
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("查询菜单失败:" + e.Message);
            }

        }

        public class Food
        {
            public string food_id { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public string category { get; set; }
            public string number { get; set; }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
           GetFoodListAndInsert();
        
        }

        private void  GetFoodListAndInsert()
        {
            int oder_id;
            float sum = 0;//订单总价
            oder_id = GetOderId();
            List<Food> foodList = new List<Food>();

            foreach (Food fooTemp in dataGrid.Items)
            {

               

                string num = fooTemp.number;
                if (!num.Equals(""))
                {
                    foodList.Add(new Food()
                    {
                        food_id = fooTemp.food_id,
                        name = fooTemp.name,
                        price = fooTemp.price,
                        category = fooTemp.category,
                        number = fooTemp.number
                    });
                }
            }

            try
            {

                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                foreach (Food foodTemp in foodList)
                {
                    float total_price = float.Parse(foodTemp.price) * float.Parse(foodTemp.number);
                    sum += total_price;
                    String cmd = "insert into food_odered( food_id, food_name, oder_id, number, total_price, price) values('" + foodTemp.food_id + "','" + foodTemp.name + "','" + oder_id + "','" + foodList.Count + "','" + total_price + "','" + foodTemp.price + "')";
                    MySqlCommand mycmd = new MySqlCommand(cmd, conn);
                    mycmd.ExecuteNonQuery();
                }

                String cmd2 = "insert into oder( oder_id, sum) values('" + oder_id + "','" + sum +  "')";
                MySqlCommand mycmd2 = new MySqlCommand(cmd2, conn);
                ;
                if (mycmd2.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("下单成功！");         
                    conn.Close();

                }
            }
            

            
            catch (Exception e)
            {
                MessageBox.Show("对不起，数据库连接失败，请再次尝试！" + e.Message);
            }


        }

        private int GetOderId()
        {
            Random random = new Random();
            int oder_id = random.Next(0, 5000);
            return oder_id;
        }

       

       
    }
}
