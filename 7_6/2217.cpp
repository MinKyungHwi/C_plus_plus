#include<iostream>
#include<algorithm>
#include<string>

using namespace std;

#define MAX 100000

int n;
int ary[MAX + 1];
int sum;
int compare;

bool under_count(int a, int b) {
	return a > b;
}

int main() {

	cin >> n;
	for (int i = 0; i < n; i++) {
		cin >> ary[i];
	}

	sort(ary, ary + n , under_count);

	for (int i = 0; i < n; i++) {
		sum = ary[i] * (i + 1);
		if (sum > compare) {
			compare = sum;
		}
	}

	cout << compare << endl;

	return 0;

}