#include <stdlib.h>
#include <stdio.h>
#include <signal.h>
#include <unistd.h>
   
  void sigCatch(int signum) {
    switch (signum) {
      case SIGUSR1: puts("Caught SIGUSR1");
                    break;
      case SIGUSR2: puts("Caught SIGUSR2");
                    break;
      default:      printf("sigCatch caught unexpected signal %d\n",
                           signum);
    }
 
  }
 
 
  main() {
    sigset_t sigset;
    struct sigaction sigAct;
	printf("Daniel Matheson\n");
	printf("CS 3060\n");
	printf("project-7a\n");  
    if (fork() == 0) {
      sleep(3);
      printf("Child: Running, parent is:\n");
      puts("child is sending SIGUSR2 signal\n ");
      kill(getppid(), SIGUSR2);
      sleep(3);
      puts("child is sending SIGUSR1 signal\n ");
      kill(getppid(), SIGUSR1);
      exit(0);
    }
 
    sigemptyset(&sigAct.sa_mask);
    sigAct.sa_flags = 0;
    sigAct.sa_handler = sigCatch;
    if (sigaction(SIGUSR1, &sigAct, NULL) != 0)
      perror("1st sigaction() error");
 
 
    else if (sigaction(SIGUSR2, &sigAct, NULL) != 0)
      perror("2nd sigaction() error");
 
    else {
      sigfillset(&sigset);
      sigdelset(&sigset, SIGUSR1);
      printf("parent is waiting for child to send SIGUSR1\n");
      if (sigsuspend(&sigset) == -1)
        perror("sigsuspend() returned -1\n");
      printf("sigsuspend ended");
    }

}

