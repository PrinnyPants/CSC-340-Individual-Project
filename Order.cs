using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Pizza_Order_System
{
    internal class Order
    {
        int orderID;
        double totalPrice;
        String status;
        int totalOrdered;
        String name;
        String address;
        public Order()
        {
            this.orderID = orderID;
            this.totalPrice = totalPrice;
            this.status = status;
            this.totalOrdered = totalOrdered;
            this.name = name;
            this.address = address;
        }

        public int getOrderID()
        {
            return orderID;
        }

        public double gettotalPrice()
        {
            return totalPrice;
        }

        public String getStatus()
        {
            return status;
        }
        public int getTotalOrdered()
        {
            return totalOrdered;
        }

        public String getName()
        {
            return name;
        }

        public String getAddress()
        {
            return address;
        }


        public static void insertNewOrder()
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO cordial_order (totalPrice, status, totalOrdered, date) VALUES (null, null, null, null)";
                string sql2 = "UPDATE cordial_order SET totalPrice=0, totalOrdered=0, date=CURDATE() WHERE date IS NULL";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sql2, conn);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        public static void addPizza(int id)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE cordial_order SET totalOrdered=totalOrdered+1, totalPrice=totalPrice+29.99 WHERE orderID=@id";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

        }

        public static int retrieveMaxOrderID()
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT MAX(orderID) FROM cordial_order";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    return Convert.ToInt32(myReader["MAX(orderID)"].ToString());
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            Console.WriteLine("Done");
            return 0;
        }
        public static string retrieveOrderPrice(int id)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM cordial_order WHERE orderID=@id";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    return myReader["totalPrice"].ToString();
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            Console.WriteLine("Done");
            return null;
        }

        public static void placeOrder(string name, string email, string address, int id)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE cordial_order SET name=@name, email=@email, address=@address, status = 'Preparin da Pizza!' WHERE orderID=@id";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        public static string retrieveOrderStatus(int id)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM cordial_order WHERE orderID=@id";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    return "Order #"+myReader["orderID"]+": "+myReader["status"].ToString();
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            Console.WriteLine("Done");
            return null;

        }

        public static string retrieveOrderHistory(string email)
        {
            StringBuilder order = new StringBuilder();

            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT o.orderId, o.date, o.status, o.totalOrdered, o.totalPrice, o.email" +
                    " FROM cordial_order o INNER JOIN cordial_member m ON o.email = m.email WHERE o.email = @email";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);

                using (MySqlDataReader myReader = cmd.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        order.AppendLine($"Order ID: {myReader.GetInt32("orderID")}");
                        order.AppendLine($"Order Date: {myReader.GetDateTime("date")}");
                        order.AppendLine($"Total: ${myReader.GetDecimal("totalPrice")}");
                        order.AppendLine($"Status: {myReader.GetString("status")}");
                        order.AppendLine($"Email: {myReader.GetInt32("email")}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            Console.WriteLine("Done");
            return order.ToString();
        }
    }
}
