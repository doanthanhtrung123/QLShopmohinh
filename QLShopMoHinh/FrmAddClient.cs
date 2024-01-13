using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopMoHinh
{
    public partial class FrmAddClient : DevExpress.XtraEditors.XtraForm
    {
        ShopMoHinhDB context = new ShopMoHinhDB();
        public FrmAddClient()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtID.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin !!");
                Client db = new Client();
                {
                    db.ID = txtID.Text.ToString();
                    db.FullName = txtFullName.Text.ToString();
                    db.Address = txtAddress.Text.ToString();
                    db.PhoneNumber = txtPhoneNumber.Text.ToString();
                    int Money = int.Parse("0");
                    db.ToltalMoney = Money;
                    db.RankID = 1;
                    context.Clients.Add(db);
                    context.SaveChanges();
                    txtID.Text = ""; txtFullName.Text = ""; txtAddress.Text = ""; txtPhoneNumber.Text = "";
                    try
                    {

                        List<Client> ListClient = context.Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    throw new Exception("Thêm dữ liệu thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}