#include<iostream>
#include<ctime>

using namespace std;

enum STATE {

	SCISSORS,
	ROCK,
	PAPER,
};

void Game() {

	int computer;
	int player;

	srand((unsigned)time(NULL));
	computer = rand() % 3; // 0 ~ 2

	
	

	while (true) {
		cout << "1.가위\t 2.바위\t 3.보 " << endl;
		cin >> player;

		
		if (player > 3 || player < 1) {
			cout << "1부터 3까지의 숫자만 입력할 수 있습니다." << endl;
		}
		else {
			player--;
			break;
		}
	}

	cout << "COM : ";

	switch (computer) {

		case SCISSORS:
			cout << "가위" << endl;

			if (player == SCISSORS) {
				cout << "나 : 가위" << endl << endl;
				cout << "무승부 입니다." << endl << endl;
			}
			else if (player == ROCK) {
				cout << "나 : 바위" << endl << endl;
				cout << "승리 입니다." << endl << endl;
			}
			else if (player == PAPER) {
				cout << "나 : 보" << endl << endl;
				cout << "패배 입니다." << endl << endl;
			}
			break;

		case ROCK:
			cout << "주먹" << endl;
			if (player == SCISSORS) {
				cout << "나 : 가위" << endl << endl;
				cout << "패배 입니다." << endl << endl;
			}
			else if (player == ROCK) {
				cout << "나 : 바위" << endl << endl;
				cout << "무승부 입니다." << endl << endl;
			}
			else if (player == PAPER) {
				cout << "나 : 보" << endl << endl;
				cout << "승리 입니다." << endl << endl;
			}
			break;

		case PAPER:
			cout << "보" << endl;
			if (player == SCISSORS) {
				cout << "나 : 가위" << endl << endl;
				cout << "승리 입니다." << endl << endl;
			}
			else if (player == ROCK) {
				cout << "나 : 바위" << endl << endl;
				cout << "패배 입니다." << endl << endl;
			}
			else if (player == PAPER) {
				cout << "나 : 보" << endl << endl;
				cout << "무승부 입니다." << endl << endl;
			}
			break;
		}

}



int main() {
	int select = 1;

	while (select == 1 ) {
		Game();

		cout << "게임을 계속 하시려면 1번을 눌려주세요." << endl << endl;
		cin >> select;

		system("cls");
	}
	
}