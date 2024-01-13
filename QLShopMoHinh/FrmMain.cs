using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopMoHinh
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Clear_panel()
        {
            Main.Controls.Clear();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmTrade f = new FrmTrade() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Main.Controls.Add(f);
            f.Show();
        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            Clear_panel();
            FrmTrade f = new FrmTrade() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Main.Controls.Add(f);
            f.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Clear_panel();
            FrmStaffManager f = new FrmStaffManager() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Main.Controls.Add(f);
            f.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            Clear_panel();
            FrmClientManager f = new FrmClientManager() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Main.Controls.Add(f);
            f.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Clear_panel();
            FrmInventoryManager f = new FrmInventoryManager() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Main.Controls.Add(f);
            f.Show();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            Clear_panel();
            FrmBillManager f = new FrmBillManager() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Main.Controls.Add(f);
            f.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FrmDangNhap f = new FrmDangNhap();
            this.Hide();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}