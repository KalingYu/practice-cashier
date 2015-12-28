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
    /// AddFoodPage.xaml 的交互逻辑
    /// </summary>
    ///  
    public partial class AddFoodPage : Page
    {

        private String mysqlConnStr = "server=localhost;User Id=root;password=;Database=canyin";
        public AddFoodPage()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {

            InsertFood();

        }

        private void InsertFood()
        {
            String foodName = foodNameBox.Text.ToString();
            String price = priceBox.Text.ToString();
            String category = categoryBox.Text;

            if(foodName.Equals("") || price.Equals("") || category.Equals(""))
            {
                resultBox.Text = "请将食物信息填写完整";
                return;
            }

            // MessageBox.Show("食物名称是：" + foodName + ", 价格是： " + price + "， 种类是： " + category);
            try
            {
             
                MySqlConnection conn = new MySqlConnection(mysqlConnStr);
                conn.Open();
                String cmd = "insert into food(name, price, category) values('" + foodName + "','" + price + "','" + category + "')";
                MySqlCommand mycmd = new MySqlCommand(cmd, conn);
                if (mycmd.ExecuteNonQuery() > 0)
                {           
                    resultBox.Text = "食品添加成功";
                    foodNameBox.Text = "";
                    priceBox.Text = "";
                    categoryBox.Text = "";
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                resultBox.Text = "食品添加失败" + e.Message;
     
            }
        }

      
    }
}
