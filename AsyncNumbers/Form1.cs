using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncNumbers
{
    public partial class Form1 : Form
    {

        decimal Numbers = 0;
        decimal SettedNumbers = 0;
        TimeSpan Speed = TimeSpan.FromMilliseconds(1);
        int NumberPatency = 1;

        public Form1()
        {
            InitializeComponent();
        }
        private void FormLoad(object sender, EventArgs e)
        {
            TextNumbers.Text = $"{Numbers}";
            numericUpDown1.Value = Numbers;
        }

        async void ChangeNumbers()
        {
                while (Numbers != SettedNumbers)
                {
                    if (Numbers <= SettedNumbers)
                    {
                        await Task.Delay(Speed);
                        Numbers += NumberPatency;
                    }
                    else if (Numbers >= SettedNumbers)
                    {
                        await Task.Delay(Speed);
                        Numbers -= NumberPatency;
                    }
                    setNumbersText();
                    
                }
                lockAndBlock(true);
                stoper.Enabled = false;
                
        }

        void setNumbersText()
        {
            TextNumbers.Text = $"{Numbers}";
        }

        private void Apply(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == Numbers)
            {
                MessageBox.Show("Введите число которе больше или ниже нынешнего!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lockAndBlock(false);
                SettedNumbers = numericUpDown1.Value;
                NumberPatency = Convert.ToInt32(patency.Value);
                Speed = TimeSpan.FromMilliseconds(Convert.ToDouble(speedMS.Value));
                label4.Text = "Скорость (сек): " + Speed.TotalSeconds;
                ChangeNumbers();
                stoper.Enabled = true;
            }
            
        }
        bool lockAndBlock(bool locked)
        {
            applyButton.Enabled = locked;
            numericUpDown1.Enabled = locked;
            speedMS.Enabled = locked;
            patency.Enabled = locked;
            return locked ;
        }

        private void StopChangeNumbers(object sender, EventArgs e)
        {
            SettedNumbers = Numbers;
            stoper.Enabled = false;
            lockAndBlock(true);
        }

        private void numbersTextBox_DoubleClick(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
        }
    }
}
