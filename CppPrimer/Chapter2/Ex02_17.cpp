#include <iostream>

using namespace std;

int main(int argc, char **argv)
{
	int i, &ri = i;
	i = 5; ri = 10;
	cout << i << " " << ri << endl;
	getchar();
	return 0;
}