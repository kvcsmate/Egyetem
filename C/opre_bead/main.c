#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <unistd.h> 
#include <sys/types.h> 
#include <string.h> 
#include <sys/wait.h>
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
int teruletszm=5;
char teruletek[5][100]={"Jenő telek","Lovas dűlő","Hosszú","Selyem telek","Malom telek és Szula"};
int munkaszm=4;
char munkak[4][100]={"metszés","rügyfakasztó permetezés","tavaszi nyitás","horolás"};
int munkasmin=2;
char hetilimit[7]={0,0,0,0,0,0,0};


void fillhet()
{
	for(int i=0;i<7;++i)
	{
		hetilimit[i]=0;
	}
	FILE *data;
	data=fopen("workers.txt","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file, hét kapacitás feltöltése sikertelen");
	}
	else
	{
		struct munkas temp;
	while(fread(&temp,sizeof(struct munkas),1,data))
	{
		
		for(int i=0;i<7;++i)
		{
			hetilimit[i]+=temp.napok[i];
		}
	}
	}
	
}

void savetofile(struct munkas *worker)
{

	FILE *data;
	data= fopen("workers.txt","wb");
	if(data==NULL)
	{

		printf("a file nem megnyitható");
	}
	fwrite(&worker[0],sizeof(struct munkas),1,data);
	fwrite(&worker[1],sizeof(struct munkas),1,data);
	if(fwrite==0)
	{
	printf("hiba történt mentés közben!");
	}
	printf("mentés lefutott");
	fclose(data);


}
void add()
{
	
	struct munkas worker;
	FILE *data;
	data=fopen("workers.txt","ab");
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
	data=fopen("workers.txt","rb");
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
		fclose(data);
		int choosekey=-1;
		while(choosekey==-1)
		{
			int temp=-1;
			scanf("%i",&temp);
			while((getchar())!='\n');
			if(temp==-1||temp>i)
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
			switch(menukey)
			{
				
				case '1':
				printf("\núj név:\n");
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
				strcpy(tempworker.nev,nev);
				break;
				case '2':;printf("\núj cím:\n");
				char buffer[100];
				scanf("%100[a-zA-ZöÖüÜóÓúÚőŐáÁíÍéÉűŰ0-9.,-' ]s",buffer);
				strcpy(tempworker.cim,buffer);
				while((getchar())!='\n');
				printf("cím megváltoztatva\n");
				break;
				case '3':
				printf("még nem teli napok:");
					
					for(int j=0;i<7;j++)
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
						if(strstr(napok,het[i])!=NULL && hetilimit[j]<5)
						{
							
							tempworker.napok[j]=1;
						}else tempworker.napok[j]=0;
						
						
					}
				printf("\n heti beosztás megváltoztatva.\n");
				break;
				default:printf("\n nem megfelelő menükulcs!\n");menukey=0;break;	
	
			}	
		data=fopen("workers.txt","wb");
		for(int j=0;j<i;j++)
		{
			
			if(j==choosekey)
			{
				fwrite(&tempworker,sizeof(struct munkas),1,data);
			} else fwrite(&worker[j],sizeof(struct munkas),1,data);
		}
		fclose(data);
		}
	}
};
void delet()
{
	printf("munkások (törléshez írd be a törölni kívánt munkás sorszámát):\n");
	struct munkas worker[max_workers];
	FILE *data;
	data=fopen("workers.txt","rb");
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
		data=fopen("workers.txt","wb");
		for(int j=0;j<i-1;j++)
		{
			fwrite(&worker[j],sizeof(struct munkas),1,data);
		}
		fclose(data);
		
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
	//printf("\n%s:",het[choosekey-1]);
	FILE *data;
	data=fopen("workers.txt","rb");
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
	
	for(int i=0;i<7;++i)
	{
	printf("\n%s:",het[i]);
	FILE *data;
	data=fopen("workers.txt","rb");
	if(data==NULL)
	{
		printf("hiba olvasás közben/üres data file");
	}
	else
	{
		struct munkas temp;
		int j=0;
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
	data=fopen("workers.txt","rb");
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
			//printf("\n");
		}
	}
	fclose(data);
};


int bead2()
{
	
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
		//printf("%s ",napi_f);
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
			//nap választás
			srand(time(0));
			int mainap=rand()%7;
			printf("%s\n",het[mainap]);
			int munkas_sz=0;
			FILE *data;
			data=fopen("workers.txt","rb");
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
						strcat(napi_f,delim);
						strcat(napi_f,temp.nev);
						munkas_sz++;
					}
				}
			}
			fclose(data);
			//printf("%s",msg);
			srand(time(0));
			write(mjp[1],&munkas_sz,sizeof(int));
			write(mjp[1],napi_f,1000);
			close(mjp[1]);
		 }
		 else // gyerek:munkásjárat
		 {
			 close(mjp[1]);
			 int munkas_sz=0;

			 char lista[1000]="";
			 read(mjp[0],&munkas_sz,sizeof(int));
			 printf("%i \n",munkas_sz);
			 read(mjp[0],lista,1000);
			 printf("%s \n",lista);
			 close(mjp[0]);
			 exit(0);
		 }

		 
	 }
	 else //gyerek:gazdatiszt
	 {
		 close(pp[0]);
		 //time_t t;
		 //srand((unsigned) time(&t));
		 
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
	
	char menukey;
	do
	{
		fillhet();
	
		printf("\n1.munkás hozzáadása \n2.munkás módosítása \n3.munkás törlése \n4.napi lista készítése \n5.teljes lista készítése\n6.dolgozók kiírása\n7.2.beadandó indítása\n0.kilépés\n");
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
			case '7':bead2();break;
			case '0':printf("\n exit program.. \n");break;
			default:printf("\n nem megfelelő menükulcs!\n");break;	
	
		}
	}while(menukey!='0');
	return 0;
};


