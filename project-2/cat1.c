#include<stdio.h> 
#include<stdlib.h> 
 
#define MAX_LINE_LEN 512

void usage_exit();  
void open_and_dump_file(const char *filepath, int show_lines);
void dump_file(FILE *pfile, int show_lines);
 
int main(int argc, char *argv[]) {
    int show_lines = 0;
 
    /* Check for options */ 
    if ( argc > 1 && strncmp(argv[1], "-", 1) == 0 ) { 
        /* -n : The line number option */
        if ( strcmp(argv[1], "-n") == 0 ) { 
            show_lines = 1; 
            argc--; 
            argv++; 
        } else { 
            fprintf(stderr, "Invalid argument '%s'\n", argv[1]); 
            usage_exit(); 
        } 
    } 
 
    /* No files, then display stdin */ 
    if ( argc <= 1 )
        dump_file(stdin, show_lines); 
    else 
        for ( ; argc > 1; argc--, argv++ ) 
            open_and_dump_file(argv[1], show_lines);
} 

void usage_exit() { 
    fprintf(stderr, "cat1 [-n] [files...]\n"); 
    fprintf(stderr, "Options:\n"); 
    fprintf(stderr, " -n : Number lines of output.\n"); 
    fprintf(stderr, " files... : Zero or more files to concatenate.\n"); 
    fprintf(stderr, " If no files are given, use Standard Input.\n"); 
 
    exit(1); 
  
} 

void open_and_dump_file(const char *filepath, int show_lines) {
    FILE * pfile = fopen(filepath, "r");
 
    if ( pfile == NULL ) {
        fprintf(stderr, "%s: ", filepath); 
        perror("File open error"); 
        exit(2); 
    } 
 
    dump_file(pfile, show_lines); 
} 
 

void dump_file(FILE *pfile, int show_lines) {
    int linecount = 0;
    char txtline[MAX_LINE_LEN]; 
 
    while( fgets(txtline, MAX_LINE_LEN, pfile) != NULL ) { 
        if ( show_lines > 0 ) {
            printf("%d : ", ++linecount); 
        } 
        fputs(txtline, stdout); 
    } 
} 
