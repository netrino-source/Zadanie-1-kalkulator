using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kaukulator
{
    public partial class Form1 : Form
    {
        double pierwszaLiczba = 0.0;
        string operacja = "";
        bool nowaLiczba = true; // Flaga do resetowania wyświetlacz
        bool gotowyDoObliczenia = false;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        Kalkulator kalkulator = new Kalkulator();// instalacja klasy klaukulaotr

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode; // Zmienna przechowująca wciśnięty kod klawisza
            e.Handled = true;
            // OBSŁUGA CYFR (Klawiatura alfanumeryczna i NumPad)
            if (keyCode == Keys.D0 || keyCode == Keys.NumPad0)
            {
                this.button10.PerformClick();
            }
            else if (keyCode == Keys.D1 || keyCode == Keys.NumPad1)
            {
                this.button1.PerformClick();
            }
            else if (keyCode == Keys.D2 || keyCode == Keys.NumPad2)
            {
                this.button2.PerformClick();
            }
            else if (keyCode == Keys.D3 || keyCode == Keys.NumPad3)
            {
                this.button3.PerformClick();
            }
            else if (keyCode == Keys.D4 || keyCode == Keys.NumPad4)
            {
                this.button4.PerformClick();
            }
            else if (keyCode == Keys.D5 || keyCode == Keys.NumPad5)
            {
                this.button5.PerformClick();
            }
            else if (keyCode == Keys.D6 || keyCode == Keys.NumPad6)
            {
                this.button6.PerformClick();
            }
            else if (keyCode == Keys.D7 || keyCode == Keys.NumPad7)
            {
                this.button7.PerformClick();
            }
            else if (keyCode == Keys.D8 || keyCode == Keys.NumPad8)
            {
                this.button8.PerformClick();
            }
            else if (keyCode == Keys.D9 || keyCode == Keys.NumPad9)
            {
                this.button9.PerformClick();
            }
            //OBSŁUGA OPERATORÓW
            else if (keyCode == Keys.Add || (e.Shift && keyCode == Keys.Oemplus))
            {
                this.button11.PerformClick(); // Dodawanie (+)
            }
            else if (keyCode == Keys.Subtract || keyCode == Keys.OemMinus)
            {
                this.button12.PerformClick(); // Odejmowanie (-)
            }
            else if (keyCode == Keys.Multiply || (e.Shift && keyCode == Keys.D8))
            {
                this.button14.PerformClick(); // Obsługa '*'
            }
            else if (keyCode == Keys.Divide || keyCode == Keys.OemQuestion) // Dzielenie
            {
                this.button15.PerformClick();
            }
            else if (keyCode == Keys.Decimal || keyCode == Keys.OemPeriod || keyCode == Keys.Oemcomma) // Obsługa separatora 
            {
                this.button_kropka.PerformClick(); // Obsługa separatora
            }
            //OBSŁUGA RÓWNA SIĘ I Backspace
            else if (keyCode == Keys.Enter || keyCode == Keys.Oemplus)
            {
                this.button13.PerformClick(); // Obsługa '='
            }
            else if (keyCode == Keys.Back)
            {
                this.backspace_Click(sender, e); // Backspace
            }
            else if (keyCode == Keys.C)
            {
                this.button_clear_Click(sender, e); // Clear (C)
            }
            else
            {
                e.Handled = false; // Jeśli klawisz nie został obsłużony
            }
        }

        private void button11_Click(object sender, EventArgs e) // Obsługa przycisku '+'
        {
            if (wyswietlacz_wyniku.Text.Length == 0)
            {
                return;
            }
            if (operacja != "" && !gotowyDoObliczenia)
            {
                return; // Ochrona przed podwójnym kliknięciem operatora
            }
            try
            {
                bool obliczenieWykonane = false;

                if (gotowyDoObliczenia) //Wykonaj zaległe obliczenie
                {
                    button13_Click(sender, e);
                    obliczenieWykonane = true;
                }
                if (!obliczenieWykonane)
                {
                    pierwszaLiczba = double.Parse(wyswietlacz_wyniku.Text);
                }
                operacja = "+";
                nowaLiczba = true;
                gotowyDoObliczenia = true;
            }
            catch (FormatException)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("BŁĄD DANYCH");
                return;
            }
            catch (Exception)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR");
                return;
            }
        }

        private void button13_Click(object sender, EventArgs e) // Obsługa przycisku '='
        {
            double wynik = 0.0;
            double drugaLiczba = 0.0;
            if (operacja == "")
            {
                nowaLiczba = true;
                return;
            }
            if (nowaLiczba && gotowyDoObliczenia)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("OCZEKIWANO DRUGIEJ LICZBY");
                return;
            }
            try
            {
                if (wyswietlacz_wyniku.Text.Length > 0)
                {
                    drugaLiczba = double.Parse(wyswietlacz_wyniku.Text);
                }
                if (operacja == "+" || operacja == "-")
                {
                    wynik = kalkulator.Dodaj(pierwszaLiczba, drugaLiczba);
                }
                else if (operacja == "*")
                {
                    wynik = kalkulator.Mnoz(pierwszaLiczba, drugaLiczba);
                }
                else if (operacja == "/")
                {
                    wynik = kalkulator.Dziel(pierwszaLiczba, drugaLiczba);
                }
                wyswietlacz_wyniku.Text = wynik.ToString();

                operacja = "";
                gotowyDoObliczenia = false;
                nowaLiczba = true;
                double.TryParse(wyswietlacz_wyniku.Text, out pierwszaLiczba);
            }
            catch (DivideByZeroException ex)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR: " + ex.Message);
                return;
            }
            catch (ArgumentException ex)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR: " + ex.Message);
                return;
            }
            catch (FormatException)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR: Zły format danych");
                return;
            }
            catch (Exception)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("BŁĄD OBLICZENIA");
                return;
            }
        }
        private void buttonCyfra_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender; // Pobierz cyfrę z przycisku
            string cyfra = b.Text;
            if (nowaLiczba) // 1. Obsługa resetowania wyświetlacza dla nowej liczby
            {
                if (operacja == "-") // Jeśli tak, to oznacza, że będziemy wpisywać drugą liczbę ujemną.
                {
                    wyswietlacz_wyniku.Text = "-";
                }
                else
                {
                    wyswietlacz_wyniku.Text = "";
                }
                nowaLiczba = false;
            }
            if (wyswietlacz_wyniku.Text == "0" && cyfra != ",")   // Zapobieganie wpisaniu "0" przed cyfrą (chyba że to jest 0 przed przecinkiem)
            {
                wyswietlacz_wyniku.Text = cyfra; // Zastąp zero pierwszą cyfrą
                return;
            }
            if (wyswietlacz_wyniku.Text == "-")
            {
                wyswietlacz_wyniku.Text += cyfra;
            }
            else
            {
                wyswietlacz_wyniku.Text += cyfra;
            }
        }

        private void button_kropka_Click(object sender, EventArgs e)
        {
            {
                string separator = ",";
                if (!wyswietlacz_wyniku.Text.Contains(separator))  // Sprawdzamy, czy w wyświetlaczu znajduje się już separator
                {
                    if (wyswietlacz_wyniku.Text.Length == 0) // Jeśli wyświetlacz jest pusty, dodajemy "0."
                    {
                        wyswietlacz_wyniku.Text = "0" + separator;
                    }
                    else
                    {
                        wyswietlacz_wyniku.Text += separator; // Dodajemy separator, jeśli jeszcze go tam nie ma
                    }
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            wyswietlacz_wyniku.Text = "";  // Resetowanie wyświetlacza
            pierwszaLiczba = 0.0; // Resetowanie stanu kalkulatora
            operacja = "";
            nowaLiczba = true;
        }

        private void button12_Click(object sender, EventArgs e) // Obsługa przycisku '-' 
        {
            try
            {
                if (operacja != "" && nowaLiczba && wyswietlacz_wyniku.Text.Length > 0) // 1. Logika wprowadzania liczby ujemnej po operatorze
                {
                    wyswietlacz_wyniku.Text = "-";
                    nowaLiczba = false; // Następne cyfry będą dopisywane po minusie
                    return; // Zakończ, unikając całej logiki operatora
                }
                if (wyswietlacz_wyniku.Text.Length == 0 || wyswietlacz_wyniku.Text == "-")  // 2. Logika wprowadzania pierwszej liczby ujemnej (np. -5)
                {
                    if (wyswietlacz_wyniku.Text.Length == 0)
                    {
                        wyswietlacz_wyniku.Text = "-";
                        nowaLiczba = false;
                    }
                    return;
                }
                if (operacja != "" && !gotowyDoObliczenia) // 3. Ochrona przed podwójnym kliknięciem operatora
                {
                    return;
                }
                if (gotowyDoObliczenia) // 4. Obsługa obliczeń ciągłych
                {
                    button13_Click(sender, e);
                }
                pierwszaLiczba = double.Parse(wyswietlacz_wyniku.Text);
                operacja = "-";
                nowaLiczba = true;
                gotowyDoObliczenia = true;
            }
            catch (FormatException)
            {
                this.button_clear_Click(sender, e); // Resetuj stan kalkulatora
                MessageBox.Show("BŁĄD DANYCH"); // Wyświetl komunikat błędu
                return;
            }
        }

        private void button14_Click(object sender, EventArgs e) // Obsługa przycisku '*'
        {
            if (wyswietlacz_wyniku.Text.Length == 0) return;
            if (operacja != "" && !gotowyDoObliczenia) return;
            try
            {
                bool obliczenieWykonane = false;
                if (gotowyDoObliczenia)// Obsługa obliczeń ciągłych
                {
                    button13_Click(sender, e);  // Wykonaj zaległe obliczenie (wynik staje się nową pierwszaLiczba)
                    obliczenieWykonane = true;
                }
                if (!obliczenieWykonane)
                {
                    pierwszaLiczba = double.Parse(wyswietlacz_wyniku.Text);
                }
                operacja = "*";
                nowaLiczba = true;
                gotowyDoObliczenia = true;
            }
            catch (FormatException)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("BŁĄD DANYCH");
                return;
            }
            catch (Exception)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR");
                return;
            }
        }

        private void button15_Click(object sender, EventArgs e) // Obsługa przycisku '/'
        {
            if (wyswietlacz_wyniku.Text.Length == 0) return;
            if (operacja != "" && !gotowyDoObliczenia) return;
            try
            {
                bool obliczenieWykonane = false;
                if (gotowyDoObliczenia) // Obsługa obliczeń ciągłych
                {
                    button13_Click(sender, e);
                    obliczenieWykonane = true;
                }
                if (!obliczenieWykonane) //zabezpieczenie przed formatexception
                {
                    pierwszaLiczba = double.Parse(wyswietlacz_wyniku.Text);
                }
                operacja = "/"; // Ustawiamy operator DZIELENIA
                nowaLiczba = true;
                gotowyDoObliczenia = true;
            }

            catch (FormatException)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("BŁĄD DANYCH");
                return;
            }
            catch (Exception)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR");
                return;
            }
        }

        private void pierwiastek_Click(object sender, EventArgs e)
        {
            if (wyswietlacz_wyniku.Text.Length == 0)
            {
                return;
            }
            double aktualnaLiczba;
            try
            {
                aktualnaLiczba = double.Parse(wyswietlacz_wyniku.Text);
                double wynik = kalkulator.pierwiastek(aktualnaLiczba);
                wyswietlacz_wyniku.Text = wynik.ToString();
                operacja = "";
                gotowyDoObliczenia = false;
                nowaLiczba = true;
                pierwszaLiczba = wynik;
            }
            catch (FormatException)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("BŁĄD DANYCH");
            }
            catch (ArgumentException ex)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("ERROR: " + ex.Message);
            }
            catch (Exception)
            {
                this.button_clear_Click(sender, e);
                MessageBox.Show("GLOBAL ERROR");
            }
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            if (wyswietlacz_wyniku.Text.Length > 0)
            {
                wyswietlacz_wyniku.Text = wyswietlacz_wyniku.Text.Substring(0, wyswietlacz_wyniku.Text.Length - 1); // Usuwamy ostatni znak z tekstu
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            panel_analog.Invalidate();

        }

        private void combotheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (kolor.SelectedItem.ToString())
            {
                case "niebieski":
                    this.BackColor = Color.Blue;
                    panel_analog.BackColor = Color.Blue;
                    label1.ForeColor = Color.White;

                    break;


                case "czerwony":
                    this.BackColor = Color.Red;
                    panel_analog.BackColor = Color.DarkRed;
                    label1.ForeColor = Color.White;
                    break;

                case "zielony":
                    this.BackColor = Color.Green;
                    panel_analog.BackColor = Color.GreenYellow;
                    label1.ForeColor = Color.White;
                    break;

            }
            foreach (Control c in this.Controls)
            {
                c.ForeColor = label1.ForeColor;   // dopasowanie koloru tekstu
            }

            // --- ZMIANA TŁA PRZYCISKÓW ---
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                    c.BackColor = Color.White;
            }


            foreach (Control c in this.Controls)
            {
                c.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "cyfrowy")
            {
                label1.Visible = true;
                panel_analog.Visible = false;
            }
            else
            {
                label1.Visible = false;
                panel_analog.Visible = true;
            }
        }

        private void panel_analog_Paint(object sender, PaintEventArgs e)
        {

            DateTime now = DateTime.Now;

            int centerX = panel_analog.Width / 2;
            int centerY = panel_analog.Height / 2;
            int radius = panel_analog.Width / 2 - 10;


            e.Graphics.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);


            double hourAngle = (now.Hour % 12) * 30;
            DrawHand(e.Graphics, centerX, centerY, hourAngle, radius * 0.5, 4, Color.Black);


            double minuteAngle = now.Minute * 6;
            DrawHand(e.Graphics, centerX, centerY, minuteAngle, radius * 0.7, 3, Color.Black);

            // wskazówka sekundowa
            double secondAngle = now.Second * 6;
            DrawHand(e.Graphics, centerX, centerY, secondAngle, radius * 0.85, 2, Color.Red);
        }


        private void DrawHand(Graphics g, int cx, int cy, double angle, double length, int width, Color color)
        {
            double rad = Math.PI * angle / 180;
            int x = cx + (int)(length * Math.Sin(rad));
            int y = cy - (int)(length * Math.Cos(rad));

            g.DrawLine(new Pen(color, width), cx, cy, x, y);
        }

    }
}



