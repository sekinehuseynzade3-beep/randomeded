
using System;
using System.Drawing;
using System.Windows.Forms;

namespace random_number
{
    public partial class Form1 : Form
    {
        private int eded;
        private int cehdSayi;
        private bool oyunAktivdir;

        private readonly Random r = new Random();

        private GroupBox groupBox1 = null!;
        private Label label1 = null!;
        private TextBox textBox1 = null!;
        private Button button1 = null!;
        private Button yeniOyunbtn = null!;
        private RichTextBox neticeBox = null!;
        private Label cehdLabel = null!;
        private ErrorProvider errorProvider1 = null!;

        public Form1()
        {
            InitializeComponent();
            DizaynYarat();
        }

        private void DizaynYarat()
        {
            Text = "Random number";
            ClientSize = new Size(660, 390);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(65, 0, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            groupBox1 = new GroupBox
            {
                Text = "Ədəd axtarışı",
                ForeColor = Color.White,
                Location = new Point(27, 30),
                Size = new Size(280, 290)
            };
            Controls.Add(groupBox1);

            label1 = new Label
            {
                Text = "Axtarılan ədəd",
                ForeColor = Color.White,
                Location = new Point(35, 65),
                AutoSize = true
            };
            groupBox1.Controls.Add(label1);

            textBox1 = new TextBox
            {
                Location = new Point(30, 125),
                Size = new Size(220, 27),
                Enabled = false,
                MaxLength = 3
            };
            textBox1.KeyPress += textBox1_KeyPress;
            groupBox1.Controls.Add(textBox1);

            button1 = new Button
            {
                Text = "Yoxla",
                Location = new Point(30, 185),
                Size = new Size(220, 30),
                Enabled = false
            };
            button1.Click += button1_Click;
            groupBox1.Controls.Add(button1);

            neticeBox = new RichTextBox
            {
                Location = new Point(350, 40),
                Size = new Size(280, 220),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Arial", 11)
            };
            Controls.Add(neticeBox);

            cehdLabel = new Label
            {
                Text = "Cəhd sayı: 0",
                ForeColor = Color.White,
                Location = new Point(350, 270),
                AutoSize = true
            };
            Controls.Add(cehdLabel);

            yeniOyunbtn = new Button
            {
                Text = "Yeni oyun",
                Location = new Point(350, 300),
                Size = new Size(280, 30)
            };
            yeniOyunbtn.Click += yeniOyunbtn_Click;
            Controls.Add(yeniOyunbtn);

            errorProvider1 = new ErrorProvider();
            errorProvider1.ContainerControl = this;
        }

        private void yeniOyunbtn_Click(object sender, EventArgs e)
        {
            eded = r.Next(0, 101);
            cehdSayi = 0;
            oyunAktivdir = true;

            textBox1.Clear();
            neticeBox.Clear();
            cehdLabel.Text = "Cəhd sayı: 0";

            errorProvider1.SetError(textBox1, "");

            textBox1.Enabled = true;
            button1.Enabled = true;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!oyunAktivdir)
                return;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(
                    textBox1, "Ədəd daxil edin!");
                textBox1.Focus();
                return;
            }

            if (!int.TryParse(textBox1.Text, out int daxilEdilen)
                || daxilEdilen < 0 || daxilEdilen > 100)
            {
                errorProvider1.SetError(
                    textBox1, "0-100 arasında ədəd daxil edin!");
                textBox1.Focus();
                return;
            }

            errorProvider1.SetError(textBox1, "");

            cehdSayi++;
            cehdLabel.Text = "Cəhd sayı: " + cehdSayi;

            if (daxilEdilen == eded)
            {
                neticeBox.AppendText("Təbriklər! Onu qazandınız!\n");
                neticeBox.AppendText("Cəhd sayı: " + cehdSayi);

                oyunAktivdir = false;
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                neticeBox.AppendText("Daxil edilmiş səhvdir.\n");

                if (daxilEdilen < eded)
                {
                    neticeBox.AppendText(
                        "Təsadüfi ədəddən kiçikdir.\n");
                }
                else
                {
                    neticeBox.AppendText(
                        "Təsadüfi ədəddən böyükdür.\n");
                }

                neticeBox.AppendText(
                    "Cəhd sayı: " + cehdSayi + "\n\n");

                textBox1.SelectAll();
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
