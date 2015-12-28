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
    /// OderCompletedPage.xaml 的交互逻辑
    /// </summary>
    public partial class OderCompletedPage : Page
    {
        public OderCompletedPage()
        {
            InitializeComponent();
            ShowOders();
        }

        private void ShowOders()
        {
            try
            {
                //获取表格
                DataTable data = new DataTable("oder");
                data.Columns.Add(new DataColumn("oder_id", typeof(string)));
                data.Columns.Add(new DataColumn("sum", typeof(string)));
                data.Columns.Add(new DataColumn("paid", typeof(string)));
                data.Columns.Add(new DataColumn("change", typeof(string)));

                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                string cmd = "select * from oder where complete=1";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                while (reader.Read())
                {

                    string id = reader["oder_id"].ToString();
                    string name = reader["sum"].ToString();
                    string price = reader["paid"].ToString();
                    string category = reader["change"].ToString();
                    data.Rows.Add(id, name, price, category);
                }
                listView.DataContext = data.DefaultView;

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("查询订单失败:" + e.Message);
            }

        }
    }
}
