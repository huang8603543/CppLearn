#include <iostream>
#include <vector>
#include <string>

using namespace std;

int main(int argc, char **argv)
{
	string str;
	vector<string> vec2;
	while (cin >> str)
	{
		vec2.push_back(str);
	}
	getchar();
	return 0;
}