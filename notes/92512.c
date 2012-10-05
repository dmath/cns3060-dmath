#include <stdio.h>
#include <dirent.h>
#include <sys/types.h>
#include <sys.stats.h>

#define success 0
#define failure 1

int printDirectory(const char* directory);

int main(int argc, char* argv[]){

	cont char* directory = cargc > 1 ? argv[1] : ",";
	
	DIR* directoryPointer = openDir(directory);
	if (directory == NULL){
		perror("Cant open dir");
		return failure;
	}

	struct direct* currentEntry;
	while ((currentEntry = readdir(directoryPointer)) != NULL){
		printf("Directory: '%-25s" Entry: '%s\n' directory currentyEntry->d_name);	

		printf("\tSize: %lld\n", (long long) entryStats.st_size);
		}			
		
	return success;
}
