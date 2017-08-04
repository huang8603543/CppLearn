#include <iostream>
#include <string>
#include <cctype>

using namespace std;

int main(int argc, char **argv)
{
	string str;
	while (cin >> str)
	{
		string output;
		for (decltype(str.size()) index = 0; index < str.size(); index++)
		{
			if (!ispunct(str[index]))
			{
				output += str[index];
			}
		}
		cout << output << endl;
	}

	return 0;
}