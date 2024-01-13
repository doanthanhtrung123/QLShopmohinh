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
    public partial class FrmTrade : Form
    {
        ShopMoHinhDB context = new ShopMoHinhDB();

        public FrmTrade()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            txtDay.Text = string.Format("{0}", DateTime.Now.ToString("dd/MM/yyy"));
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            txtAddress.Text = "";
            txtDiscount.Text = "";
            txtPhoneNumber.Text = "";
            txtTotalmoney.Text = "0";
            txtUserFullname.Text = "";
            txtSelectPhonenumber.Text = "";
            dgvListItemToTransaction.Rows.Clear();
        }

        private void btnFindClient_Click(object sender, EventArgs e)
        {
            try
            {
                Client dbFind = context.Clients.FirstOrDefault(p => p.PhoneNumber == txtPhoneNumber.Text);
                if (dbFind == null)
                {
                    if (txtPhoneNumber.Text == "")
                        MessageBox.Show("Vui lòng nhập số điện thoại của khách hàng để tìm");
                    else
                    {
                        DialogResult dr = MessageBox.Show("Khách hàng không tồn tại! Bạn có muốn thêm khách hàng?", "YES/NO", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            FrmAddClient f = new FrmAddClient();
                            f.ShowDialog();
                            this.Show();
                        }
                    }
                }
                else
                {
                    txtUserFullname.Text = dbFind.FullName;
                    txtAddress.Text = dbFind.Address;
                    txtSelectPhonenumber.Text = dbFind.PhoneNumber;
                    if (dbFind.RankID == 1) txtDiscount.Text = "0%";
                    if (dbFind.RankID == 2) txtDiscount.Text = "5%";
                    if (dbFind.RankID == 3) txtDiscount.Text = "10%";
                    if ((dbFind.RankID == 4)) txtDiscount.Text = "15%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillClassCombobox(List<Class> ListClassses)
        {
            cmbClass.DataSource = context.Classes.Select(x => new { x.Name, x.ID }).ToList(); ;
            cmbClass.DisplayMember = "Name";
            cmbClass.ValueMember = "ID";

        }

        private void FillNameItemCombobox()
        {
            int id = int.Parse(cmbClass.SelectedValue.ToString());
            cmbNameOfItem.DataSource = context.Items.Where(x => x.ClassID == id).ToList();
            cmbNameOfItem.DisplayMember = "ItemName";
            cmbNameOfItem.ValueMember = "ID";
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillNameItemCombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetSelectedRow(string IDItem)
        {
            IDItem = cmbNameOfItem.SelectedValue.ToString();
            for (int i = 0; i < dgvListItemToTransaction.Rows.Count; i++)
            {
                if (dgvListItemToTransaction.Rows[i].Cells[0].Value != null && dgvListItemToTransaction.Rows[i].Cells[0].Value.ToString() == IDItem)
                    return i;
            }
            return -1;
        }

        private void InsertUpdate(int SelectedRow)
        {
            string IDItem = cmbNameOfItem.SelectedValue.ToString();
            Item dbUpdate = context.Items.FirstOrDefault(p => p.ID == IDItem);
            if (dbUpdate.Inventory < int.Parse(nudSelectItem.Text))
            {
                MessageBox.Show("Số lượng trong kho còn lại không đủ");
                return;
            }
            if (dbUpdate == null)
                return;
            else
            {
                dgvListItemToTransaction.Rows[SelectedRow].Cells[0].Value = dbUpdate.ID;
                dgvListItemToTransaction.Rows[SelectedRow].Cells[1].Value = dbUpdate.ItemName;
                dgvListItemToTransaction.Rows[SelectedRow].Cells[2].Value = dbUpdate.Class.Name;
                dgvListItemToTransaction.Rows[SelectedRow].Cells[3].Value = dbUpdate.Price;
                dgvListItemToTransaction.Rows[SelectedRow].Cells[4].Value = int.Parse(nudSelectItem.Text);
                dgvListItemToTransaction.Rows[SelectedRow].Cells[5].Value = dbUpdate.Price * int.Parse(nudSelectItem.Text);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string IDItem = cmbNameOfItem.SelectedValue.ToString();
                int SelectedRow = GetSelectedRow(IDItem);
                if (SelectedRow == -1)
                {
                    SelectedRow = dgvListItemToTransaction.Rows.Add();
                    InsertUpdate(SelectedRow);
                }
                else
                {
                    InsertUpdate(SelectedRow);
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
                string IDItem = cmbNameOfItem.SelectedValue.ToString();
                int SelectedRow = GetSelectedRow(IDItem);
                if (SelectedRow == -1)
                {
                    throw new Exception("Không có sản phẩm này");
                }
                else
                {
                    dgvListItemToTransaction.Rows.RemoveAt(SelectedRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnToltalmoney_Click(object sender, EventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < dgvListItemToTransaction.Rows.Count; i++)
            {
                if (dgvListItemToTransaction.Rows[i].Cells[0].Value != null)
                {
                    sum = sum + int.Parse(dgvListItemToTransaction.Rows[i].Cells[5].Value.ToString());
                }
            }
            txtTotalmoney.Text = sum.ToString();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalmoney.Text == "0")
                    throw new Exception("Vui lòng lấp đầy giỏ hàng của bạn!!");
                Bill db = new Bill();
                {
                    Client dbFind = context.Clients.FirstOrDefault(p => p.FullName == txtUserFullname.Text);
                    //db.Date = DateTime.Now
                    db.ClientID = dbFind.ID;
                    db.TaltolMoney = txtTotalmoney.Text;
                    dbFind.ToltalMoney = dbFind.ToltalMoney + int.Parse(txtTotalmoney.Text);
                    if (dbFind.ToltalMoney > 3500000)
                    {
                        if (dbFind.ToltalMoney > 8000000)
                        {
                            if (dbFind.ToltalMoney >= 15000000)
                            {
                                dbFind.RankID = 4;
                            }
                            dbFind.RankID = 3;
                        }
                        dbFind.RankID = 2;
                    }
                    else
                        dbFind.RankID = 1;
                    context.Bills.Add(db);
                    context.SaveChanges();
                    string IDItem;
                    for (int i = 0; i < dgvListItemToTransaction.Rows.Count; i++)
                    {
                        if (dgvListItemToTransaction.Rows[i].Cells[0].Value != null)
                        {
                            IDItem = dgvListItemToTransaction.Rows[i].Cells[0].Value.ToString();
                            Item item = context.Items.FirstOrDefault(p => p.ID == IDItem);
                            int sum;
                            sum = item.Inventory - int.Parse(dgvListItemToTransaction.Rows[i].Cells[4].Value.ToString());
                            if (sum == 0)
                                context.Items.Remove(item);
                            item.Inventory = sum;
                            context.SaveChanges();
                        }
                    }
                    txtAddress.Text = ""; txtDiscount.Text = ""; txtPhoneNumber.Text = ""; txtTotalmoney.Text = "0"; txtUserFullname.Text = ""; txtSelectPhonenumber.Text = "";
                    dgvListItemToTransaction.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmTrade_Load(object sender, EventArgs e)
        {
            List<Class> Listclasses = context.Classes.ToList();
            FillClassCombobox(Listclasses);
            FillNameItemCombobox();
        }
    }
}
