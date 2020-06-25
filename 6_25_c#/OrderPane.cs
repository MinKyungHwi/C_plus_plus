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



namespace proto.Panel
{
   
    public partial class OrderPane : UserControl
    {
        OleDbConnection con =new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=C:order.accdb; Persist Security Info=False");

        public OrderPane()
        {
            InitializeComponent();

            Drop_store_name.Clear(); // 매입처 Dropdown 초기화


        }

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
            }
            catch
            {
                MessageBox.Show(" 등록 실패 ");
            }
            
        }

        private void Btn_store_modify_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE company SET 사명='" + Drop_store_name.Text + "', 주소='" + text_store_addr.Text + "', 전화번호= " + text_store_tel.Text + "' ";
                cmd.ExecuteNonQuery();
                cmd.Connection = con;
                con.Close();
                MessageBox.Show(" 수정 완료 ");
            }
            catch
            {
                MessageBox.Show(" 수정 실패 ");
            }
        }

        private void Drop_store_name_onItemSelected(object sender, EventArgs e)
        {

            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT 사명 from company";
            cmd.Connection = con;
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                Drop_store_name.AddItem(read.GetString(0));
            }
            con.Close();

        


            /* 
            
            try
            {
                con.Open();
                
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 사명 from company";
                OleDbDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Drop_store_name.AddItem(read.GetString(0));
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
            */


            /*
              try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 사명 from company";
                OleDbDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Drop_store_name.AddItem(read["사명"].ToString());
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
             */



            //cmd.Connection = con;

            /*
            Bunifu.Framework.UI.BunifuDropdown dropdown = new Bunifu.Framework.UI.BunifuDropdown();
            void addFromDb(Bunifu.Framework.UI.BunifuDropdown dropdown)
            {
             
                OleDbCommand cmd = con.CreateCommand();
                OleDbCommand con = new OleDbCommand("server=127.0.0.1;uid=admin;password=;database=bunifu_tests;");
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT `gender` FROM `students` WHERE 1", con);
                OleDbCommand read = cmd.ExecuteReader();
                while (read.Read())
                {
                    bunifuDropdown.Items.Add(read.GetString(0));
                }
                con.Close();
            }
            */

        }
    }
}




// '" + + "'  일 경유 숫자변수 받을 때는 '' 안하고 "+변수명+" 이렇게 사용함    Error: 조건식의 데이터 형식이 일치하지 않습니다.

// 필요한 매개 변수 중 한 개 이상에 값이 주어지지 않았습니다. -> Error 해결 데이터베이스 

// DB관련
// 참고 https://www.youtube.com/watch?v=c8862eE1ykg
// 참고 https://www.youtube.com/watch?v=AE-PS6-sL7U&list=PLS1QulWo1RIZLd9EIGHZ5GnDbQJFh_9Lu&index=2
// 참고 https://sosobaba.tistory.com/240
// 참고 https://sosobaba.tistory.com/240?category=797941

// DB 연결 
// 참고 https://www.connectionstrings.com/ 



//6.1
//참고 https://www.youtube.com/watch?v=OtPy_yWiRA0