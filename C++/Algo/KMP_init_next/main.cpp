#include <iostream>
#include <string>
#include  <vector>

using namespace std;

int main()
{
    string szoveg;
    cout<<"szoveg:";
    cin>>szoveg;
    string pattern;
    cout<<"pattern:";
    cin>>pattern;

    cout<<"next felto^lte'se"<<endl;
    cout<<"i j next"<<endl;
    // cout << pattern << endl;
    int phossz=pattern.length();
    int shossz=szoveg.length();
    int next[phossz];
    int i=0;
    int j=1;
    next[0]=0;
    while(j<phossz)
    {
    cout<<i<<" "<<j<<" "<<next[j-1]<<endl;
        if(pattern[i]==pattern[j])
        {
            i++;
            j++;
            next[j-1]=i;

        }
        else
        {
            if(i==0)
            {
                j++;
                next[j-1]=0;

            }
            else i=next[i-1];
        }

    }
    cout<<i<<" "<<j<<" "<<next[j-1]<<endl;
    cout<<"next:"<<endl;
    for(int a=0;a<phossz;++a)
    {
        cout<<next[a];
    }
    cout<<endl;

    cout<<"KMP algoritmus lefuta'sa:"<<endl;
    int n=0;
    int m=0;
    vector<int> S;
    int compared=0;
    while(n<szoveg.length())
    {
        compared++;
        if(pattern[m]==szoveg[n])
        {
            n++;
            m++;
             if(m==phossz)
            {
                S.push_back(n-m);
                cout<<"S:"<<n-m<<endl;
                m=next[m-1];
            }
        }
        else if(m==0)
        {
            n++;
        }
        else
        {
             m=next[m-1];
        }

    }

    cout<<"hasonlitasok szama: "<<compared<<endl;

    return 0;
}
