using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net.NetworkInformation;

namespace Ext_functions_sqlite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Data.SQLite.SQLiteConnection.CreateFile("mydb.txt");
        }
       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //IF button pressed
        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=mydb.txt");
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn);
            conn.Open();
            cmd.CommandText = textBox1.Text;
            cmd.ExecuteNonQuery();

            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            textBox2.Text = (reader[0].ToString());
            conn.Close();
        }

        //EXTERNAL FUNCTIONS-------------------------------------------------------------------------------------------------------------

        //PING
        [SQLiteFunction(Name = "PING", Arguments = 1, FuncType = FunctionType.Scalar)]
        class PING : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                Ping ping = new Ping();
                string ip = args[0].ToString();

                try
                {
                    PingReply reply = ping.Send(ip);
                    if (reply.Status == IPStatus.Success)
                    {
                        return 1;
                    }
                }
                catch (PingException e)
                {
                    MessageBox.Show(e.ToString());
                }

                return 0;
            }
        }

        //FAHRENHEIT TO CELSIUS
        [SQLiteFunction(Name = "F2C", Arguments = 1, FuncType = FunctionType.Scalar)]
        class F2C : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int fahrenheit = Convert.ToInt32(args[0].ToString());
                int celsius = (fahrenheit - 32) * 5 / 9;

                return celsius;
            }
        }

        //CELSIUS TO FAHRENHEIT
        [SQLiteFunction(Name = "C2F", Arguments = 1, FuncType = FunctionType.Scalar)]
        class C2F : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int fahrenheit = Convert.ToInt32(args[0].ToString());
                fahrenheit = (fahrenheit * 9) / 5 + 32;
                return fahrenheit;
            }
        }


        //BIANRY TO DECIMAL
        [SQLiteFunction(Name = "BIN2DEC", Arguments = 1, FuncType = FunctionType.Scalar)]
        class BIN2DEC : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                string value = args[0].ToString();
                return Convert.ToInt64(value, 2);
            }
        }

        //DECIMAL TO BINARY------------------------------------------------------------------
        [SQLiteFunction(Name = "DEC2BIN", Arguments = 1, FuncType = FunctionType.Scalar)]
        class DEC2BIN : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                return Convert.ToString(Convert.ToInt32(args[0].ToString()), 2);
            }
        }

        //DECIMAL TO HEX------------------------------------------------------------------
        [SQLiteFunction(Name = "DEC2HEX", Arguments = 1, FuncType = FunctionType.Scalar)]
        class DEC2HEX : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                int dec = Convert.ToInt32(args[0].ToString());
                string hex = dec.ToString("X");
                return hex;
            }

        }
        //HEX TO DECIMAL------------------------------------------------------------------
        [SQLiteFunction(Name = "HEX2DEC", Arguments = 1, FuncType = FunctionType.Scalar)]
        class HEX2DEC : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                string hex= args[0].ToString();
                int dec = Convert.ToInt32(hex, 16);
                return dec;
            }
        }

        //Factorial of
        [SQLiteFunction(Name = "Factorial", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Factorial : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                int factorial = 1;
                int numero = Convert.ToInt32(args[0].ToString());

                if (numero < 0)
                    return 0;
                else if (numero == 0)
                    return 1;
                else
                {
                    for (int i = 0; i < numero; numero--)
                    {
                        factorial = factorial * numero;
                    }
                }

                return factorial;
            }

        }

       //COMPARE STRING
        [SQLiteFunction(Name = "COMPARESTRING", Arguments = 1, FuncType = FunctionType.Scalar)]
        class COMPARESTRING : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                string first = args[0].ToString();
                string second= args[1].ToString();
                int comp = string.Compare(first, second);
                return comp;
            }
        }

//REPEAT
        [SQLiteFunction(Name = "REPEAT", Arguments = 2, FuncType = FunctionType.Scalar)]
        class REPEAT : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                String string1 = args[0].ToString(),result=" ";
                int num = Convert.ToInt32(args[1].ToString());
                for (int i = 0; i < num; i++)
                {
                    result+= " " + string1;
                }
       
                return result;
            }
        }



        //TRIM
        [SQLiteFunction(Name = "TRIM", Arguments = 2, FuncType = FunctionType.Scalar)]
        class TRIM : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                char string1 = Convert.ToChar(args[1].ToString());
                string string2 = args[0].ToString();
                string result = string2.Trim(string1);
                return result;
            }
        }






    }
}
