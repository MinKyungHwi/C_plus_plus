#include<iostream>
#include<cstring>

using namespace std;

int stack[10001], top = -1;

void push(int x) {
	stack[++top] = x;
	cout << stack[top] << "\n";
}

int empty() {
	if (top < 0)
		return 1;
	else return 0;
}

void pop() {
	if (empty() == 1)				//아무것도 없을 때
		cout << "-1" << "\n";
	else {
		cout << stack[top] << "\n";
		stack[top--];
	}
}

int size() {
	return top;
} 

int main() {
	int n;
	cin >> n;

	char str[10001];
	

	for (int i = 0; i < n; i++) {

		cin >> str;

		if (str == "push") {
			int x; cin >> x;
			push(x);

			return 0;
		}
		else if (str == "push") {

		}

	}
}