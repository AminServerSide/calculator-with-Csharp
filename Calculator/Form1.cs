using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double resultValue = 0;
        private string currentOperator = string.Empty;
        private bool isOperatorPerformed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonNumber_Click(object sender, EventArgs e)
        {
            // Numbers and decimal point
            if (textBox_Result.Text == "0" || isOperatorPerformed)
                textBox_Result.Clear();

            isOperatorPerformed = false;

            Button button = (Button)sender;
            if (button.Text == "." && textBox_Result.Text.Contains("."))
                return;

            textBox_Result.Text += button.Text;
        }

        private void buttonOperator_Click(object sender, EventArgs e)
        {
            // Operators: +, -, *, /
            Button button = (Button)sender;
            try
            {
                if (resultValue != 0)
                {
                    PerformCalculation();
                    currentOperator = button.Text;
                    label_Show_Op.Text = $"{resultValue} {currentOperator}";
                    isOperatorPerformed = true;
                }
                else
                {
                    currentOperator = button.Text;
                    resultValue = double.Parse(textBox_Result.Text);
                    label_Show_Op.Text = $"{resultValue} {currentOperator}";
                    isOperatorPerformed = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            // Clear current input
            textBox_Result.Text = "0";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Reset calculator
            textBox_Result.Text = "0";
            resultValue = 0;
            currentOperator = string.Empty;
            label_Show_Op.Text = string.Empty;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            // Perform calculation
            try
            {
                PerformCalculation();
                label_Show_Op.Text = string.Empty;
                resultValue = double.Parse(textBox_Result.Text);
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Cannot divide by zero!", "Math Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_Result.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformCalculation()
        {
            // Perform the selected operation
            switch (currentOperator)
            {
                case "+":
                    textBox_Result.Text = (resultValue + double.Parse(textBox_Result.Text)).ToString();
                    break;

                case "-":
                    textBox_Result.Text = (resultValue - double.Parse(textBox_Result.Text)).ToString();
                    break;

                case "*":
                    textBox_Result.Text = (resultValue * double.Parse(textBox_Result.Text)).ToString();
                    break;

                case "/":
                    if (double.Parse(textBox_Result.Text) == 0)
                        throw new DivideByZeroException();
                    textBox_Result.Text = (resultValue / double.Parse(textBox_Result.Text)).ToString();
                    break;

                default:
                    break;
            }
        }
    }
}
