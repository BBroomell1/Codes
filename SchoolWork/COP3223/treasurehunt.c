//Brandon Broomell
//COP 3223C
//DATE: 12/3/2017
//treasurehunt.c

#include <stdio.h>
#include <stdlib.h>

#define ROW 3
#define COL 3
#define CREW 4

typedef struct pirate {
	int dig;
	int carry;
}pirate;

typedef struct map {
  int sand;
	int treasure;
}map;

//Arrays of structures
map maps[3][3];
pirate pirates[CREW];

void printCrew(struct pirate pirates[CREW]);
void printMap(struct map maps[ROW][COL]);
int main()
{
int i, j, hours = 8, tempRow, tempCol, foundTreasure = 0, totalTreasure = 0, unfoundTreasure = 0;
char *filename;
FILE *ifp;

//MALLOC FILENAME
	filename = malloc(sizeof(char)*25);
	if(filename == NULL)
	{
		fprintf(stderr, "Failed to malloc filename\n");
		return 1;
	}

		//Get filename from user
printf("You have arrived at Treasure Island!\n");
printf("Enter name of filename to open.: \n");
scanf("%s", filename);
		//Open file and check if valid if not print to stderr.
ifp = fopen(filename, "r");
if(ifp == NULL)
{
	fprintf(stderr, "Failed to open %s.\n", filename);
	return 1;
}

			//Read in each grid squares sand and treasure amount
for(i = 0; i < ROW; i ++)
{
	for(j = 0; j < COL; j++)
	{
		fscanf(ifp, "%d", &maps[i][j].sand);
		fscanf(ifp, "%d", &maps[i][j].treasure);
		totalTreasure += maps[i][j].treasure;
	}
}
			//Read in each crew members amount they can dig and carry
for(i = 0; i < CREW; i++)
{
		fscanf(ifp, "%d", &pirates[i].dig);
		fscanf(ifp, "%d", &pirates[i].carry);
}


				//While hours is greater than 0
do
{
	printf("You have %d hours left to dig up treasure!\n", hours);
	printCrew(pirates);
	for(i = 0; i < CREW; i++)
	{
		printMap(maps);

				//Get the row and column from the user.
				//(Subtract 1 from the Row and Column to account for starting at 0)
		printf("Where would you like to send crewmember %d?\n", i+1);
		printf("ROW?: \n");
		scanf("%d", &tempRow);
		tempRow -= 1;
		printf("COLUMN?: \n");
		scanf("%d", &tempCol);
		tempCol -= 1;

					//Check if sand in grid location is still greater than 0, if yes then subtract the amount that crew member can
					//Dig in an hour, then check again if 0 and print out statements.
					//Do the same thing for the treasure in grid location.
					//If all the treasure has been taken out of grid location set the treasure to 0 to remove negative numbers.
		if(maps[tempRow][tempCol].sand > 0)
			{
				maps[tempRow][tempCol].sand -= pirates[i].dig;
				if(maps[tempRow][tempCol].sand <= 0)
				printf("You have removed all the sand from this area.\n");
				else
				printf("You have removed some sand from this area.\n");
			}

		else if(maps[tempRow][tempCol].treasure > 0)
			{
				maps[tempRow][tempCol].treasure -= pirates[i].carry;
				if(maps[tempRow][tempCol].treasure <= 0)
				{
					printf("You have taken all the treasure from this area.\n");
					maps[tempRow][tempCol].treasure = 0;
				}
				else
				printf("You have taken some treasure back from this area.\n");
			}

		else
			{
				printf("This area has already been cleared.\n");
			}

					//Loop though and add each number of unfound treasure to variable unfoundTreasure
					//If unfoundTreasure is < or == 0 set the hours to 0 to exit loop and print statement
					unfoundTreasure = 0;
					int h, k;
			for(h = 0; h < ROW; h++)
			{
				for(k = 0; k < COL; k++)
				{
					if(maps[h][k].treasure >= 0)
					{
						unfoundTreasure += maps[h][k].treasure;
					}
				}
			}
					//If hours == 0 and all treasure has not been foundTreasure
					//Print out amount of treasure that wa found and end program
			if(unfoundTreasure <= 0)
			{
				hours = 0;
				printf("\nYou Have Found All The Treasure and Taken It Back To The Ship!!!!!!\n");
				return 0;
			}
	}
hours--;

if(hours <= 0)
{
	foundTreasure = totalTreasure - unfoundTreasure;
	printf("You are forced to evacuate the island.\n");
	printf("You have found %d pieces of the pirates treasure!\n", foundTreasure);
	return 0;
}
}while(hours > 0);



}//END MAIN

		//Print out the crew abilities
void printCrew(struct pirate pirates[CREW])
{
	int i;
	printf("Crew\tDig\tCarry\n");
	for(i = 0; i < CREW; i++)
	{
		printf("%d\t%d\t%d\n", i + 1, pirates[i].dig, pirates[i].carry);
	}
}


			//Print the map function
			//If sand is > 0 print the amount of sand
			//else if sand is not > 0 and treasure is > 0 print the amount of Treasure
			//else print the "-" symbol to signify area has been cleared
void printMap(struct map maps[ROW][COL])
{
	printf("=======MAP=======\n");
	int i, j;
	for(i = 0; i < ROW; i++)
	{
		for(j = 0; j < COL; j++)
		{
			if(maps[i][j].sand > 0)
			{
				printf("%dS\t", maps[i][j].sand);
			}
			else if(maps[i][j].treasure > 0)
			{
				printf("%dT\t", maps[i][j].treasure);
			}
			else
			{
				printf("-\t");
			}

		}
		printf("\n");
	}
}
