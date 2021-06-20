#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <unistd.h> 
#include <sys/types.h> 
#include <string.h> 
#include <wait.h>
#include<signal.h> 
#include<time.h> 
struct munkas
{
int napok[7];
char nev[100];
char cim[100];
};

int max_workers=10;
int napilimit=5;
char het[7][20]={"hétfő","kedd","szerda","csütörtök","péntek","szombat","vasárnap"};
int hetilimit[7]={0,0,0,0,0,0,0};
int teruletszm=5;
char teruletek[5][100]={"Jenő telek","Lovas dűlő","Hosszú","Selyem telek","Malom telek és Szula"};
int munkaszm=4;
char munkak[4][100]={"metszés","rügyfakasztó permetezés","tavaszi nyitás","horolás"};
int munkasmin=1;


void fillhet()
{
	FILE *data;
	struct munkas temp;
	for(int i=0;i<7;++i)
	{
		hetilimit[i]=0;
	}
	
	data=fopen("workers.data","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file, hét kapacitás feltöltése sikertelen");
	}
	else
	{
		
	while(fread(&temp,sizeof(struct munkas),1,data))
	{
		
		for(int i=0;i<7;++i)
		{
			hetilimit[i]+=temp.napok[i];
		}
	}
	}
	
}

void add()
{
	
	struct munkas worker;
	FILE *data;
	data=fopen("workers.data","ab");
	printf("munkás neve: \n");
	char nev[100];
	int helyesbemenet=1;
	while(helyesbemenet==1)
	{
		scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",nev);	
		while((getchar())!='\n');
		helyesbemenet=0;
		
		
		for(int i=0;nev[i]!='\0';++i)
		{
			if(isdigit(nev[i]))
			{
				helyesbemenet=1;
			}
		}
		if(helyesbemenet==1)
		{
			printf("\nnév nem tartalmazhat számot!\n");
		}
	}
	
	
	
	
	char buffer[100];
	printf("munkás lakcíme:\n");
	scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",buffer);
	while((getchar())!='\n');
	strcpy(worker.nev,nev);
	strcpy(worker.cim,buffer);
	char napok[100];
	printf("még nem teli napok:");
	for(int i=0;i<7;i++)
	{
		if(hetilimit[i]<5)
		{
			printf("%s ",het[i]);
		}
		
	}
	printf("\n");
	printf("napok amiken dolgozik:\n");
	scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",napok);
	while((getchar())!='\n');
	for(int i=0;i<7;++i)
	{
		if(strstr(napok,het[i])!=NULL && hetilimit[i]<5)
		{
			worker.napok[i]=1;
			hetilimit[i]++;
		}else worker.napok[i]=0;
		
		
	}
	
	fwrite(&worker,sizeof(struct munkas),1,data);
	printf("munkás hozzáadva.\n");
	
	fclose(data);
};
void edit()
{
	printf("munkások (módosításhoz írd be a módosítani kívánt munkás sorszámát):\n");
	struct munkas worker[max_workers];
	FILE *data;
	data=fopen("workers.data","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file,módosítás sikertelen\n");
	}
	else
	{
		int i=0;
		while(fread(&worker[i],sizeof(struct munkas),1,data))
		{
				printf("%i.%s,%s\n",i,worker[i].nev,worker[i].cim);
				i++;
		}
		if(i==0)
		{
			printf("\nnincs mit módosítani.\n");
		}
		else
		{
				fclose(data);
			int choosekey=-1;
			while(choosekey==-1)
			{
				int temp=-1;
				scanf("%i",&temp);
				while((getchar())!='\n');
				if(temp==-1||temp>i-1)
				{
					temp=-1;
					printf("nem megfelelő sorszám!\n");
					
				}else choosekey=temp;
			}
			struct munkas tempworker;
			strcpy(tempworker.nev,worker[choosekey].nev);
			strcpy(tempworker.cim,worker[choosekey].cim);
			for(int j=0;j<7;++j)
			{
				tempworker.napok[i]=worker[choosekey].napok[j];
			}
			int menukey=0;
			while(menukey==0)
			{
				printf("\n1.név módosítása\n,2.lakcím módosítása\n,3.dolgozói napok módosítása\n");
				menukey=getchar();
				while((getchar())!='\n');
				if(menukey=='1')
				{
					
					
					printf("\núj név:\n");
					char nev[100];
					int helyesbemenet=1;
					while(helyesbemenet==1)
					{
						scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",nev);	
						while((getchar())!='\n');
						helyesbemenet=0;
						
						
						for(int j=0;nev[j]!='\0';++j)
						{
							if(isdigit(nev[j]))
							{
								helyesbemenet=1;
							}
						}
						if(helyesbemenet==1)
						{
							printf("\nnév nem tartalmazhat számot!\n");
						}
					}
					strcpy(tempworker.nev,nev);
				}
				else if(menukey=='2')
				{
					printf("\núj cím:\n");
					char buffer[100];
					scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",buffer);
					strcpy(tempworker.cim,buffer);
					while((getchar())!='\n');
					printf("cím megváltoztatva\n");
				}
				else if(menukey=='3')
				{
					printf("\nmég nem teli napok:\n");
						
						for(int j=0;j<7;j++)
						{
							if(tempworker.napok[j]==1)
							{
								hetilimit[j]--;
								
							}
							if(hetilimit[j]<5)
							printf("%s ",het[j]);
							
						}
						printf("\n");
						printf("napok amiken dolgozik:\n");
						char napok[100];
						scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",napok);
						while((getchar())!='\n');
						for(int j=0;j<7;++j)
						{
							if(strstr(napok,het[j])!=NULL && hetilimit[j]<5)
							{
								
								tempworker.napok[j]=1;
							}else tempworker.napok[j]=0;
							
							
						}
					printf("\n heti beosztás megváltoztatva.\n");
				}else 
				{
					printf("\n nem megfelelő menükulcs!\n");menukey=0;	
				}
		
			
				data=fopen("workers.data","wb");
				for(int j=0;j<i;j++)
				{
					
					if(j==choosekey)
					{
						fwrite(&tempworker,sizeof(struct munkas),1,data);
					} else fwrite(&worker[j],sizeof(struct munkas),1,data);
				}
				fclose(data);
				fillhet();
			}
		}
		
	}
};
void delet()
{
	printf("munkások (törléshez írd be a törölni kívánt munkás sorszámát):\n");
	struct munkas worker[max_workers];
	FILE *data;
	data=fopen("workers.data","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file, törlés sikertelen.\n");
	}
	else
	{
		int i=0;
		while(fread(&worker[i],sizeof(struct munkas),1,data))
		{
				printf("%i.%s,%s\n",i,worker[i].nev,worker[i].cim);
				i++;
		}
		fclose(data);
		if(i==0)
		{
			printf("\nnincs mit törölni.\n");
		} else 
		{
			int deletekey=-1;
		while(deletekey==-1)
		{
			int temp=-1;
			scanf("%i",&temp);
			while((getchar())!='\n');
			if(temp==-1||temp>i)
			{
				temp=-1;
				printf("nem megfelelő sorszám!\n");
				
			}else deletekey=temp;
		}
		for(int j=deletekey;j<i;j++)
		{
			worker[j]=worker[j+1];
			
		}
		data=fopen("workers.data","wb");
		for(int j=0;j<i-1;j++)
		{
			fwrite(&worker[j],sizeof(struct munkas),1,data);
		}
		fclose(data);
		
		}
		
	}
	
};

void daily_list()
{
		printf("\nválassz napot:");
		printf("\n1.hétfő \n2.kedd\n3.szerda \n4.csütörtök \n5.péntek \n6.szombat \n7.vasárnap\n");
		int choosekey=-1;
		while(choosekey==-1)
		{
			int temp=-1;
			scanf("%i",&temp);
			while((getchar())!='\n');
			if(temp==-1||temp>7)
			{
				temp=-1;
				printf("nem megfelelő sorszám!\n");
				
			}else choosekey=temp;
		}
	FILE *data;
	data=fopen("workers.data","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file");
	}
	else
	{
		struct munkas temp;
		while(fread(&temp,sizeof(struct munkas),1,data))
		{
			if(temp.napok[choosekey-1]==1)
			{
				printf("\n%s,%s",temp.nev,temp.cim);
			}
		}
	}
	fclose(data);
	
};

		

void full_list()
{
	FILE *data;
	int j=0;
	struct munkas temp;
	for(int i=0;i<7;++i)
	{
	printf("\n%s:",het[i]);
	
	data=fopen("workers.data","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file");
	}
	else
	{
		
		j=0;
		while(fread(&temp,sizeof(struct munkas),1,data))
		{
			if(temp.napok[i]==1)
			{
				j++;
				printf("\n%i.%s,%s\n",j,temp.nev,temp.cim);
			}
		}
	}
	fclose(data);
	}
};


void writeout()
{
	FILE *data;
	data=fopen("workers.data","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file");
	}
	else
	{
		struct munkas temp;
		int n=0;
		while(fread(&temp,sizeof(struct munkas),1,data))
		{
			n++;
			printf("\n%i.%s ,%s ",n,temp.nev,temp.cim);
			for(int i=0;i<7;++i)
			{
				
				if(temp.napok[i]==1)
				{
					printf("%s ",het[i]);
				}
			}
			
		}
	}
	fclose(data);
};
int szovegkeres(char *text1, char* text2)
{
    int meret=strlen(text1);
    int meret2=strlen(text2);
    int start=-1;
    int match=0;
    int i=0;
    while(i<meret)
    {
        if(text1[i]==text2[0])
        {
            start=i;
            for(i;i<meret2+start;++i)
            {
                if(text1[i]!=text2[i-start])
                {
                    match=0;
                    start=-1;
                }
            }
        }
        ++i;
    }
  return start;
}
static void handler(int signo)
{
}
int bead2()
{
	sigset_t  set;
	sigemptyset(&set);
	int signum=SIGUSR1;
	sigaddset(&set, signum);
	siginfo_t info;
	signal(signum,handler);
	srand(time(0));
	int mainap=rand()%7;
	
	 const char* delim=", ";
	 int pp[2];
	 pid_t f;
	 if(pipe(pp)==-1)
	 {
		 fprintf(stderr,"Pipe létrehozás nem sikerült!");
		 return 1;
	 }
	 f=fork();
	 if(f<0)
	 {
		 fprintf(stderr,"fork-olás nem sikerült!");
		 return 1;
	 }
	 //szülő
	 else if(f>0)
	 {
		close(pp[1]);
		char napi_f[1000]="";
		read(pp[0],napi_f,1000);
		close(pp[0]);
		
		 // munkásjárat indítása
		int mjp[2];
		if(pipe(mjp)==-1)
		 {
			fprintf(stderr,"Pipe létrehozás nem sikerült!");
			return 1;
		 }
		 f=fork();
		 if(f<0)
		 {
			 fprintf(stderr,"fork-olás nem sikerült!");
			 return 1;
		 }
		 else if(f>0) // szülő
		 {
			close(mjp[0]);
			
			char workers[1000]="";
			printf("%s\n",het[mainap]);
			int munkas_sz=0;
			FILE *data;
			data=fopen("workers.data","rb");
			if(data==NULL)
			{
				printf("hiba olvasás közben/üres data file"); 
			}
			else
			{
				struct munkas temp;
				while(fread(&temp,sizeof(struct munkas),1,data))
				{
					if(temp.napok[mainap]==1)
					{
						strcat(workers,temp.nev);
						strcat(workers," ");
						munkas_sz++;
					}
				}
			}
			fclose(data);
			
			srand(time(0));
			write(mjp[1],&munkas_sz,sizeof(int));
			write(mjp[1],napi_f,1000);
			write(mjp[1],workers,1000);
			close(mjp[1]);
			sigwaitinfo(&set, &info);
			printf("Kiszállított munkások száma: %i\n",info.si_value.sival_int);
		 }
		 else // gyerek:munkásjárat
		 {
			 close(mjp[1]);
			 int munkas_sz=0;
			 char lista[1000]="";
			 read(mjp[0],&munkas_sz,sizeof(int));
			 char locwork[1000]="";
			 read(mjp[0],lista,1000);
			 read(mjp[0],locwork,1000);
			 printf("%s \n %s \n",lista,locwork);
			 if(munkas_sz<munkasmin)
			 {
				printf("nincs elég munkás, várakozás többre..\n");
			 }
				while(munkas_sz<munkasmin)
				{
					FILE *data;
					data=fopen("workers.data","rb");
					if(data==NULL)
					{
						//printf("hiba olvasás közben/üres data file\n");ki lett szedve hogy nem létező file esetén ne játsszon mátrixosat a képernyővel
					}
					else
					{
						struct munkas temp;
						while(fread(&temp,sizeof(struct munkas),1,data))
						{
								if(szovegkeres(lista,temp.nev)==-1&&temp.napok[mainap]==1)
								{
									strcat(locwork,temp.nev);
									strcat(locwork," ");
									munkas_sz++;
									
								}
						}
					}
					fclose(data);
			
				}
					
			 close(mjp[0]);
			 if(munkas_sz>=munkasmin)
			 {
				union sigval value;
				value.sival_int=munkas_sz;
				sigqueue(getppid(),SIGUSR1,value);

			 }
			
			 exit(0);
		 }

		 
	 }
	 else //gyerek:gazdatiszt
	 {
		 srand(time(0));
		 close(pp[0]);
		int munka=rand() % munkaszm;
		int terulet=rand() % teruletszm;
		char msg[1000]="";
		strcpy(msg,teruletek[terulet]);
		strcat(msg,delim);
		strcat(msg,munkak[munka]);
		write(pp[1],msg,1000);
		close(pp[1]);
		exit(0);
	 }

	 return 0;
}


int main()
{
	FILE *data;
	data=fopen("workers.data","ab");
	fclose(data);
	char menukey;
	do
	{
		fillhet();
	
		printf("\n1.munkás hozzáadása \n2.munkás módosítása \n3.munkás törlése \n4.napi lista készítése \n5.teljes lista készítése\n6.dolgozók kiírása\n7.2.beadandó indítása \n0.kilépés\n");
		menukey=getchar();
		while((getchar())!='\n');
		switch(menukey)
		{
			case '1':add();break;
			case '2':edit();break;
			case '3':delet();break;
			case '4':daily_list();break;
			case '5':full_list();break;
			case '6':writeout();break;
			case '0':printf("\n exit program.. \n");break;
			case '7':bead2();break;
			case '\n':break;
			default:printf("\n nem megfelelő menükulcs!\n");break;	
	
		}
	}while(menukey!='0');
	return 0;
};


