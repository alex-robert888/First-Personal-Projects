using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Operations_and_Conversions_Calculator.Classes;

namespace Operations_and_Conversions_Calculator
{
    public partial class Form1 : Form
    {
        private bool waiting_for_second_operand;
        private string first_operand;
        private string second_operand;
        private int numeric_base;
        private int source_base;
        private int destination_base;
        private string result;
        private bool waiting_for_number_to_be_converted;
        private string number_to_convert;

        private List<Button> list_buttons = new List<Button>();

        private enum AppState{
            OPERATIONS,
            CONVERSIONS
        }

        AppState current_app_state = AppState.OPERATIONS;

        public Form1()
        {
            InitializeComponent();

            Init_Buttons();

            numeric_base = 10;
            waiting_for_second_operand = false;
            waiting_for_number_to_be_converted = true;
            this.Width = panel_MainMenu.Width;
        }

        private void Init_Buttons()
        {
            list_buttons.Add(button0);
            list_buttons.Add(button1);
            list_buttons.Add(button2);
            list_buttons.Add(button3);
            list_buttons.Add(button4);
            list_buttons.Add(button5);
            list_buttons.Add(button6);
            list_buttons.Add(button7);
            list_buttons.Add(button8);
            list_buttons.Add(button9);
            list_buttons.Add(button10);
            list_buttons.Add(button11);
            list_buttons.Add(button12);
            list_buttons.Add(button13);
            list_buttons.Add(button14);
            list_buttons.Add(button15);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_digit_click(object sender, EventArgs e)
        {
            Button current_button = (Button)sender;
            textBox_Display.Text += current_button.Text;
        }

        private void Label_Clear_Click(object sender, EventArgs e)
        {
            textBox_Display.Text = "";
        }

        private void Button_Equal_Click(object sender, EventArgs e)
        {
            if (current_app_state == AppState.OPERATIONS)
            {
                if (string.IsNullOrWhiteSpace(textBox_Display.Text) == false && waiting_for_second_operand == true)
                {
                    second_operand = textBox_Display.Text;

                    result = Operations.Execute_Operation(buttonOperation.Text, first_operand, second_operand, numeric_base);
                    if (buttonOperation.Text == "/")
                    {
                        result += ",r:" + Operations.string_remainder;
                    }
                    textBox_Display.Text = result;
                    waiting_for_second_operand = false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox_Display.Text) == false && waiting_for_number_to_be_converted == true)
                {
                    number_to_convert = textBox_Display.Text;

                    result = Conversions.ExecuteConversion(buttonOperation.Text, number_to_convert, source_base, destination_base);
                    textBox_Display.Text = result;

                    waiting_for_number_to_be_converted = true;
                }
            }
            
        }

        private void ButtonOperation_Click(object sender, EventArgs e)
        {
            if (current_app_state == AppState.CONVERSIONS)
            {
                MessageBox.Show(label_DisplayBase.Text + "\n In order to perform the conversion, please press the equal button :)");
            }

            if (string.IsNullOrWhiteSpace(textBox_Display.Text) == false)
            {
                waiting_for_second_operand = true;
                first_operand = textBox_Display.Text;
                textBox_Display.Text = "";
            }
        }

        private void Panel_Calculator_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button_ChangeBase_Click(object sender, EventArgs e)
        {
            this.Width = panel_MainMenu.Width + panel_Calculator.Width + panel_ChangeBase.Width;
        }

        private void TriangularButtonDown_Click(object sender, EventArgs e)
        {
            Button sndr = (Button)sender;
            string label_text = "";
            if (current_app_state == AppState.OPERATIONS) label_text = label_Base.Text;
            else
            {
                if (sndr.Name == "triangularButtonDown_DestinationBase") label_text = label_DestinationBase.Text;        
                else label_text = label_SourceBase.Text;        
            }

            int val = Convert.ToInt32(label_text);

            if (val == 16) val = 10;
            else if (val == 2) val = 16;
            else --val;

            label_text = val.ToString();

            if (label_text != "10" && label_text != "16")
            {
                string txt = label_text;
                label_text = "0" + txt;
            }

            if (current_app_state == AppState.OPERATIONS) label_Base.Text = label_text;
            else
            {
                if (sndr.Name == "triangularButtonDown_DestinationBase") label_DestinationBase.Text = label_text;
                else label_SourceBase.Text = label_text;
            }
        }

        private void TriangularButtonUp_Click(object sender, EventArgs e)
        {
            Button sndr = (Button)sender;
            string label_text = "";
            if (current_app_state == AppState.OPERATIONS) label_text = label_Base.Text;
            else
            {
                if (sndr.Name == "triangularButtonUp_DestinationBase") label_text = label_DestinationBase.Text;
                else label_text = label_SourceBase.Text;
            }

            int val = Convert.ToInt32(label_text);

            if (val == 16) val = 2;
            else if (val == 10) val = 16;
            else ++val;

            label_text = val.ToString();

            if (label_text != "10" && label_text != "16")
            {
                string txt = label_text;
                label_text = "0" + txt;
            }

            if (current_app_state == AppState.OPERATIONS) label_Base.Text = label_text;
            else
            {
                if (sndr.Name == "triangularButtonUp_DestinationBase") label_DestinationBase.Text = label_text;
                else label_SourceBase.Text = label_text;
            }
        }

        private void Label_Base_Click(object sender, EventArgs e)
        {

        }

        private void Label_Base_TextChanged(object sender, EventArgs e)
        {
        }

        public void Disable_Invalid_Digits(int from)
        {
            for (int i = 0; i < from; ++i)
            {
                list_buttons[i].Enabled = true;
                list_buttons[i].BackColor = Color.MediumSpringGreen;
            }

            for(int i = from; i <= 15; ++i)
            {
                list_buttons[i].Enabled = false;
                list_buttons[i].BackColor = Color.LightGreen;
            }
        }

        private void Button_Apply_Click(object sender, EventArgs e)
        {
            if (current_app_state == AppState.OPERATIONS)
            {
                numeric_base = Convert.ToInt32(label_Base.Text);
                Disable_Invalid_Digits(numeric_base);
                label_DisplayBase.Text = "Base " + label_Base.Text;
            }
            else
            {
                source_base = Convert.ToInt32(label_SourceBase.Text);
                Disable_Invalid_Digits(source_base);
                destination_base = Convert.ToInt32(label_DestinationBase.Text);
                label_DisplayBase.Text = "Base " + label_SourceBase.Text + " => Base " + label_DestinationBase.Text; 
            }

            this.Width = panel_MainMenu.Width + panel_Calculator.Width;
        }

        private void Button_MainMenuOperation_Click(object sender, EventArgs e)
        {
            waiting_for_second_operand = false;
            numeric_base = 10;
            Disable_Invalid_Digits(numeric_base);
            current_app_state = AppState.OPERATIONS;
            this.Width = panel_MainMenu.Width + panel_Calculator.Width;

            Button sndr = (Button)sender;

            if (sndr.Text == "Addition") buttonOperation.Text = "+";
            if (sndr.Text == "Substraction") buttonOperation.Text = "-";
            if (sndr.Text == "Multiplication") buttonOperation.Text = "*";
            if (sndr.Text == "Division") buttonOperation.Text = "/";

            this.label_DisplayBase.Text = "Base 10";
            panel_2Bases.Hide();
        }

        private void Button_MainMenuConversion(object sender, EventArgs e)
        {
            waiting_for_number_to_be_converted = true;
            source_base = 10;
            Disable_Invalid_Digits(numeric_base);
            destination_base = 2;
            current_app_state = AppState.CONVERSIONS;
            this.Width = panel_MainMenu.Width + panel_Calculator.Width;
            Button sndr = (Button)sender;
            buttonOperation.Text = sndr.Text;

            this.label_DisplayBase.Text = "Base 10 => Base 2";
            panel_2Bases.Show();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            this.Width = panel_MainMenu.Width;
        }


        private void Label11_Click(object sender, EventArgs e)
        {

        }


        private void Label9_Click(object sender, EventArgs e)
        {

        }


        private void Panel_ChangeBase_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button22_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_Display.Text) == false)
            {
                textBox_Display.Text = textBox_Display.Text.Remove(textBox_Display.Text.Length - 1);
            }
        }
    }
}
