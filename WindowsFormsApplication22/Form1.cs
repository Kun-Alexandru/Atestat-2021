using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int codClientNou, suma, s, ver, verComanda, codComandaNou=44, codDetaliuNou=5, verDetaliu, codDetaliuInitial, codComandaInitial, aFostAnulatCom;
        int coefDetalii = 5, coefComanda = 44;

        private void clientiBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientiBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.atestatMagazinDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atestatMagazinDataSet.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'atestatMagazinDataSet.Produse' table. You can move, or remove it, as needed.
            this.produseTableAdapter.Fill(this.atestatMagazinDataSet.Produse);
            // TODO: This line of code loads data into the 'atestatMagazinDataSet.Furnizori' table. You can move, or remove it, as needed.
            this.furnizoriTableAdapter.Fill(this.atestatMagazinDataSet.Furnizori);
            // TODO: This line of code loads data into the 'atestatMagazinDataSet.Detalii' table. You can move, or remove it, as needed.
            this.detaliiTableAdapter.Fill(this.atestatMagazinDataSet.Detalii);
            // TODO: This line of code loads data into the 'atestatMagazinDataSet.Comenzi' table. You can move, or remove it, as needed.
            this.comenziTableAdapter.Fill(this.atestatMagazinDataSet.Comenzi);
            // TODO: This line of code loads data into the 'atestatMagazinDataSet.Clienti' table. You can move, or remove it, as needed.
            this.clientiTableAdapter.Fill(this.atestatMagazinDataSet.Clienti);
            textBox5.PasswordChar = '*';

            suma = 0;
            s = 0;
            ver = 0;
            verComanda = 0;
            verDetaliu = 0;
            aFostAnulatCom = 0;

            DataTable dt = this.atestatMagazinDataSet.Clienti;
            codClientNou = dt.Rows.Count;


            codComandaNou = (int)this.comenziTableAdapter.ScalarQueryMaxIdComanda();
            codDetaliuNou = (int)this.detaliiTableAdapter.ScalarQueryMaxIdDetalii();
            this.comenziTableAdapter.UpdateQueryComenzi("Finalizat", 22);

        }


        private void unuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void doiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void treiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox4.Text != "admin" | textBox5.Text != "password")
                MessageBox.Show("DATE INCORECTE");
            else tabControl1.SelectedTab = tabPage5;
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a;
            a = (int)this.clientiTableAdapter.ScalarQueryESTEclientNOU(textBox1.Text, textBox2.Text, textBox3.Text);
            DataTable dt = this.atestatMagazinDataSet.Clienti;
            if (a == 0)
            {
                if (ver == 0)
                {
                    codClientNou = dt.Rows.Count + 1;
                    ver++;
                }
                else codClientNou = codClientNou + 1;
                MessageBox.Show("CLIENT NOU " + codClientNou);
                this.clientiTableAdapter.InsertQueryClientNou(codClientNou, textBox1.Text, textBox2.Text, textBox3.Text);
            }
            else
            {
                codClientNou = (int)this.clientiTableAdapter.ScalarQueryGasireIDCLIENT(textBox1.Text, textBox2.Text, textBox3.Text);
                MessageBox.Show("BINE ATI REVENIT! " + codClientNou.ToString());
            }

            codComandaNou = (codComandaNou / 11 + 1) * 11;

            this.comenziTableAdapter.InsertQueryComanda(codComandaNou, null, "Card", "Procesare", codClientNou);

            tabControl1.SelectedTab = tabPage4;
            richTextBox2.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            aFostAnulatCom = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            int a;
            a = (int)this.produseTableAdapter.ScalarQueryuu();
            richTextBox1.Text += Convert.ToString(a);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.produseTableAdapter.FillByNuAvem(this.atestatMagazinDataSet.Produse);
            DataTable dt = this.atestatMagazinDataSet.Produse;
            richTextBox1.Clear();
            richTextBox1.Text = "Denumire " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["denumire"] + "\n";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.comenziTableAdapter.FillByMlsg(this.atestatMagazinDataSet.Comenzi);
            DataTable dt = this.atestatMagazinDataSet.Comenzi;
            richTextBox1.Clear();
            richTextBox1.Text = "idComanda / Suma comanda " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["idComanda"].ToString() + " " + dt.Rows[i]["SumaComanda"].ToString() + "\n";

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.comenziTableAdapter.FillByNuEFinalizat(this.atestatMagazinDataSet.Comenzi);
            DataTable dt = this.atestatMagazinDataSet.Comenzi;
            richTextBox1.Clear();
            richTextBox1.Text = "ID comanda " + " \n ";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["idComanda"].ToString() + "\n";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.detaliiTableAdapter.FillByCeleMaiVanduteDESC(this.atestatMagazinDataSet.Detalii);
            DataTable dt = this.atestatMagazinDataSet.Detalii;
            richTextBox1.Clear();
            richTextBox1.Text = "Denumire / nrVanzari " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["denumire"].ToString() + " " + dt.Rows[i]["NrVanzari"].ToString() + "\n";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            int a;
            a = (int)this.detaliiTableAdapter.ScalarQueryAparitieX(textBox6.Text);
            richTextBox1.Text += Convert.ToString(a);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.produseTableAdapter.FillByFurnizoriProdusX(this.atestatMagazinDataSet.Produse, textBox6.Text);
            DataTable dt = this.atestatMagazinDataSet.Produse;
            richTextBox1.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["numeFurnizor"].ToString() + "\n";
        }

        private void button14_Click(object sender, EventArgs e)
        {


        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            this.produseTableAdapter.Fill(this.atestatMagazinDataSet.Produse);
            DataTable dt = this.atestatMagazinDataSet.Produse;
            richTextBox1.Clear();
            richTextBox1.Text = "Denumire / Pret / Stoc " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["denumire"].ToString() + " / " + dt.Rows[i]["pret"].ToString() + " / " + dt.Rows[i]["stocCurent"].ToString() + "\n";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button17_Click(object sender, EventArgs e)
        {

            DataTable mp = this.atestatMagazinDataSet.Detalii;
            codDetaliuInitial = mp.Rows.Count;
            if (radioButton1.Checked == true)
            {
                richTextBox2.Text += " i7 8700K " + " x " + numericUpDown1.Value + " = " + (1000 * numericUpDown1.Value).ToString() + "\n";
                s = s + 1000 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();

                codDetaliuNou = codDetaliuNou + 1;

                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 1, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
               
            }
            else if (radioButton2.Checked == true)
            {
                richTextBox2.Text += " Ryzen 5 3600 " + " x " + numericUpDown1.Value + " = " + (900 * numericUpDown1.Value).ToString() + "\n";
                s = s + 900 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();

                codDetaliuNou = codDetaliuNou + 1;
                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 2, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
                
            }
            else if (radioButton3.Checked == true)
            {
                richTextBox2.Text += " i5 10400 " + " x " + numericUpDown1.Value + " = " + (550 * numericUpDown1.Value).ToString() + "\n";
                s = s + 550 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();
                codDetaliuNou = codDetaliuNou + 1;
                    this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 4, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
               
            }
            else if (radioButton4.Checked == true)
            {
                richTextBox2.Text += " i3 10300 " + " x " + numericUpDown1.Value + " = " + (400 * numericUpDown1.Value).ToString() + "\n";
                s = s + 400 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();
                codDetaliuNou = codDetaliuNou + 1;
                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 5, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
               
            }
            else if (radioButton5.Checked == true)
            {
                richTextBox2.Text += " 2060 SUPER " + " x " + numericUpDown1.Value + " = " + (2500 * numericUpDown1.Value).ToString() + "\n";
                s = s + 2500 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();
                codDetaliuNou = codDetaliuNou + 1;

                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 3, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
            }
            else if (radioButton6.Checked == true)
            {
                richTextBox2.Text += " RX 580 " + " x " + numericUpDown1.Value + " = " + (2200 * numericUpDown1.Value).ToString() + "\n";
                s = s + 2200 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();

                codDetaliuNou = codDetaliuNou + 1;
                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 7, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
                
            }
            else if (radioButton7.Checked == true)
            {
                richTextBox2.Text += " 3060 TI " + " x " + numericUpDown1.Value + " = " + (3000 * numericUpDown1.Value).ToString() + "\n";
                s = s + 3000 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();
               codDetaliuNou = codDetaliuNou + 1;
                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 6, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
                
            }
            else if (radioButton8.Checked == true)
            {
                richTextBox2.Text += " B450 - PLUS " + " x " + numericUpDown1.Value + " = " + (300 * numericUpDown1.Value).ToString() + "\n";
                s = s + 300 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();
                codDetaliuNou = codDetaliuNou + 1;
                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 8, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
               
            }
            else if (radioButton9.Checked == true)
            {
                richTextBox2.Text += " Z590 " + " x " + numericUpDown1.Value + " = " + (800 * numericUpDown1.Value).ToString() + "\n";
                s = s + 800 * (int)numericUpDown1.Value;
                textBox7.Text = s.ToString();
                codDetaliuNou = codDetaliuNou + 1;
                    
                
                this.detaliiTableAdapter.InsertQueryDetalii(codDetaliuNou, 9, codComandaNou, Convert.ToInt32(numericUpDown1.Value));
                
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            richTextBox2.Text = "";
            s = 0;
            suma = 0;
            this.detaliiTableAdapter.DeleteQueryDetalii(codComandaNou);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("VA MULTUMIM CA ATI COMANDAT DE LA NOI!!");
            tabControl1.SelectedTab = tabPage1;
            s = 0;
            suma = 0;

            this.comenziTableAdapter.UpdateQueryComenzi("Finalizat", codComandaNou);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.clientiTableAdapter.Fill(this.atestatMagazinDataSet.Clienti);
            DataTable dt = this.atestatMagazinDataSet.Clienti;
            richTextBox1.Clear();
            richTextBox1.Text = "Nume / Prenume / Email " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["nume"].ToString() + " / " + dt.Rows[i]["prenume"].ToString() + " / " + dt.Rows[i]["email"].ToString() + "\n";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.comenziTableAdapter.FillByNumePren(this.atestatMagazinDataSet.Comenzi);
            DataTable dt = this.atestatMagazinDataSet.Comenzi;
            richTextBox1.Clear();
            richTextBox1.Text = "idComanda / stareComanda / Nume " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["idComanda"].ToString() + " / " + dt.Rows[i]["stareComanda"].ToString() + " / "  + dt.Rows[i]["nume"].ToString() + " " + dt.Rows[i]["prenume"].ToString() + "\n";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.detaliiTableAdapter.FillByMSS(this.atestatMagazinDataSet.Detalii);
            DataTable dt = this.atestatMagazinDataSet.Detalii;
            richTextBox1.Clear();
            richTextBox1.Text = "idComanda / denumire / cantitate " + "\n";
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text +=  dt.Rows[i]["idComanda"].ToString() + " / " + dt.Rows[i]["denumire"].ToString() + " / " + dt.Rows[i]["cantitate"].ToString() + "\n";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.detaliiTableAdapter.DeleteQueryDetalii(codComandaNou);
            tabControl1.SelectedTab = tabPage1;
            s = 0;
            suma = 0;
        }
    }
}
