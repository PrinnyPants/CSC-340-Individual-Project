using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza_Order_System
{
    public partial class Form1 : Form
    {
        double tax = 0;
        double price = 0;
        double totalPrice = 0;

        public Form1()
        {
            InitializeComponent();
            memPanel.Hide();
            menuPanel.Hide();
            checkPanel.Hide();
            accPanel.Hide();
            panel2.Hide();
            loginOH.Hide();
            orderTrack.Hide();
            orderHistory.Hide();
            textBox1.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox18.ReadOnly = true;
            pictureBox1.Show();
            pictureBox2.Show();
            int cart;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order.insertNewOrder();
            menuPanel.Show();
            memPanel.Hide();
            checkPanel.Hide();
            accPanel.Hide();
            panel2.Hide();
            loginOH.Hide();
            orderTrack.Hide();
            orderHistory.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            memPanel.Show();
            menuPanel.Hide();
            checkPanel.Hide();
            accPanel.Hide();
            panel2.Hide();
            loginOH.Hide();
            orderTrack.Hide();
            orderHistory.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label24.Text = "" + Order.retrieveOrderStatus(Order.retrieveMaxOrderID()) + "";
            memPanel.Hide();
            menuPanel.Hide();
            checkPanel.Hide();
            accPanel.Hide();
            panel2.Hide();
            loginOH.Hide();
            orderHistory.Hide();
            orderTrack.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            loginOH.Show();
            memPanel.Hide();
            menuPanel.Hide();
            checkPanel.Hide();
            accPanel.Hide();
            panel2.Hide();
            orderTrack.Hide();
            orderHistory.Hide();
            //label28.Text = "" + Order.retrieveOrderHistory() + "";
            //orderHistory.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            memPanel.Hide();
            menuPanel.Hide();
            checkPanel.Hide();
            accPanel.Hide();
            panel2.Show();
            loginOH.Hide();
            orderTrack.Hide();
            orderHistory.Hide();

            //accPanel.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            bool login = Member.login(textBox16.Text, textBox17.Text);
            if (login)
            {
                panel2.Hide();
                textBox1.Text = "" + Member.retrieveMemName(textBox16.Text, textBox17.Text) + "";
                textBox8.Text = "" + Member.retrieveMemAddress(textBox16.Text, textBox17.Text) + "";
                textBox7.Text = "" + Member.retrieveMemEmail(textBox16.Text, textBox17.Text) + "";
                textBox9.Text = "" + Member.retrieveMemPass(textBox16.Text, textBox17.Text) + "";
                textBox18.Text = "" + Member.retrieveMemID(textBox16.Text, textBox17.Text) + "";
                accPanel.Show();

            }
            else
            {
                string message = "The email or password is invalid. Please enter a valid email and password.";
                string title = "Error.";
                MessageBox.Show(message, title);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool login = Member.login(textBox21.Text, textBox20.Text);
            if (login)
            {
                loginOH.Hide();
                richTextBox1.Text = "" + Order.retrieveOrderHistory(textBox21.Text) + "";
                orderHistory.Show();
            }
            else
            {
                string message = "The email or password is invalid. Please enter a valid email and password.";
                string title = "Error.";
                MessageBox.Show(message, title);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool existence = Member.checkEmail(textBox4.Text, textBox5.Text);
            if(existence)
            {
                //textBox4.Text = "New Member";
                Member.displayExistenceError();
            }
            else
            {
                Member.insertNewMember(textBox15.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                string message = "Congratulations on successfully creating your ZZZ Pizza account!";
                string title = "Yippee!!";
                MessageBox.Show(message, title);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            memPanel.Hide();
            label1.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*tax = 0.90;
            price = 14.99;
            totalPrice = price + tax;
            label16.Text += "Cheese Pizza - $14.99\n";
            label19.Text += tax.ToString();
            label17.Text += totalPrice.ToString();*/
            Order.addPizza(Order.retrieveMaxOrderID());

        }
        private void button9_Click(object sender, EventArgs e)
        {
            /*tax = 0.90; ;
            price = 14.99;
            totalPrice = price + tax;
            label16.Text += "Pepperoni Pizza - $14.99\n";
            label19.Text += tax.ToString();
            label17.Text += totalPrice.ToString();*/
            Order.addPizza(Order.retrieveMaxOrderID());

        }

        private void button10_Click(object sender, EventArgs e)
        {
            menuPanel.Hide();
            label17.Text = "$" + Order.retrieveOrderPrice(Order.retrieveMaxOrderID());
            checkPanel.Show();
        }


        private void button11_Click(object sender, EventArgs e)
        {
            menuPanel.Hide();
            label1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            /*
            string message = "Please enter your payment information to checkout.";
            string title = "Error";
            MessageBox.Show(message, title);
            */
            if(textBox6.Text == "" || textBox10.Text == "" || textBox11.Text == "")
            {
                string eMessage = "Please enter your payment information.";
                string error = "Payment Error.";
                MessageBox.Show(eMessage, error);

            }
            else
            {
                bool existence = Payment.checkPayment(textBox6.Text, textBox12.Text, textBox13.Text);
                if (existence)
                {

                    Order.placeOrder(textBox12.Text, textBox19.Text, textBox13.Text, Order.retrieveMaxOrderID());
                }
                else
                {
                    //Payment.savePaymentInfo(textBox6.Text, textBox12.Text, textBox13.Text);
                    Payment.insertNewPaymentInfo();
                    Payment.savePaymentInfo(textBox6.Text, textBox12.Text, textBox13.Text);
                    Order.placeOrder(textBox12.Text, textBox19.Text, textBox13.Text, Order.retrieveMaxOrderID());
                }
                string message = "Thank you for your order! Your pizza is on its way!";
                string title = "Yippee!!";
                MessageBox.Show(message, title);
                checkPanel.Hide();
                panel1.Show();
                label1.Show();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            checkPanel.Hide();
            menuPanel.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;

            Member.saveMemDataToDatabase(textBox1.Text, textBox8.Text, textBox7.Text, textBox9.Text, textBox18.Text);

            string message = "Your information has been updated.";
            string title = "Yippee!!";
            MessageBox.Show(message, title);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox7.ReadOnly = false;
            textBox8.ReadOnly = false;
            textBox9.ReadOnly = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            accPanel.Hide();
            panel1.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            orderTrack.Hide();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            orderHistory.Hide();

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void orderTrack_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
