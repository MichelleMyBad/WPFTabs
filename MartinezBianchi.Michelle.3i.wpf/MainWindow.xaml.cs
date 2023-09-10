using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace MartinezBianchi.Michelle._3i.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int[] unsortedStatic = new int[] { 2, 1, 3, 8, 7, 5, 6, 4, 100, 79, 1, 2, 10 };
        int[] unsorted = new int[] { 2, 1, 3, 8, 7, 5, 6, 4, 100, 79, 1, 2, 10 };

        private Timer _timer;
        Thread thread1;
        Thread thread2;

        public MainWindow()
        {

            InitializeComponent();
            thread1 = new Thread(Sort);
            thread1.Start();
            LstUnsorted.ItemsSource = unsortedStatic;



            _timer = new Timer(Stufa, null, 0, 1000);
        }

        //Funzione per aggiornamento timer
        private void Stufa(object state)
        {
            Dispatcher.Invoke(() =>
            {

                txtDataCo.Text = DateTime.Now.ToString().Split(' ')[0];
                txtOraCo.Text = DateTime.Now.ToString().Split(' ')[1];
            });
        }


        //Funzioni per bubble
        public void Sort()
        {
            Dispatcher.Invoke(() =>
            {
                LstSorted.ItemsSource = unsorted;
            });
            int tmp;
            for (int lunghezza = unsorted.Length; lunghezza > 1; lunghezza--)
            {
                for (int i = 0; i < lunghezza - 1; i++)
                {
                    if (unsorted[i] > unsorted[i + 1])
                    {
                        tmp = unsorted[i];
                        unsorted[i] = unsorted[i + 1];
                        unsorted[i + 1] = tmp;
                    }
                    Dispatcher.Invoke(() =>
                    {
                        LstSorted.Items.Refresh();
                    });
                    Thread.Sleep(100);
                }
            }
        }
        
        public void Generate()
        {
            var random = new Random();
            for (int x = 0; x < unsorted.Length; x++)
            {
                unsortedStatic[x] = random.Next(1, 101);
                unsorted[x] = unsortedStatic[x];
                Dispatcher.Invoke(() =>
                {
                    LstUnsorted.Items.Refresh();
                    LstSorted.Items.Refresh();
                });
                Thread.Sleep(100);
            }
            thread1 = new Thread(Sort);
            thread1.Start();
        }


        // Funzioni per collatz
        public int Passi(int n)
        {
            int counter = 0;
            if (n <= 0)
            {
                throw new ArgumentException();
            }

            for (counter = 0; n > 1; counter++)
            {
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else if (n % 2 == 1)
                {
                    n = (n * 3 + 1);
                }
            }
            return counter;
        }


        //Funzioni per telefono
        public string Pulisci(string phoneNumber)
        {
            for (int i = 0; i < phoneNumber.Length; i++)
            {

                if (char.IsDigit(phoneNumber[i]) == false)
                {
                    phoneNumber = phoneNumber.Remove(i, 1);
                    i--;
                }
            }
            if (phoneNumber[0] == '1')
            {
                phoneNumber = phoneNumber.Remove(0, 1);
            }
            if (phoneNumber.Length != 10)
            {
                throw new ArgumentException();
            }
            else if (Convert.ToInt32(phoneNumber[0].ToString()) < 2 || Convert.ToInt32(phoneNumber[3].ToString()) < 2)
            {
                throw new ArgumentException();
            }
            else
            {
                return phoneNumber;
            }

        }



        //Funzioni per acronimo
        public string Abbrevia(string phrase)
        {
            string acronimo = "";

            if (char.IsLetter(phrase[0]))
            {
                acronimo = phrase[0].ToString().ToUpper();
            }
            while (char.IsLetter(phrase[phrase.Length - 1]) == false) 
            {
                phrase=phrase.Substring(0, phrase.Length - 1);
            }
            int lunghezza = phrase.Length;
            for (int i = 0; i < lunghezza; i++)
            {
                if (phrase[i].ToString() == "'" && (phrase[i + 1].ToString() != "s" && phrase[i + 1].ToString() != "S"))
                {
                    if (char.IsLetter(phrase[i + 1]))
                    {
                        acronimo = acronimo + phrase[i + 1].ToString().ToUpper();
                    }

                }
                else if(char.IsLetter(phrase[i]) == false && phrase[i].ToString() != "'")
                {
                    if (char.IsLetter(phrase[i + 1]))
                    {
                        acronimo = acronimo + phrase[i + 1].ToString().ToUpper();
                    }
                }
            }
            return acronimo;
        }

        //Funzioni isogramma
        public bool Verifica(string word)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                for (int j = i + 1; j < word.Length; j++)
                {
                    if (Char.IsSymbol(word[i])==false && Char.ToLower(word[i]) == Char.ToLower(word[j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Buttons interactions

        //Bottoni per bubble

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (thread1.ThreadState == ThreadState.Stopped)
            {
                if (thread2 == null || thread2.ThreadState == ThreadState.Stopped)
                {
                    thread2 = new Thread(Generate);
                    thread2.Start();
                }

            }
        }

        //Bottoni per collatz
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int valore = Convert.ToInt32(edtCollatz.Text);
                int risultato = Passi(valore);
                txtCollatz.Text= risultato.ToString();
            }
            catch(ArgumentException) 
            {
                MessageBox.Show("Inserire un numero maggiore di 0 nella casella");
            }
            catch (Exception)
            {
                MessageBox.Show("Inserire un numero nella casella");
            }
        }

        //Bottoni per telefono
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string numero = edtTelefono.Text;
                txtstrTelefono.Text = numero;
                string numeroPulito = Pulisci(numero);
                txtTelefono.Text = numeroPulito;
            }
            catch(Exception)
            {
                MessageBox.Show("Inserire un numero valido nella casella");
            }
        }

        //Bottoni per acronimo
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string frase = edtAcronimo.Text;
                string acronimo = Abbrevia(frase);
                txtAcronimo.Text = acronimo;
            }
            catch(Exception)
            {
                MessageBox.Show("Inserire delle parole nella casella");
            }

        }

        //Bottoni isogramma
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string testo = edtIsogramma.Text;
            bool verificatore = Verifica(testo);

            if (verificatore == false)
            {
                txtIsogramma.Text = "La stringa inserita non è un isogramma";
            }
            else
            {
                txtIsogramma.Text = "La stringa inserita è un isogramma";
            }
        }
    }
}
