#include <iostream>
#include <string>
#include <map>
#include <vector>
using namespace std;

int main()
{
    string szoveg;
    cout<<"szoveg:";
    cin>>szoveg;
    string pattern;
    cout<<"pattern:";
    cin>>pattern;
    int abc_length;
    cout<<"abc hossz:";
    cin>>abc_length;
    cout<<"abc karakterei:"<<endl;
    char abc[abc_length];
    map<char, int> shift;

    ///initShift
    for(int i=0;i<abc_length;++i)
    {
        cin>>abc[i];
        shift.insert(pair<char, int>(abc[i],pattern.length()+1));
    }

    for(int j=0;j<pattern.length();++j)
    {
     shift[pattern[j]]= pattern.length()-j;
    }
    ///initShift vége

    cout<<"shift:"<<endl;
    for(int i=0;i<abc_length;++i)
    {
        cout<<abc[i]<<": "<<shift[abc[i]]<<"    ";
    }

    int s=0;
    vector<int> S;
    int compared=0;
    while(s+pattern.length()<=szoveg.length())
    {
        compared++;
        if(szoveg.substr(s,s+pattern.length())==pattern)
        {
            S.push_back(s);
        }
        if(s+pattern.length()<szoveg.length())
        {
            s+=shift[szoveg[s+pattern.length()]];
        } else break;
    }

    for(int i=0;i<S.size();i++)
    {
        cout<<S[i]<<" ";
    }
}
