using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proto.Panel.sell;
using db;
using proto.Data;

namespace proto.Panel
{
    public partial class SellPane : UserControl
    {
        private int sum = 0;

        public SellPane()
        {
            InitializeComponent();

            FillList("", "");
        }

        private void FillList(string category, string search)
        {
            list_book.Controls.Clear();

            List<Book> bookList;
            if (category == "" || search == "")
            {
                bookList = DBC.GetInstance().SelectRecord<Book>("book", "");
            }
            else
            {
                bookList = DBC.GetInstance().SelectRecord<Book>("book", category + " Like " + "'%" + search + "%'");
            }

            foreach (Book b in bookList)
            {
                book_item item = new book_item(b);
                item.Click += new System.EventHandler(this.item_click);
                item.DoubleClick += new System.EventHandler(this.item_double_click);
                list_book.Controls.Add(item);
            }
        }

        private void item_click(object sender, EventArgs e)
        {
            txt_isbn.Text = ((book_item)sender).lbl_isbn_data.Text;
            lbl_title.Text = ((book_item)sender).lbl_title_data.Text;
        }

        private void item_double_click(object sender, EventArgs e)
        {
            book_item item = ((book_item)sender);

            if (Int32.Parse(item.lbl_amount_data.Text) == 0)
            {
                MessageBox.Show("재고가 없습니다.", "주의!");
                return;
            }

            sum += item.book_info.price;
            lbl_price_sum.Text = string.Format("{0:#,###}", sum) + "\\";

            for (int i = 0; i < grid_sell.Rows.Count; ++i)
            {
                if (grid_sell.Rows[i].Cells[0].Value == null)
                    continue;
                if(Int32.Parse((string)grid_sell.Rows[i].Cells[0].Value) == item.book_info.ISBN)
                {
                    int amount = Int32.Parse((string)grid_sell.Rows[i].Cells[1].Value);

                    if (Int32.Parse(item.lbl_amount_data.Text) == amount)
                    {
                        MessageBox.Show("재고가 부족합니다.", "주의!");
                        return;
                    }    
                    amount++;
                    grid_sell.Rows[i].Cells[1].Value = amount.ToString();

                    return;
                }
            }

            grid_sell.Rows.Add(
                new object[]
                {
                    item.lbl_isbn_data.Text,
                    "1",
                    item.lbl_title_data.Text,
                    item.lbl_price_data.Text
                });
        }

        private void btn_sell_record_Enter(object sender, EventArgs e)
        {
            btn_sell_record.ForeColor = Color.DarkGray;
        }

        private void btn_sell_record_Leave(object sender, EventArgs e)
        {
            btn_sell_record.ForeColor = Color.White;
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            FillList(combo_search.Text, txt_search.Text);
        }

        private void grid_sell_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grid_sell.SelectedRows[0].Cells[0].Value == null)
                return;

            string price = ((string)grid_sell.SelectedRows[0].Cells[3].Value);
            price = price.Substring(0, price.Length - 1);
            price = price.Replace(",","");
            sum -= Int32.Parse(price);
            lbl_price_sum.Text = string.Format("{0:#,###}", sum) + "\\";

            if (Int32.Parse((string)grid_sell.SelectedRows[0].Cells[1].Value) == 1)
                grid_sell.Rows.Remove(grid_sell.SelectedRows[0]);
            else
            {
                grid_sell.SelectedRows[0].Cells[1].Value = (Int32.Parse((string)grid_sell.SelectedRows[0].Cells[1].Value) - 1).ToString();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            grid_sell.Rows.Clear();
            sum = 0;
            lbl_price_sum.Text = string.Format("{0:#,###}", sum) + "\\";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(grid_sell.Rows.Count == 1)
            {
                MessageBox.Show("판매할 도서가 없습니다.", "주의!");
                return;
            }

            List<Book> bookList = new List<Book>();
            for(int i = 0; i < grid_sell.Rows.Count - 1; ++i)
            {
                Book b = new Book();
                b.ISBN = Int32.Parse(grid_sell.Rows[i].Cells[0].Value.ToString());
                b.amount = Int32.Parse(grid_sell.Rows[i].Cells[1].Value.ToString());
                b.name = grid_sell.Rows[i].Cells[2].Value.ToString();

                string price = grid_sell.Rows[i].Cells[3].Value.ToString();
                price = price.Substring(0, price.Length - 1);
                price = price.Replace(",", "");
                b.price = Int32.Parse(price) * b.amount;

                bookList.Add(b);
            }


            sell_form form = new sell_form(bookList);
            if(form.ShowDialog() != DialogResult.Cancel)
            {
                grid_sell.Rows.Clear();
                sum = 0;
                lbl_price_sum.Text = string.Format("{0:#,###}", sum) + "\\";
                FillList("", "");
            }
        }

        private void btn_sell_record_Click(object sender, EventArgs e)
        {
            sales_record form = new sales_record();
            form.ShowDialog();
        }
    }
}
