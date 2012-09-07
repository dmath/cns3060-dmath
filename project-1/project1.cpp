#include <iostream>
using namespace std;

int main(int argc, char* argv[])
{
	cout<< "Daniel Matheson"<<endl;
	cout<< "CS 3060"<<endl;
	for(int i = 0; i < argc; i++)
	{
		cout<<argv[i]<<endl;
	}
	return 0;
} 
