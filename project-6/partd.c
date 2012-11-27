#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>


int count;
void* incrementCounter(void* m);
pthread_mutex_t lock = PTHREAD_MUTEX_INITIALIZER;
main()
{
        count = 0;
        pthread_t t1, t2, t3, t4;
        pthread_create(&t1, NULL, incrementCounter, (void*)&count);
        pthread_create(&t2, NULL, incrementCounter, (void*) &count);
        pthread_create(&t3, NULL, incrementCounter, (void*) &count);
        pthread_create(&t4, NULL, incrementCounter, (void*) &count);
        pthread_join(t1, NULL);
        pthread_join(t2, NULL);
        pthread_join(t3, NULL);
        pthread_join(t4, NULL);
        printf("count is %d", count);
}
void* incrementCounter( void* m )
 {
         int i;
         for (i = 0; i < 10; ++i)
         {
		pthread_mutex_lock(&lock);
                 int tempValue = count;
                 sleep(1);
                 tempValue = tempValue + 1;
                 count = tempValue;
		pthread_mutex_unlock(&lock);
         }
         return NULL;
 }

