using KafeTekno.DATA;
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
    public partial class GecmisSiparisler : Form
    {
        private readonly KafeVeri _db;
        public GecmisSiparisler(KafeVeri db)
        {
            _db = db;
            InitializeComponent();
            dgwSiparisler.DataSource = _db.GecmisSiparisler;
        }

        private void dgwSiparisDetaylar_SelectionChanged(object sender, EventArgs e)
        {
            // satırlarda seçim olduğunda tetiklenir.

            if (dgwSiparisler.SelectedRows.Count == 0)
            {
                dgwSiparisDetaylar.DataSource = null;
            }
            else
            {
                DataGridViewRow seciliSatir = dgwSiparisler.SelectedRows[0];
                Siparis siparis = (Siparis)seciliSatir.DataBoundItem; // datasource ile çalışıldığında satırlara birşey
                                                                                        // eklendiğinde databounditem propertisinde
                                                                                        // ürünleri sergilyor
                dgwSiparisDetaylar.DataSource = siparis.SiparisDetaylar;
            }

        }
    }
}
