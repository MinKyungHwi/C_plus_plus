#include <iostream>
#include <string>

using namespace std;

int main()
{

	string s;
	string temp;
	int count = 0;
	cin >> s;
	
	int ans = 0;
	bool bFirst = false;
	

	for(int i = 0 ; i <= s.length(); i++)
	{
		if (s[i] == '+' || s[i] == '-' || s[i] == '\0')
		{
			if (bFirst)
			{
				ans -= stoi(temp);
			}
			else
			{
				ans += stoi(temp);
			}

			if (s[i] == '-')
				bFirst = true;

			temp.clear();
			temp.shrink_to_fit();
			continue;

		}
		temp += s[i];
		
	}

	cout << ans << endl;

	return 0;
}

