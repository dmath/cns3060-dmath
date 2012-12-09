#include <stdio.h>
#include <termios.h>
#include <stdlib.h>

#define LINELEN 23
int main(int argc, char* argv[])
{
	FILE * pfile = NULL;
	int line[LINELEN];
	int i, count = 0;
	
	if ( argc > 1 ) {
		pfile = fopen(argv[1], "r"); 

		if ( pfile == NULL ) { 
       		fprintf(stderr, "%s: ", argv[1]); 
      	 	perror("Unable to open"); 
      		exit(1); 
	     } 
	
	}

}
