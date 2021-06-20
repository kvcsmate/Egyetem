#include <iostream>
#include <sstream>
#include <string>
#include <deque>
#include <utility>
#include <fstream>
#include <vector>
#include <future>
using namespace std;
int nextspace(string s,int &start)
{
    string a="";
    unsigned int pos= unsigned(start);
    while(s[pos]!=' '&&pos!=s.length()+1)
    {
        a+=s[pos];
        pos++;
    }
    pos++;
    start=int(pos);
    int b=stoi(a);
    return b;
}

string isconnected(string s)
{
    int start=0;
    int hossz=nextspace(s,start);
    deque<pair<int,int>> graph;

    for(int i=0;i<hossz;++i)
    {
        graph.push_back({nextspace(s,start),nextspace(s,start)});
       // cout<<graph.back().first<<graph.back().second<<endl;
    }



    deque<pair<int,int>> connect;
    connect.push_front(graph.front());
    graph.pop_front();
    //cout<<connect.front().first<<connect.front().second<<endl;
    int tombhossz;
    int ujtombhossz=graph.size();;
    do
    {
        //cout<<"elindult"<<endl;
        tombhossz=ujtombhossz;
        for(int i=0;i<tombhossz;++i)
        {
            int jcycle=connect.size();
            bool prev1=true;
            for(int j=0;j<jcycle&&prev1;++j)
            {
                if(connect.at(j).first==graph.at(i).first || connect.at(j).first==graph.at(i).second||connect.at(j).second==graph.at(i).first||connect.at(j).second==graph.at(i).second)
                {
                    connect.push_back(graph.at(i));
                    prev1=false;
                }
            }
        }
         for(int i=0;i<int(connect.size());++i)
        {
            int jcycle=graph.size();
            bool prev2=true;
            for(int j=0;j<jcycle&&prev2;++j)
            {
                if(connect.at(i)==graph.at(j))
                {
                    graph.at(j)=graph.front();
                    graph.pop_front();
                    prev2=false;
                }
            }

        }
        ujtombhossz=graph.size();
        //cout<<"lefutott"<<endl;
    }while(tombhossz!=ujtombhossz);


    if(graph.size()==0)
    {
        return "yes";
    }else return "no";

}

int main()
{
    std::ifstream input("input.txt");

	 int N;

	input >> N;
	string asd="";
	getline(input,asd);
	string graph;
	std::vector<string> graphs;
	for(int i=0;i<N;++i)
    {
        graph="";
        getline(input,graph);
        graphs.push_back(graph);
    }
    input.close();

    std::vector<std::future<string>> results;
    for(int i=0;i<N;++i)
    {
        results.push_back(std::async(std::launch::async, isconnected,graphs[i]));
    }
    std::ofstream output("output.txt");
    for (std::future<string>& f : results)
	{
		f.wait();
		output << f.get() << std::endl;
	}
	output.close();

    //cout<<isconnected("5 1 3 2 3 1 2 5 4 3 5");

    return 0;
}
