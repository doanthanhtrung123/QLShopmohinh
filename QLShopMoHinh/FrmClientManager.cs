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
    public partial class FrmClientManager : DevExpress.XtraEditors.XtraForm
    {
        public FrmClientManager()
        {
            InitializeComponent();
        }
        ShopMoHinhDB context = new ShopMoHinhDB();
        private void FrmClientManager_Load(object sender, EventArgs e)
        {

            List<Client> ListClients = context.Clients.ToList();
            List<ClientRank> ListClientRank = context.ClientRanks.ToList();
            FillGrid(ListClients);


        }
        public void FillGrid(List<Client> ListClients)
        {
            DgvClient.Rows.Clear();
            foreach (var client in ListClients)
            {
                int index = DgvClient.Rows.Add();
                DgvClient.Rows[index].Cells[0].Value = client.ID;
                DgvClient.Rows[index].Cells[1].Value = client.FullName;
                DgvClient.Rows[index].Cells[2].Value = client.Address;
                DgvClient.Rows[index].Cells[3].Value = client.ToltalMoney;
                DgvClient.Rows[index].Cells[4].Value = client.ClientRank.Rank;
                DgvClient.Rows[index].Cells[5].Value = client.PhoneNumber;


            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ( txtFullName.Text == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtID.Text == "")
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
                        FillGrid(ListClient);
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

        private void DgvClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            DataGridViewRow row = DgvClient.Rows[e.RowIndex];
            if (row.Cells[0].Value != null)
                txtID.Text = row.Cells[0].Value.ToString();
            if (row.Cells[1].Value != null)
                txtFullName.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value != null)
                txtAddress.Text = row.Cells[2].Value.ToString();
            if (row.Cells[5].Value != null)
                txtPhoneNumber.Text = row.Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtID.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!!");
                Client dbUpdate = context.Clients.FirstOrDefault(p => p.PhoneNumber == txtPhoneNumber.Text);
                if (dbUpdate == null)
                    throw new Exception("Không tìm thấy sản phẩm cần sửa!!!");
                else
                {
                    dbUpdate.FullName = txtFullName.Text.ToString();
                    dbUpdate.Address = txtAddress.Text.ToString();
                    dbUpdate.ID = txtID.Text.ToString();
                    dbUpdate.PhoneNumber = txtPhoneNumber.Text.ToString();
                    context.SaveChanges();
                    txtID.Text = "";  txtFullName.Text = ""; txtAddress.Text = ""; txtPhoneNumber.Text = "";
                    try
                    {

                        List<Client> ListClients = context.Clients.ToList();

                        FillGrid(ListClients);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    throw new Exception("Cập nhật dữ liệu thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtID.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!!");
                Client dbDelete = context.Clients.FirstOrDefault(p => p.PhoneNumber == txtPhoneNumber.Text);
                if (dbDelete == null)
                    throw new Exception("Không tìm thấy sản phẩm cần xóa!!!");
                else
                {
                    context.Clients.Remove(dbDelete);
                    context.SaveChanges();
                    txtID.Text = "";  txtFullName.Text = ""; txtAddress.Text = ""; txtPhoneNumber.Text = "";
                    try
                    {

                        List<Client> ListClient = context.Clients.ToList();
                        FillGrid(ListClient);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    throw new Exception("Xóa dữ liệu thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                Client dbFind = context.Clients.FirstOrDefault(p => p.PhoneNumber == txtPhoneNumber.Text);
                if (dbFind == null)
                    MessageBox.Show("Nhân viên không tồn tại trong dữ liệu!!");
                else
                {
                    txtID.Text = dbFind.ID;
                    txtFullName.Text = dbFind.FullName;
                    txtPhoneNumber.Text = dbFind.PhoneNumber;
                    txtAddress.Text = dbFind.Address;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}