# include <stdio.h>
# include <sys/stat.h>
# include <dirent.h>
# include <sys/types.h>
# include <string.h>


void outSize(char* dName);
bool isDir(const char* target);

int main(int argc, char* argv[]){
	struct stat st_buf;
	outSize(argv[1]);
	}


void outSize(char* dName){
	 struct stat statbuf;
         stat(dName, &statbuf);
	DIR *dir_ptr;
	struct dirent *direntp;

	if(isDir(dName))
	{
		printf ("%s is a directory.\n", dName);
		printf ("%s has %ld bytes.\n", dName, statbuf.st_size);
		if((dir_ptr = opendir( dName)) == NULL)
			fprintf(stderr, "Can not open %s\n", dName);
		else
		{	
			while((direntp = readdir( dir_ptr)) != NULL && strcmp(dName, ".") != 0 && strcmp(dName, "..") != 0)
				outSize(direntp->d_name);
				closedir(dir_ptr);
		}
	}
	else
	{
	printf ("%s is a regular file.\n", dName);
	printf ("%s has %ld bytes.\n", dName, statbuf.st_size);
	}
}

bool isDir(const char* target)
{
   struct stat statbuf;
   stat(target, &statbuf);
   return S_ISDIR(statbuf.st_mode);
}
