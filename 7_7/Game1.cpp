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
		cout << "1.����\t 2.����\t 3.�� " << endl;
		cin >> player;

		
		if (player > 3 || player < 1) {
			cout << "1���� 3������ ���ڸ� �Է��� �� �ֽ��ϴ�." << endl;
		}
		else {
			player--;
			break;
		}
	}

	cout << "COM : ";

	switch (computer) {

		case SCISSORS:
			cout << "����" << endl;

			if (player == SCISSORS) {
				cout << "�� : ����" << endl << endl;
				cout << "���º� �Դϴ�." << endl << endl;
			}
			else if (player == ROCK) {
				cout << "�� : ����" << endl << endl;
				cout << "�¸� �Դϴ�." << endl << endl;
			}
			else if (player == PAPER) {
				cout << "�� : ��" << endl << endl;
				cout << "�й� �Դϴ�." << endl << endl;
			}
			break;

		case ROCK:
			cout << "�ָ�" << endl;
			if (player == SCISSORS) {
				cout << "�� : ����" << endl << endl;
				cout << "�й� �Դϴ�." << endl << endl;
			}
			else if (player == ROCK) {
				cout << "�� : ����" << endl << endl;
				cout << "���º� �Դϴ�." << endl << endl;
			}
			else if (player == PAPER) {
				cout << "�� : ��" << endl << endl;
				cout << "�¸� �Դϴ�." << endl << endl;
			}
			break;

		case PAPER:
			cout << "��" << endl;
			if (player == SCISSORS) {
				cout << "�� : ����" << endl << endl;
				cout << "�¸� �Դϴ�." << endl << endl;
			}
			else if (player == ROCK) {
				cout << "�� : ����" << endl << endl;
				cout << "�й� �Դϴ�." << endl << endl;
			}
			else if (player == PAPER) {
				cout << "�� : ��" << endl << endl;
				cout << "���º� �Դϴ�." << endl << endl;
			}
			break;
		}

}



int main() {
	int select = 1;

	while (select == 1 ) {
		Game();

		cout << "������ ��� �Ͻ÷��� 1���� �����ּ���." << endl << endl;
		cin >> select;

		system("cls");
	}
	
}