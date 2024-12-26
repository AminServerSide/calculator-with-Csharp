using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private string _operation = string.Empty;
        private double _result = 0;
        private bool _isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCalculatorButtons();
        }

        private void InitializeCalculatorButtons()
        {
            for (int i = 0; i <= 9; i++)
            {
                Button btn = new Button
                {
                    Text = i.ToString(),
                    Tag = i,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold),
                    Size = new System.Drawing.Size(50, 50),
                };
                btn.Click += NumberButton_Click;
                Controls.Add(btn);
            }

            string[] operators = { "+", "-", "*", "/" };
            foreach (string op in operators)
            {
                Button btn = new Button
                {
                    Text = op,
                    Tag = op,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold),
                    Size = new System.Drawing.Size(50, 50),
                };
                btn.Click += OperatorButton_Click;
                Controls.Add(btn);
            }

            // Special buttons
            Button btnEquals = new Button
            {
                Text = "=",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(50, 50),
            };
            btnEquals.Click += EqualsButton_Click;
            Controls.Add(btnEquals);

            Button btnClear = new Button
            {
                Text = "C",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(50, 50),
            };
            btnClear.Click += ClearButton_Click;
            Controls.Add(btnClear);
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (_isOperationPerformed)
            {
                textBox_Result.Text = string.Empty;
                _isOperationPerformed = false;
            }

            Button btn = sender as Button;
            if (textBox_Result.Text == "0")
                textBox_Result.Text = btn.Text;
            else
                textBox_Result.Text += btn.Text;
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (_result != 0)
            {
                btnEquals.PerformClick();
                _operation = btn.Text;
                _isOperationPerformed = true;
            }
            else
            {
                _operation = btn.Text;
                _result = double.Parse(textBox_Result.Text);
                _isOperationPerformed = true;
            }

            label_Show_Op.Text = $"{_result} {_operation}";
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            double secondOperand = double.Parse(textBox_Result.Text);

            switch (_operation)
            {
                case "+":
                    _result += secondOperand;
                    break;
                case "-":
                    _result -= secondOperand;
                    break;
                case "*":
                    _result *= secondOperand;
                    break;
                case "/":
                    _result /= secondOperand;
                    break;
            }

            textBox_Result.Text = _result.ToString();
            label_Show_Op.Text = string.Empty;
            _result = 0;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            _result = 0;
            _operation = string.Empty;
            label_Show_Op.Text = string.Empty;
        }
    }
}
