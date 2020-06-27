using db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using proto.Data;

namespace proto.Panel.sell
{
    public partial class sales_record : Form
    {
        Point mouse_down;
        bool click;

        public sales_record()
        {
            InitializeComponent();

            FIll_List("", "");
        }

        private void FIll_List(string combo, string search)
        {
            grid_record.Rows.Clear();
            List<Sell> sell_list;

            try
            {
                string query = "";
                if (combo == "" || search == "")
                    query = "Select 영수증번호, Sum(가격) as price, 고객번호, 날짜, 포인트사용 from sell, customer Group by 영수증번호, 고객번호, 날짜, 포인트사용";
                else if(combo == "이름")
                    query = "Select 영수증번호, Sum(가격) as price, 고객번호, 날짜, 포인트사용 from sell, customer where customer.ID = sell.고객번호 and " + combo + " Like '%" + search + "%' Group by 영수증번호, 고객번호, 날짜, 포인트사용";
                else
                    query = "Select 영수증번호, Sum(가격) as price, 고객번호, 날짜, 포인트사용 from sell, customer where " + combo + " Like '%" + search + "%' Group by 영수증번호, 고객번호, 날짜, 포인트사용";

                OleDbCommand cmd = new OleDbCommand(query, DBC.GetInstance().conn);
                cmd.CommandType = System.Data.CommandType.Text;
                OleDbDataReader read = cmd.ExecuteReader();

                sell_list = new List<Sell>();

                while (read.Read())
                {
                    if (read["영수증번호"].ToString() == "0")
                        continue;
                    grid_record.Rows.Add(new object[]
                        {
                            read["영수증번호"].ToString(),
                            string.Format("{0:#,###}", Int32.Parse(read["price"].ToString())) + "\\",
                            read["고객번호"].ToString() == "0" ? "" : read["고객번호"].ToString(),
                            read["고객번호"].ToString() == "0" ? "" : DBC.GetInstance().SelectRecord<Customer>("customer", "ID = " + read["고객번호"].ToString())[0].name,
                            read["날짜"].ToString(),
                            read["포인트사용"].ToString()
                        });
                }

                read.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            mouse_down = new Point(e.X, e.Y);
        }

        private void titlebar_MouseUp(object sender, MouseEventArgs e)
        {
            click = false;
        }

        private void titlebar_MouseMove(object sender, MouseEventArgs e)
        {
            if (click)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - mouse_down.X, p.Y - mouse_down.Y);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_record_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            grid_detail.Rows.Clear();

            List<Sell> sell_list;

            try
            {
                string query = "Select book.ISBN, book.도서명, sell.수량, sell.가격 from sell, book where sell.영수증번호 = " + grid_record.SelectedRows[0].Cells[0].Value + " and sell.ISBN = book.ISBN";
                OleDbCommand cmd = new OleDbCommand(query, DBC.GetInstance().conn);
                cmd.CommandType = System.Data.CommandType.Text;
                OleDbDataReader read = cmd.ExecuteReader();

                sell_list = new List<Sell>();

                while (read.Read())
                {
                    grid_detail.Rows.Add(new object[]
                        {
                            read["ISBN"].ToString(),
                            read["도서명"].ToString(),
                            read["수량"].ToString(),
                            string.Format("{0:#,###}", Int32.Parse(read["가격"].ToString())) + "\\"
                        });
                }

                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            FIll_List(combo_record.Text, txt_search.Text);
        }

        private void btn_return_Click(object sender, EventArgs e)
        {
            if(grid_detail.SelectedRows.Count == 1 || grid_detail.SelectedRows.Count == 0)
            {
                if(grid_detail.SelectedRows[0].Cells[0].Value == null)
                {
                    MessageBox.Show("환불할 도서를 선택하세요.", "주의!");
                    return;
                }
            }
            if(grid_detail.SelectedRows.Count > 0)
            {
                for(int i = 0; i < grid_detail.SelectedRows.Count; ++i)
                {
                    if (grid_detail.SelectedRows[i].Cells[0].Value == null)
                        continue;

                    Sell item = DBC.GetInstance().SelectRecord<Sell>("sell", "영수증번호 = " + grid_record.SelectedRows[0].Cells[0].Value + " and ISBN = " + grid_detail.SelectedRows[i].Cells[0].Value)[0];
                    item.returns = "True";
                    DBC.GetInstance().UpdateRecord<Sell>(item);
                }
            }

            this.Close();
        }
    }
}
