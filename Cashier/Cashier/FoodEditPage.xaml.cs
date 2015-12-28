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
    /// FoodEditPage.xaml 的交互逻辑
    /// </summary>
    public partial class FoodEditPage : Page
    {
        public FoodEditPage()
        {
            InitializeComponent();
            InitData();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
           
            updateFoodInfo();
            
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteFoodData();
        }

        private void updateFoodInfo()
        {
            try
            {
                string name = foodNameBox.Text.ToString();
                string price = foodPriceBox.Text.ToString();
                string category = categoryBox.Text;
                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                String cmd = "update food set name='" + name + "',price='" + price + "',category='" + category + "'where food_id='" + CommonValue.EDIT_FOOD_ID + "'";
                MySqlCommand mycmd = new MySqlCommand(cmd, conn);
                if (mycmd.ExecuteNonQuery() > 0)
                {
                    resultBox.Text = "食品信息修改成功,继续修改或退出";                   
                    conn.Close();
                    InitData();
                  
                }
            }
            catch(Exception e)
            {
                resultBox.Text = "食品修改失败" + e.Message;
            }     

        }

        private void InitData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                String cmd = "select * from food where food_id='" + CommonValue.EDIT_FOOD_ID + "'";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                reader.Read();
                string name = reader["name"].ToString();
                string price = reader["price"].ToString();
                string category = reader["category"].ToString();
                foodNameBox.Text = name;
                foodPriceBox.Text = price;
                categoryBox.Text = category;

            }
            catch(Exception e)
            {
                resultBox.Text = "获取食物信息失败" + e.Message;
            }
        }

        private void DeleteFoodData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                String cmd = "delete from food where food_id='" + CommonValue.EDIT_FOOD_ID + "'";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                if(myCmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("删除食物成功！");
                    conn.Close();
            
                }

            }
            catch (Exception e)
            {
                resultBox.Text = "删除食物失败" + e.Message;
            }
        }
    }
}
