#include <iostream>
#include <thread>

using namespace std;


void func1() {
	for (int i = 0; i < 10; i++) {
		cout << "������ 1 �۵���! \n";
	}
}

void func2() {
	for (int i = 0; i < 10; i++) {
		cout << "������ 2 �۵���! \n";
	}
}

void func3() {
	for (int i = 0; i < 10; i++) {
		cout << "������ 3 �۵���! \n";
	}
}
	thread t1(func1);
	thread t2(func2);
	thread t3(func3);

	
int main() {
	t1.join();
	t2.join();
	t3.join();
}