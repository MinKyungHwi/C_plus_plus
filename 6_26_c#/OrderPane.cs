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

// 변수 3개중 2개 입력시 가능하게
// 콤보박스 load 함수
// 
   
namespace proto.Panel
{
   
    public partial class OrderPane : UserControl
    {
        OleDbConnection con =new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=book.accdb; Persist Security Info=False");
        public string targetPath = Application.StartupPath + @"Data Source=order.accdb;";

        //
        //book
        //등록 Group 시작
        //
        //
        public OrderPane()
        {
            InitializeComponent();

            // 매입처 사명 콤보박스 
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT 사명 from company";
            cmd.Connection = con;
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                store_name.Items.Add(read.GetString(0));
                order_store_name.Items.Add(read.GetString(0));

            }


            OleDbCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT ISBN from T_order";
            cmd2.Connection = con;
            OleDbDataReader read2 = cmd2.ExecuteReader();
            while (read2.Read())
            {
                comboBox1.Items.Add(read2[0].ToString());
            }

            con.Close();
        }
        
        void combo()
        {
            store_name.Items.Clear();

            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT 사명 from company";
            cmd.Connection = con;
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                store_name.Items.Add(read.GetString(0));
                order_store_name.Items.Add(read.GetString(0));
            }
            con.Close();
        }

        void combo2()
        {
            store_name.Items.Clear();

            con.Open();
            OleDbCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT ISBN from T_order";
            cmd2.Connection = con;
            OleDbDataReader read2 = cmd2.ExecuteReader();
            while (read2.Read())
            {
                comboBox1.Items.Add(read2[0].ToString());
            }
            con.Close();
        }

        //등록 버튼
        private void Btn_store_add_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO company (사명, 주소, 전화번호) VALUES ('" + text_store_name.Text + "', '" + text_store_addr.Text + "', " + text_store_tel.Text + ") ";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(" 등록 완료 ");
                combo();
            }
            catch

            {
                MessageBox.Show(" 등록 실패 ");
            }
        }

        //수정 버튼
        private void Btn_store_modify_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE company SET 주소='" + text_store_addr.Text + "', 전화번호= " + text_store_tel.Text + " where 사명= '" + store_name.Text + "' ";
                cmd.ExecuteNonQuery();
                cmd.Connection = con;
                MessageBox.Show(" 수정 완료 ");
            }
            catch(Exception ex)
            {
                MessageBox.Show(" 수정 실패 " + ex.Message);
            }
            finally
            {
                con.Close();

                text_store_tel.Text = "";
                text_store_addr.Text = "";
                store_name.Text = "";
            }
        }

        //삭제 버튼
        private void Btn_store_delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM company where 사명='" + store_name.Text + "' ";
                cmd.ExecuteNonQuery();
                cmd.Connection = con;
                con.Close();
                MessageBox.Show(" 삭제 완료 ");
                combo();
            }
            catch
            {
                MessageBox.Show(" 삭제 실패 ");
            }
        }





        //
        //
        //주문 Group 시작
        //
        //
        //주문 전체 조회 버튼
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NUM, 사명, 구매일, sum(매입가격) as 총가격, sum(갯수) as 총갯수 from T_order group by NUM ,사명, 구매일";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.AllowUserToResizeColumns = true;
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch
            {
                MessageBox.Show("조회 실패");
            }
        }

        // 매입처별 조회 버튼
        private void Btn_company_order_check_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * from T_order where 사명 = '" + order_store_name.Text + "' ";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.AllowUserToResizeColumns = true;
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch
            {
                MessageBox.Show(" 조회 실패 ");
            }
        }


        // 날짜별 주문 조회 버튼
        private void Btn_date_order_check_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * FROM T_order WHERE 구매일 BETWEEN #" + dateTimePicker1.Text + "# AND #" + dateTimePicker2.Text + "# ";
            cmd.ExecuteNonQuery();

            //"Select * FROM T_order  '" + dateTimePicker1.Text + "' ";

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.DataSource = dt;
            con.Close();
        }



        //주문 취소 버튼
        private void Btn_book_order_cancle_Click_1(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM T_order where ISBN = " + comboBox1.Text + " ";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("삭제 완료");
                combo2();

            }
            catch
            {
                MessageBox.Show("삭제 실패");
            }
        }




        //동종 도서 수량 변경 버튼




        //
        //
        //
        //
        //
        //Gridview 클릭 시 
        class DataConn
        {
            public DataSet GetDataset(string sql, string DB_path)
            {
                string connStr = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=order.accdb ;Persist Security Info=False";


                OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connStr);
                DataSet ds = new DataSet();
                OleDbDataAdapter adp = new OleDbDataAdapter(sql, conn);
                
                return ds;

            }
        }
        

        private void dataGridView1_Click(object sender, DataGridViewCellEventArgs e)
        {
            /*
            
            string DB_path = targetPath;

            DataSet ds = new DataSet();
            DataConn conn1 = new DataConn();
            string sql = @"SELECT T_order.도서명, book.출판사, T_order.갯수, T_order.매입가격, T_order 구매일
                          FROM T_order JOIN Orders ON T_order.도서명 = book.도서명
                          WHERE (((T_order.order_ID)='" + ID_1 + "'))";
            ds = conn1.GetDataset(sql, DB_path);
            dataGridView2.DataSource = ds.Tables[0];
            */

         
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string ID_1 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmd.CommandText = "Select  book.ISBN , book.도서명, T_order.갯수, T_order.매입가격, T_order.구매일 " +
                 "FROM T_order,book " +
                 "WHERE (T_order.ISBN = book.ISBN) and NUM = " +  ID_1 + " ";
            cmd.ExecuteNonQuery();
            cmd.Connection = con;
            con.Close();

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.AllowUserToResizeColumns = true;
            dataGridView2.DataSource = dt;
            con.Close();
         

            dataGridView1.CurrentRow.Selected = true;




            /*
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  from T_order";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.DataSource = dt;
            con.Close();
            */
        }

        private void store_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * FROM company WHERE 사명 = '" + store_name.SelectedItem.ToString() + "'";

            //"Select * FROM T_order  '" + dateTimePicker1.Text + "' ";

            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                text_store_addr.Text = read["주소"].ToString();
                text_store_tel.Text = read["전화번호"].ToString();
            }
            con.Close();
        }
    }
}
