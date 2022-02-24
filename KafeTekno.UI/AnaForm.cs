using KafeTekno.DATA;
using KafeTekno.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeTekno.UI
{
    public partial class AnaForm : Form
    {
        KafeVeri db = new KafeVeri();
        public AnaForm()
        {
            InitializeComponent();            
            OrnekUrunleriYukle();
            MasalariOlustur();


        }

        private void MasalariOlustur()
        {
            lvwMasalar.LargeImageList = BuyukImajListesi(); // lvwMasalar da LargeImageList e BuyukImajListesini
                                                            // yolladık
            for (int i = 1; i <= db.MasaAdet; i++) // masaları ekledik
            {
                ListViewItem lvi = new ListViewItem(i.ToString("00"));
                lvi.ImageKey = "bos";
                lvi.Tag = i; // tagine seçilen masayı koymak için masa numarasını koyduk,
                             // lvwMasalar_doubleclick te int e dönüştürüp masa numarasını aldık
                lvwMasalar.Items.Add(lvi);
            }
        }

        private void OrnekUrunleriYukle()
        {
            db.Urunler.Add(new Urun() { UrunAd = "Kola", BirimFiyat = 7.00m });
            db.Urunler.Add(new Urun() { UrunAd = "Ayran", BirimFiyat = 5.00m });

        }
        private ImageList BuyukImajListesi()
        {
            ImageList il = new ImageList();
            il.ImageSize = new Size(64, 64);
            il.Images.Add("bos", Resources.bos);
            il.Images.Add("dolu", Resources.dolu);
            return il;


        }

        private void lvwMasalar_DoubleClick(object sender, EventArgs e)
        {
            // herhangi bir öğeye tıklayınca ekrana getirecek.
            ListViewItem lvi = lvwMasalar.SelectedItems[0];
            int masaNo = (int)lvi.Tag; // object ten int e unboxing yaptık
            lvi.ImageKey = "dolu";
            Siparis siparis = SiparisBulYaDaOlustur(masaNo);
            new SiparisForm(db,siparis).ShowDialog(); // sipariş formunu açar // constructordan gelen ilk değerleri giriyoruz (db,siparis)
            MessageBox.Show("Bu yazı geldiğinde sipariş formu sonlanmıştır.");
            if (siparis.Durum != SiparisDurum.Aktif)
            {
                lvi.ImageKey = "bos";
            }
        }

        private Siparis SiparisBulYaDaOlustur(int masaNo)
        {
            Siparis siparis = db.AktifSiparisler.FirstOrDefault(x => x.MasaNo == masaNo); // masayı kontrol et,sipariş yoksa null döndür,
                                                                                          // if'e gir yeni sipariş oluşur.
                                                                                          // varsa aktif siparişten devam et
            if (siparis == null)
            {
                siparis = new Siparis() { MasaNo = masaNo };
                db.AktifSiparisler.Add(siparis);
            }
            return siparis;
        }
    }
}
