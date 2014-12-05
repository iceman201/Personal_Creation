using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Calculator
{
    public partial class Form1 : Form
    {
        Double value = 0;
        String operation = "";
        bool operation_pressed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Result.Text == "0") || (operation_pressed == true))
            {
                Result.Clear();
            }
            operation_pressed = false;
            Button b = (Button)sender;
            Result.Text = Result.Text + b.Text;

        }


        private void operator_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            operation = b.Text;
            value = Double.Parse(Result.Text);
            operation_pressed = true;
            label2.Text = value + " " + operation;
        }

        private void button_Equal_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            switch(operation){
                case "+":
                    Result.Text = (value + Double.Parse(Result.Text)).ToString();
                    break;
                case "-":
                    Result.Text = (value - Double.Parse(Result.Text)).ToString();
                    break;
                case "*":
                    Result.Text = (value * Double.Parse(Result.Text)).ToString();
                    break;
                case "/":
                    Result.Text = (value / Double.Parse(Result.Text)).ToString();
                    break;
                default:
                    break;
            }//end
        }
        private void button_C_Click(object sender, EventArgs e)
        {
            Result.Text = "0";
        }

        private void button_CE_Click(object sender, EventArgs e)
        {
            Result.Text = "0";
            value = 0;
        }


    }
}
