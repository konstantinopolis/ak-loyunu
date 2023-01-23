using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace WindowsFormsApp23
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] Elemanlar = { "hero1", "hero2", "hero3", "hero4", "hero5", "hero6", "hero7", "hero8", "hero1", "hero2","hero3","hero4","hero5","hero6 ","hero7","hero8" };
        ArrayList liste1 = new ArrayList();
        ArrayList liste2 = new ArrayList();
        ArrayList secimler = new ArrayList(2); //secimler icin dizi
        PictureBox[] resimler = new PictureBox[16];//resimler icin dizi
        int acilan_resim = 0;
        private void Resim_Ac(string isim, int i)
        {   //isimlere göre resim atamasi yapılır
            if (isim == "hero1")
            {
                resimler[i].Image = Properties.Resources.hero1;
            }
            else if (isim == "hero2")
            {
                resimler[i].Image = Properties.Resources.hero2;
            }
            else if (isim == "hero3")
            {
                resimler[i].Image = Properties.Resources.hero3;
            }
            else if (isim == "hero4")
            {
                resimler[i].Image = Properties.Resources.hero4;
            }
            else if (isim == "hero5")
            {
                resimler[i].Image = Properties.Resources.hero5;
            }
            else if (isim == "hero6")
            {
                resimler[i].Image = Properties.Resources.hero6;
            }
            else if (isim == "hero7")
            {
                resimler[i].Image = Properties.Resources.hero7;
            }
            else if (isim == "hero8")
            {
                resimler[i].Image = Properties.Resources.hero8;
            }
        }

        private void Oyun_Baslat()
        {

            liste1 = new ArrayList();//listeler sıfırlanır
            liste2 = new ArrayList();
            resimler = new PictureBox[16];

            Random rnd = new Random();
            foreach (string i in Elemanlar) //liste1 icine üyeler eklenir
            {
                liste1.Add(i);
            }

            for (int i = 0; i < 16; i++)
            {
                int index = rnd.Next(0, liste1.Count); //liste1 elemanlarindan birinin indexini seçer rastgele
                liste2.Add(liste1[index]); //bu indexe ait liste1 elemanini liste2ye ekler
                liste1.RemoveAt(index);//liste1deki elemani kaldirir
            } //elemanlar listesi rastgele sekilde liste2ye dagitilmis olur

            for (int i = 0; i < 16; i++)
            {
                resimler[i] = new PictureBox(); //resim olusturur
                resimler[i].Name = i.ToString(); //ismini olusturur
                resimler[i].Size = new Size(90, 90); //boyutunu olusturur
                resimler[i].Image = Properties.Resources.which_is_hero; //baslangic resmini belirler
                resimler[i].SizeMode = PictureBoxSizeMode.Zoom;
                resimler[i].Click += Resimler_Click; //tiklama özelligi kazandirir
                tableLayoutPanel1.Controls.Add(resimler[i]);
            }
        }
        
        private async void Resimler_Click(object sender, EventArgs e)
        {

            PictureBox p = (PictureBox)sender; 
            int index=Convert.ToInt32(p.Name.ToString());//resmin indexini yollar

            if (secimler.Count != 2) //eger iki tane secim yapilmamissa
            {
                
                secimler.Add(index); //sceim yaparz
                Resim_Ac(liste2[index].ToString(), index); //liste2deki sayiya göre resim acar
                resimler[index].Enabled = false; //resimin tekrar secilmesi engellenir
            }
            if(secimler.Count==2) //eger iki tane secildiyse
            {
                if(liste2[Convert.ToInt32(secimler[0])]==liste2[Convert.ToInt32(secimler[1])])
                {
                    acilan_resim += 2;//acilan resmi 2 artirir
                    secimler = new ArrayList(); //secimleri sifirlar
                    if(acilan_resim==16) //eger 16 tane resim acildiysa
                    {
                        MessageBox.Show(skor); //skoru ekrana yollar
                        timer1.Enabled = false; // zamani durdurur
                        Application.Restart(); //uygulamayi baslatir
                    }
                }
                else //resimler es degilse
                {

                    System.Threading.Thread.Sleep(300);
                    resimler[Convert.ToInt32(secimler[0])].Image = Properties.Resources.which_is_hero; 
                    resimler[Convert.ToInt32(secimler[1])].Image = Properties.Resources.which_is_hero;
                    resimler[Convert.ToInt32(secimler[0])].Enabled = true;//resimler tekrar aktif olur
                    resimler[Convert.ToInt32(secimler[1])].Enabled = true;
                    secimler = new ArrayList(); // secimleri sifirlar
                }
            }
            
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            Wtch.Start(); //sayaci baslatir
            timer1.Enabled = true; //timer baslatir
            Oyun_Baslat();// oyun baslatir
           
        }
        Stopwatch Wtch = new Stopwatch();
        public string skor = "";
        private void Timer1_Tick(object sender, EventArgs e)
        {
            
                TimeSpan zaman = TimeSpan.FromMilliseconds(Wtch.ElapsedMilliseconds); //milisaniyeyi saniyeye cevirir 
                 skor = string.Format("{0:D2} hour {1:D2} minute {2:D2} second yaptınız.",
                            zaman.Hours,
                            zaman.Minutes,       //saat dakika saniye formatina dönüstürürz
                            zaman.Seconds,
                            zaman.Milliseconds);
                
            }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
