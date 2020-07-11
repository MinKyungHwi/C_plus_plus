#include<iostream>

using namespace std;

int main() {
	signed int n1, n2;

	cin >> n1;
	cin >> n2;

	if (n1 > 0 && n2 > 0) {
		cout << "1" ;
	}
	else if (n1 < 0 && n2 > 0)
	{
		cout << "2";
	}
	else if (n1 < 0 && n2 < 0)
	{
		cout << "3";
	}
	else if(n1 >0 && n2 < 0)
	{
		cout << "4" ;
	}

}