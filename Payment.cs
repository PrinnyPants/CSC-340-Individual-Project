using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Pizza_Order_System
{
    internal class Payment
    {
        public static void insertNewPaymentInfo()
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO cordial_payment (cardNum, cardHoldName, cardHoldAdd) VALUES (null, null, null)";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
        public static void savePaymentInfo(string num, string name, string add)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE cordial_payment SET cardNum=@num, cardHoldName=@name, cardHoldAdd=@add WHERE cardNum IS NULL";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@num", num);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@add", add);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
        public static void displayPaymentError()
        {
            string message = "There was an issue with your payment. Please double check you entered it right and try again.";
            string title = "Payment Error";
            MessageBox.Show(message, title);
        }

        public static bool checkPayment(string cardnum, string name, string address)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM cordial_member WHERE cardNum=@num AND expDate=@date AND cardHoldName=@name AND cardHoldAdd=@add";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@num", cardnum);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@add", address);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    myReader.Close();
                    return true;
                }
                else
                {
                    myReader.Close();
                    return false;
                }
                //myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return false;
        }
    }
}
