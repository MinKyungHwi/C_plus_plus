using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace proto.Panel
{
    public partial class PurchasePane : UserControl
    {
        string strSQL = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=book.accdb;Persist Security Info=False";
        OleDbCommand cmd = null;
        OleDbConnection conn = null;
        string selectBook = null;
        int count = 0;

        public PurchasePane()
        {
            InitializeComponent();
            LoadData();
            LoadCompany();
        }

        //데이터 로드
        public void LoadData() {
            if (conn == null)
            {
                conn = new OleDbConnection(strSQL);
                conn.Open();
            }
            string strQry = "SELECT * FROM book";
            OleDbDataAdapter oleData = new OleDbDataAdapter(strQry, conn);
            DataTable AccessData = new DataTable();

            dataGridView1.DataSource = AccessData;
            AccessData.Clear();
            oleData.Fill(AccessData);
        }

        //콤보 박스 회사명 값
        public void LoadCompany() {
            conn = new OleDbConnection(strSQL);
            conn.Open();
            string cmbSql = "SELECT 사명 FROM company Group by 사명";
            cmd = new OleDbCommand(cmbSql, conn);
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                comboBox2.Items.Add(read.GetString(0));
            }
        }

        //텍스트박스 비우기
        public void ClearTxt() {
            isbnTxt.Text = null;
            titleTxt.Text = null;
            pubTxt.Text = null;
            stockTxt.Text = null;
            authorTxt.Text = null;
            costTxt.Text = null;
            getTxt.Text = null;
        }

        //검색
        private void search_Click(object sender, EventArgs e)
        {
            string com = comboBox1.SelectedItem.ToString();
            string text = textBox1.Text.ToString();
            string selectQry = "SELECT * FROM book WHERE " + com + " LIKE " + "'" + text + "%'";

            if (textBox1.Text == "")
            {
                MessageBox.Show("검색할 내용을 입력하세요");
                return;
            }

            conn = new OleDbConnection(strSQL);
            conn.Open();

            OleDbDataAdapter oleData = new OleDbDataAdapter(selectQry, conn);
            DataTable AccessData = new DataTable();
            dataGridView1.DataSource = AccessData;
            AccessData.Clear();
            oleData.Fill(AccessData);
            conn.Close();
        }

        //데이터 그리드 선택
        private void dataGridcellClick(object sender, DataGridViewCellEventArgs e)
        {
            isbnTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();//isbn
            titleTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();//제목
            pubTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();//출판사
            stockTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//재고
            authorTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();//저자
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();//발행날짜
            costTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();//가격
            getTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();//매입가격
        }

        //추가
        private void addNew_Click(object sender, EventArgs e)
        {
            double isbn = Convert.ToDouble(isbnTxt.Text.ToString());
            string title_st = titleTxt.Text.ToString();
            string pub_st = pubTxt.Text.ToString();
            int stock_int = Convert.ToInt32(stockTxt.Text.ToString());
            string au_st = authorTxt.Text.ToString();
            string dt = dateTimePicker1.Text.ToString();
            double cost_n = Convert.ToDouble(costTxt.Text.ToString());
            double get_n = Convert.ToDouble(getTxt.Text.ToString());

            conn = new OleDbConnection(strSQL);
            conn.Open();

            string inQry = "INSERT INTO book (ISBN, 도서명, 출판사, 재고, 저자, 발행날짜, 가격, 매입가격) VALUES (" + isbn + ",'" + title_st + "','" + pub_st + "'," + stock_int + ",'" + au_st + "','" + dt + "'," + cost_n + "," + get_n + ")";

            cmd = new OleDbCommand(inQry, conn);

            try
            {
                int x = cmd.ExecuteNonQuery();
                if (x == 1)
                    MessageBox.Show("정상 입력되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "입력실패");
            }
            finally
            {
                conn.Close();
                conn = null;
            }
            LoadData();
            ClearTxt();
        }

        //수정
        private void modify_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;

            if (isbnTxt.Text == "")
            {
                MessageBox.Show("수정할 데이터를 선택하세요");
                return;
            }

            string UpSql = "UPDATE book SET 도서명= '"+titleTxt.Text.ToString()+"', 출판사='"+pubTxt.Text.ToString()+"', " +
                "재고="+Convert.ToInt32(stockTxt.Text)+", 저자='"+authorTxt.Text.ToString()+"', 발행날짜='"+dt+"', " +
                "가격="+Convert.ToDouble(costTxt.Text)+", 매입가격="+Convert.ToDouble(getTxt.Text)+" WHERE ISBN="+Convert.ToDouble(isbnTxt.Text);

            if (conn == null)
            {
                conn = new OleDbConnection(strSQL);
                conn.Open();
            }
            cmd = new OleDbCommand(UpSql, conn);
            try
            {
                int x = cmd.ExecuteNonQuery();
                if (x >= 1)
                    MessageBox.Show("수정 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러 발생");
            }
            conn.Close();
            conn = null;

            LoadData();
            ClearTxt();
        }

        //삭제
        private void delete_Click(object sender, EventArgs e)
        {
            if (titleTxt.Text == "")
            {
                MessageBox.Show("삭제할 데이터를 선택하세요");
                return;
            }

            if (conn == null)
            {
                conn = new OleDbConnection(strSQL);
                conn.Open();
            }
            string dSql = "DELETE FROM book WHERE ISBN=" + Convert.ToInt32(isbnTxt.Text.ToString());
            cmd = new OleDbCommand(dSql, conn);
            try
            {
                int x = cmd.ExecuteNonQuery();
                if (x == 1)
                    MessageBox.Show("삭제 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "삭제 실패");
            }
            conn.Close();
            conn = null;

            LoadData();
            ClearTxt();
        }

        //매입
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text == "")
            {
                MessageBox.Show("회사명을 선택해주세요.", "주의!");
                return;
            }

            if (conn == null)
            {
                conn = new OleDbConnection(strSQL);
                conn.Open();
            }
            string dSql = "SELECT MAX(NUM) FROM T_order";
            cmd = new OleDbCommand(dSql, conn);
            int no = Int32.Parse(cmd.ExecuteScalar().ToString()) + 1;

            for(int i = 0; i < dataGridView2.Rows.Count - 1; ++i)
            {
                int amount = Int32.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                dSql = "SELECT 매입가격 FROM book Where ISBN = " + dataGridView2.Rows[i].Cells[0].Value.ToString();
                cmd = new OleDbCommand(dSql, conn);
                int price = Int32.Parse(cmd.ExecuteScalar().ToString());

                dSql = "Insert Into T_order (NUM, 사명, ISBN, 구매일, 매입가격, 갯수) Values (" + no + ", '" + comboBox2.Text + "', " + dataGridView2.Rows[i].Cells[0].Value.ToString()
                    + ", NOW(), " + price * amount + ", " + amount + ")";
                cmd = new OleDbCommand(dSql, conn);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
            dataGridView2.Rows.Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ISBN = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string bookName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            for (int i = 0; i < dataGridView2.Rows.Count - 1; ++i)
            {
                if (dataGridView2.Rows[i].Cells[0].Value.ToString().CompareTo(ISBN) == 0)
                {
                    int amount = Int32.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                    dataGridView2.Rows[i].Cells[2].Value = amount + 1;
                    return;
                }
            }

            dataGridView2.Rows.Add(new object[] { ISBN, bookName, 0 });
        }
    }
}
