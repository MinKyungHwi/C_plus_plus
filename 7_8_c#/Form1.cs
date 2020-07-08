using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int PreviousWhat = 1; // 오류검출 위한 이전 상태저장 변수(0:숫자, 1:처음, 2:문자)

        int PN = 1;         // 양수음수 구분변수 (양:1, 음:2)

        String Situation;      // 현재까지의 식 보여주는 String 변수
        String ChoiceNumber;   // 입력값 B에 입력하기 위해 String형으로 받는 변수
        int ChoiceMac = 1;       // 연산버튼 클릭 시 종류 구분을 위한 변수
        int PointLimit = 0;         // 점 두번 이상 입력 못하게 하는 변수

        Double A = 0;     // 결과저장 변수
        Double B = 0;     // 입력값 저장 변수
        int K = 1;        // 입력 연산 저장 변수

        Double A2 = 0;    // *, / 우선 계산을 위한 값 임시저장 변수
        int K2 = 1;       // 우선 계산을 위한 연산 변수




            private void bnt_number_Click(object sender, EventArgs e)  //숫자 입력 이벤트
        {
            Button _Button;
            _Button = (Button)sender;

            ChoiceNumber += _Button.Text;
            textBox2.Text = ChoiceNumber;
            PreviousWhat = 0;
        }

        private void Button_Plus_Click(object sender, EventArgs e)
        {
            ChoiceMac = 1;
            SelectCase(PreviousWhat, 1);
            textBox2.Text = "";
            PN = 1;
        }

        private void Button_Minus_Click(object sender, EventArgs e)
        {
            ChoiceMac = 2;
            SelectCase(PreviousWhat, 1);
            textBox2.Text = "";
            PN = 1;
        }

        private void Button_Multiplication_Click(object sender, EventArgs e)
        {
            ChoiceMac = 3;
            SelectCase(PreviousWhat, 2);
            textBox2.Text = "";
            PN = 1;
        }

        private void Button_Division_Click(object sender, EventArgs e)
        {
            ChoiceMac = 4;
            SelectCase(PreviousWhat, 2);
            textBox2.Text = "";
            PN = 1;
        }

        private void Button_Equl_Click(object sender, EventArgs e)
        {
            SelectCase(PreviousWhat, 3);
        }

        private void Button_Point_Click(object sender, EventArgs e)
        {
            PointLimit += 1;
            if(PointLimit < 2) { SelectCase(PreviousWhat, 4); }
            else { MessageBox.Show("포인트(점)은 피연산자에 하나만 입력할 수 있습니다."); }
        }

        private void Button_C_Click(object sender, EventArgs e)
        {
            InitializationC();
        }

        private void Button_PoNe_Click(object sender, EventArgs e)
        {
            if (PN == 1)
            {
                string[] strArray = { "", ChoiceNumber };
                ChoiceNumber = String.Join("-", strArray);
                textBox2.Text = ChoiceNumber;
            }
            else if (PN == 2)
            {
                string[] Result = ChoiceNumber.Split('-');
                ChoiceNumber = Result[1];
                textBox2.Text = ChoiceNumber;
            }

            if (PN == 1) { PN = 2; }
            else if (PN == 2) { PN = 1; }

        }

    //여기부터 메소드-------------------------------------------------------------------------

    // 오류 검출 및 연산 함수(핵심)
    public void SelectCase(int i,int n)   //오류 검출, 연산 선택 각각 담당하는 매개변수
        {

            if (i==0)         // 이전버튼이 숫자인 경우 - 정상작동
            {

                switch (n)
                {
                    case 1:          //Button_Plus, Minus _Click 이벤트

                        B = Convert.ToDouble(ChoiceNumber);
                        SuButtonAction();
                        SelectK(ChoiceMac);
                        PointLimit = 0;
                        break;
                    case 2:          //Button_Multiplication, Division _Click 이벤트

                        B = Convert.ToDouble(ChoiceNumber);
                        MuDiButtonAction();
                        SelectK(ChoiceMac);
                        PointLimit = 0;
                        break;
                    case 3:          //Button_Equl_Click 이벤트
                        B = Convert.ToDouble(ChoiceNumber);
                        SuButtonAction();
                        LastDeployment();
                        SuButtonAction();
                        CalcResult();
                        CalcReady();
                        PointLimit = 0;
                        break;
                    case 4:          //Button_Point_Click 이벤트
                        ChoiceNumber += ".";
                        textBox2.Text = ChoiceNumber;
                        break;
                }

                if (n != 3 && n != 4)
                {       // 점입력, = 입력 시 문자저장 하지 않게 하는 조건
                    switch (ChoiceMac)
                    {
                        case 1:
                            Situation += ChoiceNumber + " ";
                            Situation += "+" + " ";
                            textBox1.Text = Situation;
                            break;
                        case 2:
                            Situation += ChoiceNumber + " ";
                            Situation += "-" + " ";
                            textBox1.Text = Situation;
                            break;
                        case 3:
                            Situation += ChoiceNumber + " ";
                            Situation += "*" + " ";
                            textBox1.Text = Situation;
                            break;
                        case 4:
                            Situation += ChoiceNumber + " ";
                            Situation += "/" + " ";
                            textBox1.Text = Situation;
                            break;
                        default: break;
                    }
                    ChoiceNumber = "";
                }
                if (n == 3) { PreviousWhat = 0; ChoiceMac = 0; } //문자버튼 입력마다 첫입력과 문자입력을 구분
                else { PreviousWhat = 2; ChoiceMac = 0; }
            }

            else if (i==1)   // 첫 입력 오류 - 숫자 아닌 문자 입력 등
            {
                MessageBox.Show("피연산자(숫자)가 필요합니다.");
            }
            else if(i==2)   // 문자 관련 오류 - 연산자만 두번씩 입력하는 등 
            {
                MessageBox.Show("연산자(.포함)를 연속으로 입력할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("알 수 없는 오류.");
            }

        }
        // (핵심) 끝--------------------------------------------------------


        public void InitializationC()
        {
            Situation = "";
            ChoiceNumber = "";
            ChoiceMac = 1;
            A = 0;
            B = 0;
            K = 1;
            A2 = 0;
            K2 = 1;
            textBox1.Text = "";
            textBox2.Text = "";
            PreviousWhat = 1;
            PointLimit = 0;
            PN = 1;
        }


        public void SelectK(int i)
        {
            K = i;
        }


        public void SuButtonAction()
        {

            switch (K)
            {
                case 1:
                    A = A + B;
                    break;
                case 2:
                    B = -B;
                    A = A + B;
                    break;
                case 3:
                    A = A * B;
                    break;
                case 4:
                    if (B == 0) { MessageBox.Show("나누기 0은 할 수 없는 계산입니다."); InitializationC();}
                    else { A = A / B; }
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }



        public void MuDiButtonAction()
        {
            switch (K)
            {
                case 3:
                    A = A * B;
                    break;
                case 4:
                    if (B == 0) { MessageBox.Show("나누기 0은 할 수 없는 계산입니다."); InitializationC();}
                    else { A = A / B; }
                    break;

                default:
                    switch (K)
                    {
                        case 1:
                            A2 = A2 + A;
                            A = B;
                            K = ChoiceMac;
                            break;
                        case 2:
                            A2 = A2 + A;
                            A = -B;
                            K = ChoiceMac;
                            break;
                    }
                    break;
            }
        }

        public void LastDeployment()
        {
            K = K2;
            B = A;
            A = A2;
        }

        public void CalcResult()
        {
            A = Math.Round(A, 15);
            ChoiceNumber = Convert.ToString(A);
            textBox2.Text = ChoiceNumber;
            Situation = "";
            textBox1.Text = Situation;
        }

        public void CalcReady()
        {
            B = A;
            A = 0;
            K = 1;
            A2 = 0;
            K2 = 1;
            ChoiceNumber = Convert.ToString(B);
        }


        /// text Box 1 ,2  줄바꿈 및 커서 코딩
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.ScrollToCaret();
        }
    }
}
