using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab2
{
 
    public partial class Form1 : Form
    {

        private bool mouseDown;
        private Point lastLocation;
        double[,] alinan_matris = new double[100,100];


        public double[,] Transpose(double[,] matrix,int satir,int sutun)//Transpoze alma  kismi
        {
            double[,] transposedMatrix = new double[sutun, satir];
            for (int i = 0; i < satir; i++)
                for (int j = 0; j < sutun; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            return transposedMatrix;
        }



        public double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] kHasil = new double[rA, cB];
            if (cA != rB)
            {
                Console.WriteLine("Matris çarpılamıyor !!");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                            t_art("auto");
                            t_art("manuel");
                            c_art("auto");
                            c_art("manuel");
                        }
                        kHasil[i, j] = temp;
                    }
                }
                return kHasil;
            }
            return kHasil;
        }

        private double[,] InvertMatrix(double[,] matrix)
        {
            const double tiny = 0.00001;

            int num_rows = matrix.GetUpperBound(0) + 1;
            double[,] augmented = new double[num_rows, 2 * num_rows];
            for (int row = 0; row < num_rows; row++)
            {
                for (int col = 0; col < num_rows; col++)
                    augmented[row, col] = matrix[row, col];
                augmented[row, row + num_rows] = 1;
            }

            int num_cols = 2 * num_rows;

            for (int row = 0; row < num_rows; row++)
            {
                if (Math.Abs(augmented[row, row]) < tiny)
                {
                    for (int r2 = row + 1; r2 < num_rows; r2++)
                    {
                        if (Math.Abs(augmented[r2, row]) > tiny)
                        {
                            for (int c = 0; c < num_cols; c++)
                            {
                                double tmp = augmented[row, c];
                                augmented[row, c] = augmented[r2, c];
                                augmented[r2, c] = tmp;
                            }
                            break;
                        }
                    }
                }

                if (Math.Abs(augmented[row, row]) > tiny)
                {

                    for (int col = 0; col < num_cols; col++)
                        if (col != row)
                            augmented[row, col] /= augmented[row, row];
                            c_art("auto");
                            c_art("manuel");
                    augmented[row, row] = 1;

                    for (int row2 = 0; row2 < num_rows; row2++)
                    {
                        if (row2 != row)
                        {
                            double factor = augmented[row2, row] / augmented[row, row];
                            c_art("auto");
                            c_art("manuel");
                            for (int col = 0; col < num_cols; col++)
                                augmented[row2, col] -= factor * augmented[row, col];
                            t_art("auto");
                            t_art("manuel");
                        }
                    }
                }
            }

            if (augmented[num_rows - 1, num_rows - 1] == 0) return null;

            double[,] inverse = new double[num_rows, num_rows];
            for (int row = 0; row < num_rows; row++)
            {
                for (int col = 0; col < num_rows; col++)
                {
                    inverse[row, col] = augmented[row, col + num_rows];
                }
            }

            return inverse;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {          
            if (!IsNumeric(satir.Text) || !IsNumeric(sutun.Text))
            {
                MessageBox.Show("Girilmesi İzin Verilmeyen Değer Girdiniz !");
                return;
            }
            decimal d1 = decimal.Parse(satir.Text, CultureInfo.InvariantCulture);
            decimal d2 = decimal.Parse(sutun.Text, CultureInfo.InvariantCulture);
            if (d1 > 5 || d2 > 5 || d1<1 ||d2<1)
            {
                MessageBox.Show("Girilmesi İzin Verilmeyen Değer Girdiniz !");
                return;
            }
            if (d1 % 1 != 0 || d2 % 1 !=0)
            {
                MessageBox.Show("Girilmesi İzin Verilmeyen Değer Girdiniz !");
                return;
            }

            satir.Enabled = false;
            sutun.Enabled = false;
            int sayac = 0;
            int lok_X = 45;
            int lok_Y = 75;
            for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                {
                    TextBox txt = new TextBox();
                    txt.Name = "matrissayi" + Convert.ToString(sayac);
                    txt.Size = new Size(40, 20);
                    txt.Location = new Point(lok_X, lok_Y);
                    panel7.Controls.Add(txt);
                    lok_X += 45;
                    sayac++;
                }
                lok_Y += 30;
                lok_X = 45;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int sayac = 0;
            toplama1.Text = "0";
            carpma1.Text = "0";
            manuel_log.Text = "";

            for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                {
                    TextBox pb = (TextBox)panel7.Controls.Find("matrissayi" + Convert.ToString(sayac), true)[0];
                    if (!IsNumeric(pb.Text))
                    {
                        MessageBox.Show("Matris Verilerine Girilmesi İzin Verilmeyen Değer Girdiniz !");
                        temizle();
                        return;
                    }
                    decimal d1 = decimal.Parse(pb.Text, CultureInfo.InvariantCulture);
                    alinan_matris[i,j] = Convert.ToDouble(d1);
                    sayac++;
                    t_art("manuel");
                }
            }



            if (Convert.ToInt32(satir.Text) > Convert.ToInt32(sutun.Text))
            {


                double[,] islem = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(sutun.Text)];  // İlk çarpma
                double[,] islem1 = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(sutun.Text)];
                double[,] transpoz = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(satir.Text)];
                double[,] boyut = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text)];

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        boyut[i, j] = alinan_matris[i, j];
                    }
                }

                transpoz = Transpose(alinan_matris, Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text));
                manuel_log.AppendText("Monroe-Penrose Inverse yöntemiyle çözülmüştür. (Formül: (A^t * A)^-1 * A^t)" + Environment.NewLine + "  Oluşturulan Matris:" + Environment.NewLine + Environment.NewLine);

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        manuel_log.AppendText(" " + alinan_matris[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }
                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                manuel_log.AppendText("Oluşturulan Matrisin Transpozu Bulundu:" + Environment.NewLine + Environment.NewLine);


                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + transpoz[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                islem = MultiplyMatrix(transpoz, boyut); // A * A^t

                manuel_log.AppendText("Matrisin Transpozu ile kendisi çarpılır:" + Environment.NewLine + Environment.NewLine);

                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        manuel_log.AppendText(" " + islem[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                double[,] gecici = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(sutun.Text)];
                double[,] gecici1 = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(sutun.Text)];
                double[,] sonuc = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(satir.Text)];
                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        gecici[i, j] = islem[i, j];
                    }
                }

                manuel_log.AppendText("Önceki adımda elde edilen matrisin tersi alındı:" + Environment.NewLine + Environment.NewLine);
                if (InvertMatrix(gecici) == null)
                {
                    MessageBox.Show("Matris Hata Oluştuğundan Hesaplanamıyor.");
                    temizle();
                    return;
                }
                gecici1 = InvertMatrix(gecici); // Tersini Aldık

                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {

                        manuel_log.AppendText(" " + gecici1[i, j] + " ");
                    }
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                manuel_log.AppendText("Önceki adımda tersi alınarak elde edilen matris ile , en başta oluşturulan matrisin transpozu çarpılarak sonuç elde edilir:" + Environment.NewLine + Environment.NewLine);
                sonuc = MultiplyMatrix(gecici1, transpoz);


                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + sonuc[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        Console.Write(" " + gecici[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        Console.Write(" " + sonuc[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }








            if (Convert.ToInt32(satir.Text) < Convert.ToInt32(sutun.Text))
            {


                double[,] islem = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(satir.Text)];  // İlk çarpma
                double[,] islem1 = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(sutun.Text)];
                double[,] transpoz = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(satir.Text)];
                double[,] gecici1 = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(satir.Text)];
                double[,] boyut = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text)];
                double[,] sonuc = new double[Convert.ToInt32(sutun.Text), Convert.ToInt32(satir.Text)];

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        boyut[i, j] = alinan_matris[i, j];
                    }
                }

                manuel_log.AppendText("Monroe-Penrose Inverse yöntemiyle çözülmüştür. (Formül: (A^t * A)^-1 * A^t)" + Environment.NewLine + "  Oluşturulan Matris:" + Environment.NewLine + Environment.NewLine);
                transpoz = Transpose(alinan_matris, Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text));


                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        manuel_log.AppendText(" " + alinan_matris[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }
                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                manuel_log.AppendText("Girilen Matrisin Transpozu Bulundu:" + Environment.NewLine + Environment.NewLine);


                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + transpoz[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                manuel_log.AppendText("Girilen matris ile transpozu çarpıldı:" + Environment.NewLine + Environment.NewLine);

                islem = MultiplyMatrix(boyut, transpoz); // A * A^t

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + islem[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);


                manuel_log.AppendText("Elde edilen matrisin tersi alınır:" + Environment.NewLine + Environment.NewLine);
                if (InvertMatrix(islem) == null)
                {
                    MessageBox.Show("Matris Hata Oluştuğundan Hesaplanamıyor.");
                    temizle();
                    return;
                }
                gecici1 = InvertMatrix(islem); // Tersini Aldık


                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + gecici1[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText("Oluşturulan matrisin transpozu ile bir önceki adımda elde edilen matris çarpılır ve sonuç elde edilir:" + Environment.NewLine + Environment.NewLine);

                sonuc = MultiplyMatrix(transpoz, gecici1);


                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + sonuc[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        Console.Write(" " + islem[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        Console.Write(" " + sonuc[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }






            if (Convert.ToInt32(satir.Text) == Convert.ToInt32(sutun.Text))
            {
                manuel_log.AppendText("Monroe-Penrose Inverse yöntemiyle çözülmüştür. (Formül: (A^t * A)^-1 * A^t)" + Environment.NewLine + "  Oluşturulan Matris:" + Environment.NewLine + Environment.NewLine);

                double[,] boyut = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text)];
                double[,] snc = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text)];

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        boyut[i, j] = alinan_matris[i, j];
                    }
                }

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir.Text); j++)
                    {
                        manuel_log.AppendText(" " + boyut[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                manuel_log.AppendText("Matris kare olduğundan direkt olarak matrisin tersi alınır ve sonuç elde edilir:" + Environment.NewLine + Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);
                manuel_log.AppendText(Environment.NewLine);

                if (InvertMatrix(boyut) == null)
                {
                    MessageBox.Show("Matris Hata Oluştuğundan Hesaplanamıyor.");
                    temizle();
                    return;
                }
                snc = InvertMatrix(boyut);


                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        manuel_log.AppendText(" " + snc[i, j] + " ");
                    }
                    manuel_log.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        Console.Write(" " + boyut[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Convert.ToInt32(sutun.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        Console.Write(" " + snc[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }















        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static readonly Random random1 = new Random();
        private static readonly object syncLock1 = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }

        public static double RandomFNumber()
        {
            lock (syncLock1)
            {
                return random1.NextDouble();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            satir1.Text = Convert.ToString(RandomNumber(1, 5));
            sutun1.Text = Convert.ToString(RandomNumber(1, 5));
      
            int sayac = 0;
            int lok_X = 45;
            int lok_Y = 75;
            for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                {
                    TextBox txt = new TextBox();
                    txt.Name = "matrissayi" + Convert.ToString(sayac);
                    txt.Size = new Size(40, 20);
                    Random random = new Random();
                    Random random1 = new Random();
                    int sayi = RandomNumber(0, 9);
                    double sayi1 = RandomFNumber();
                    double sonuc = sayi + sayi1;
                    decimal bar = Math.Round(Convert.ToDecimal(sonuc),1);
                    txt.Text = Convert.ToString(bar);
                    txt.Enabled = false;
                    txt.Location = new Point(lok_X, lok_Y);
                    panel11.Controls.Add(txt);
                    lok_X += 45;
                    sayac++;
                }
                lok_Y += 30;
                lok_X = 45;
            }
        }

        int t_art(string tip)
        {
            if (tip == "manuel")
            {
                toplama1.Text = Convert.ToString(Convert.ToInt32(toplama1.Text) + 1);
            }
            else
            {
                toplama.Text = Convert.ToString(Convert.ToInt32(toplama.Text) + 1);
            }
            return 1;
        }

        int c_art(string tip)
        {
            if (tip == "manuel")
            {
                carpma1.Text = Convert.ToString(Convert.ToInt32(carpma1.Text) + 1);
            }
            else
            {
                carpma.Text = Convert.ToString(Convert.ToInt32(carpma.Text) + 1);
            }       
            return 1;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            int sayac = 0;
            toplama.Text = "0";
            carpma.Text = "0";
            oto_log.Text="";
            for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                {
                    TextBox pb = (TextBox)panel11.Controls.Find("matrissayi" + Convert.ToString(sayac), true)[0];
                    alinan_matris[i, j] = Convert.ToDouble(pb.Text);
                    sayac++;
                    t_art("auto");
                }
            }


            if (Convert.ToInt32(satir1.Text)> Convert.ToInt32(sutun1.Text))
            {


                double[,] islem = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(sutun1.Text)];  // İlk çarpma
                double[,] islem1 = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(sutun1.Text)];
                double[,] transpoz = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(satir1.Text)];
                double[,] boyut = new double[Convert.ToInt32(satir.Text), Convert.ToInt32(sutun.Text)];

                for (int i = 0; i < Convert.ToInt32(satir.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun.Text); j++)
                    {
                        boyut[i, j] = alinan_matris[i, j];
                    }
                }

                transpoz = Transpose(alinan_matris, Convert.ToInt32(satir1.Text), Convert.ToInt32(sutun1.Text));
                oto_log.AppendText("Monroe-Penrose Inverse yöntemiyle çözülmüştür. (Formül: (A^t * A)^-1 * A^t)" +Environment.NewLine+ "  Oluşturulan Matris:" + Environment.NewLine + Environment.NewLine);

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        oto_log.AppendText(" " + alinan_matris[i, j]+" ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }
                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                oto_log.AppendText("Oluşturulan Matrisin Transpozu Bulundu:" + Environment.NewLine + Environment.NewLine);


                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + transpoz[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                islem = MultiplyMatrix(transpoz, boyut); // A * A^t

                oto_log.AppendText("Matrisin Transpozu ile kendisi çarpılır:" + Environment.NewLine + Environment.NewLine);

                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        oto_log.AppendText(" " + islem[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                double[,] gecici = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(sutun1.Text)];
                double[,] gecici1 = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(sutun1.Text)];
                double[,] sonuc = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(satir1.Text)];
                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        gecici[i, j] = islem[i, j];
                    }
                }

                oto_log.AppendText("Önceki adımda elde edilen matrisin tersi alındı:" + Environment.NewLine + Environment.NewLine);
                if (InvertMatrix(gecici) == null)
                {
                    MessageBox.Show("Matris Hata Oluştuğundan Hesaplanamıyor.");
                    temizle();
                    return;
                }
                gecici1 = InvertMatrix(gecici); // Tersini Aldık

                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {

                        oto_log.AppendText(" " + gecici1[i, j] + " ");
                    }
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                oto_log.AppendText("Önceki adımda tersi alınarak elde edilen matris ile , en başta oluşturulan matrisin transpozu çarpılarak sonuç elde edilir:" + Environment.NewLine + Environment.NewLine);
                sonuc = MultiplyMatrix(gecici1, transpoz);


                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + sonuc[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        Console.Write(" " + gecici[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        Console.Write(" " + sonuc[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }








            if (Convert.ToInt32(satir1.Text) < Convert.ToInt32(sutun1.Text))
            {


                double[,] islem = new double[Convert.ToInt32(satir1.Text), Convert.ToInt32(satir1.Text)];  // İlk çarpma
                double[,] islem1 = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(sutun1.Text)];
                double[,] transpoz = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(satir1.Text)];
                double[,] gecici1 = new double[Convert.ToInt32(satir1.Text), Convert.ToInt32(satir1.Text)];
                double[,] boyut = new double[Convert.ToInt32(satir1.Text), Convert.ToInt32(sutun1.Text)];
                double[,] sonuc = new double[Convert.ToInt32(sutun1.Text), Convert.ToInt32(satir1.Text)];

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        boyut[i, j] = alinan_matris[i, j];
                    }
                }

                oto_log.AppendText("Monroe-Penrose Inverse yöntemiyle çözülmüştür. (Formül: (A^t * A)^-1 * A^t)" + Environment.NewLine + "  Oluşturulan Matris:" + Environment.NewLine + Environment.NewLine);
                transpoz = Transpose(alinan_matris, Convert.ToInt32(satir1.Text), Convert.ToInt32(sutun1.Text));


                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        oto_log.AppendText(" " + alinan_matris[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }
                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                oto_log.AppendText("Girilen Matrisin Transpozu Bulundu:" + Environment.NewLine + Environment.NewLine);


                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + transpoz[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                oto_log.AppendText("Girilen matris ile transpozu çarpıldı:" + Environment.NewLine + Environment.NewLine);

                islem = MultiplyMatrix(boyut, transpoz); // A * A^t

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + islem[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);


                oto_log.AppendText("Elde edilen matrisin tersi alınır:" + Environment.NewLine + Environment.NewLine);
                if (InvertMatrix(islem) == null)
                {
                    MessageBox.Show("Matris Hata Oluştuğundan Hesaplanamıyor.");
                    temizle();
                    return;
                }
                gecici1 = InvertMatrix(islem); // Tersini Aldık


                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + gecici1[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText("Oluşturulan matrisin transpozu ile bir önceki adımda elde edilen matris çarpılır ve sonuç elde edilir:" + Environment.NewLine + Environment.NewLine);

                sonuc = MultiplyMatrix(transpoz, gecici1);


                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + sonuc[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        Console.Write(" " + islem[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        Console.Write(" " + sonuc[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }






            if (Convert.ToInt32(satir1.Text) == Convert.ToInt32(sutun1.Text))
            {
                oto_log.AppendText("Monroe-Penrose Inverse yöntemiyle çözülmüştür. (Formül: (A^t * A)^-1 * A^t)" + Environment.NewLine + "  Oluşturulan Matris:" + Environment.NewLine + Environment.NewLine);

                double[,] boyut = new double[Convert.ToInt32(satir1.Text), Convert.ToInt32(sutun1.Text)];
                double[,] snc = new double[Convert.ToInt32(satir1.Text), Convert.ToInt32(sutun1.Text)];

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        boyut[i, j] = alinan_matris[i, j];
                    }
                }

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(satir1.Text); j++)
                    {
                        oto_log.AppendText(" " + boyut[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                oto_log.AppendText("Matris kare olduğundan direkt olarak matrisin tersi alınır ve sonuç elde edilir:" + Environment.NewLine + Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);
                oto_log.AppendText(Environment.NewLine);

                if (InvertMatrix(boyut) == null)
                {
                    MessageBox.Show("Matris Hata Oluştuğundan Hesaplanamıyor.");
                    temizle();
                    return;
                }
                snc = InvertMatrix(boyut);


                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        oto_log.AppendText(" " + snc[i, j] + " ");
                    }
                    oto_log.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < Convert.ToInt32(satir1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        Console.Write(" " + boyut[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Convert.ToInt32(sutun1.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(sutun1.Text); j++)
                    {
                        Console.Write(" " + snc[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }




            }

        private void label1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        int temizle()
        {
            toplama.Text = "0";
            carpma.Text = "0";
            oto_log.Text = "";
            toplama1.Text = "0";
            carpma1.Text = "0";
            manuel_log.Text = "";
            satir.Clear();
            satir1.Clear();
            sutun.Clear();
            sutun1.Clear();
            satir.Enabled = true;
            sutun.Enabled = true;
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    TextBox pb = (TextBox)panel7.Controls.Find("matrissayi" + Convert.ToString(i), true)[0];
                    pb.Dispose();
                }
            }
            catch (Exception)
            {

            }

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    TextBox pb = (TextBox)panel11.Controls.Find("matrissayi" + Convert.ToString(i), true)[0];
                    pb.Dispose();
                }
            }
            catch (Exception)
            {

            }
            return 1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            temizle();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.el_hover;
            label1.ForeColor = Color.FromArgb(225, 225, 225);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.el;
            label1.ForeColor = Color.FromArgb(119, 136, 170);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.oto_hover;
            label3.ForeColor = Color.FromArgb(225, 225, 225);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.oto;
            label3.ForeColor = Color.FromArgb(119, 136, 170);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.kapat_hover;
            label4.ForeColor = Color.FromArgb(225, 225, 225);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.kapat;
            label4.ForeColor = Color.FromArgb(119, 136, 170);
        }

        private void baslik_MouseLeave(object sender, EventArgs e)
        {
            baslik.ForeColor = Color.FromArgb(119, 136, 170);
        }

        private void baslik_MouseHover(object sender, EventArgs e)
        {
            baslik.ForeColor = Color.FromArgb(225, 225, 225);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Size.Height<85 && pictureBox1.Size.Width < 85)
            {
                if (pictureBox1.Top % 2 == 0)
                {
                    pictureBox1.Left -= 1;
                }
                pictureBox1.Top -= 1;
                int height = pictureBox1.Size.Height;
                int width = pictureBox1.Size.Width;
                pictureBox1.Size = new Size(width + 1, height + 1);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer1.Enabled = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Size.Height > 69 && pictureBox1.Size.Width > 69)
            {
                if (pictureBox1.Top % 2 == 0)
                {
                    pictureBox1.Left += 1;
                }
                pictureBox1.Top += 1;
                int height = pictureBox1.Size.Height;
                int width = pictureBox1.Size.Width;
                pictureBox1.Size = new Size(width - 1, height - 1);
            }
        }
    }
}
