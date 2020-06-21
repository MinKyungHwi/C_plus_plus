#include<iostream>
#include<cstring>

using namespace std;

struct Queue {
	int data[10001];
	int begin, end;

	Queue() {
		begin = 0;
		end = 0;
	}

	void push(int num) {
		data[end] = num;
		end += 1;
	}

	int pop() {
		if (empty() == 1)
			return -1;
		else
		{
			begin += 1;
			return data[begin - 1];
		}
	}

	int size() {
		return end - begin;
	}

	bool empty() {
		return (size() == 0) ? 1 : 0;
	}

	int front() {
		if (empty() == 1)
			return -1;
		else
			return data[begin];
	}

	int back() {
		if (empty() == 1)
			return -1;
		else
			return data[end - 1];
	}
};



int main() {

	int N;
	cin >> N;

	Queue q;

	for (int i = 0; i < N; i++) {
		string str;
		cin >> str;

		if (str == "push" || str == "PUSH") {
			int num;
			cin >> num;
			q.push(num);
		}

		else if (str == "pop" || str == "POP")
		{
			cout << q.pop() << endl;
		}

		else if (str == "size" || str == "SIZE")
		{
			cout << q.size() << endl;
		}

		else if (str == "empty" || str == "EMPTY")
		{
			cout << q.empty() << endl;
		}

		else if (str == "front" || str == "FRONT")
		{
			cout << q.front() << endl;
		}

		else if (str == "back" || str == "BACK")
		{
			cout << q.back() << endl;
		}
	}
}