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
    /// OderNotPage.xaml 的交互逻辑
    /// </summary>
    public partial class OderNotPage : Page
    {
        public OderNotPage()
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

                MySqlConnection conn = new MySqlConnection(CommonValue.mysqlConectString);
                conn.Open();
                string cmd = "select * from oder where complete=0";
                MySqlCommand myCmd = new MySqlCommand(cmd, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd, conn);
                MySqlDataReader reader = myCmd.ExecuteReader();
                while (reader.Read())
                {

                    string id = reader["oder_id"].ToString();
                    string name = reader["sum"].ToString();

                    data.Rows.Add(id, name);
                }
                listView.DataContext = data.DefaultView;

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("查询订单失败:" + e.Message);
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CommonValue.FOOD_PAY_ID = int.Parse(idPayBox.Text.ToString());
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("OderDetailPage.xaml", UriKind.Relative);
            window.Show();
        }
    }
}
