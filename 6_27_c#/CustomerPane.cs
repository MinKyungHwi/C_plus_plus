using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using proto.Data;
using db;

namespace proto.Panel
{
    public partial class CustomerPane : UserControl
    {
        public CustomerPane()
        {
            InitializeComponent();

            FillList("", "");
        }

        private void FillList(string category, string search)
        {
            list_customer.Rows.Clear();

            List<Customer> cusList;
            if (category == "" || search == "")
            {
                cusList = DBC.GetInstance().SelectRecord<Customer>("customer", "");
            }
            else
            {
                cusList = DBC.GetInstance().SelectRecord<Customer>("customer", category + " Like " + "'%" + search + "%'");
            }

            foreach(Customer cs in cusList)
            {
                list_customer.Rows.Add(new object[] { cs.number, cs.name, cs.phone, cs.sales, cs.point });
            }
        }

        private void list_customer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_ID.Text = list_customer.SelectedRows[0].Cells[0].Value.ToString();
            txt_Name.Text = list_customer.SelectedRows[0].Cells[1].Value.ToString();
            txt_Phone.Text = list_customer.SelectedRows[0].Cells[2].Value.ToString();
            txt_Point.Text = list_customer.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            FillList(combo_search.Text, txt_Search.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Customer cs = new Customer();
            cs.name = txt_Name.Text;
            cs.phone = txt_Phone.Text;

            DBC.GetInstance().InsertRecord<Customer>(cs);

            FillList("", "");
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if(lbl_ID.Text == "")
            {
                MessageBox.Show("수정할 정보를 선택해주세요.", "주의");
                return;
            }

            Customer cs = DBC.GetInstance().SelectRecord<Customer>("customer", "ID = " + lbl_ID.Text)[0];
            cs.point = Int32.Parse(txt_Point.Text);
            cs.phone = txt_Phone.Text;
            cs.name = txt_Name.Text;

            DBC.GetInstance().UpdateRecord<Customer>(cs);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DBC.GetInstance().DeleteRecord("customer", lbl_ID.Text);
        }
    }
}
