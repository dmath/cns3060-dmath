using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dsksched
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number you want to add to the queue, then hit enter.  When you are done enter a negative number");
            List<int> sched = new List<int>();
            int next = 0;
            sched.Add(15);
            while (next >= 0)
            {
                int.TryParse(Console.ReadLine(), out next);
                sched.Add(next);
            }
            //hardcoded numbers for testing:
            //sched.Add(4);
            //sched.Add(40);
            //sched.Add(11);
            //sched.Add(35);
            //sched.Add(7);
            //sched.Add(14);
            //sched.Add(-1);

            Console.WriteLine("Doing fcfs:");
            fcfs(sched);
            Console.ReadKey();
            Console.WriteLine("Doing sstf:");
            sstf(sched);
            Console.ReadKey();
            Console.WriteLine("Doing elevator:");
            elev(sched);
            Console.ReadKey();

            
    
                
        }
        public static void fcfs(List<int> l)
        {
            int i = 0;
            int cur;
            int next;
            int dif;
            int total = 0;
            while (l[i + 1] >= 0)
            {
                cur = l[i];
                next = l[i + 1];
                dif = Math.Abs(cur - next);
                total += dif;
                Console.WriteLine("Going from " + cur + " to " + next + " with a difference of " + dif + " for a total of " + total);
                i++;

            }
        }

        public static void sstf(List<int> l)
        {
            List<int> newL = new List<int>();
            foreach (int i in l)
                newL.Add(i);
            int cur = newL[0];
            int curDif;
            int next = 0;
            int dif = int.MaxValue;
            int total = 0;
            newL.RemoveAt(0);
            while (newL.Count > 1)
            {
                foreach (int i in newL)
                {
                    if (i >= 0)
                    {
                        curDif= Math.Abs(cur - i);
                        if (curDif < dif)
                        {
                            next = i;
                            dif = curDif;
                        }
                    }
                    
                }
                total += dif;
                Console.WriteLine("Going from " + cur + " to " + next + " with a difference of " + dif + " for a total of " + total);
                newL.Remove(cur);
                cur = next;
                newL.Remove(cur);
                next = newL[0];
                dif = Math.Abs(cur - next);
            }

        }

        public static void elev(List<int> l)
        {
            int start = l[0];
            
            List<int> lower = new List<int>();
            List<int> higher = new List<int>();
            foreach (int i in l)
            {
                if (i <= start)
                {
                    lower.Add(i);
                }
                else
                    higher.Add(i);
            }
            lower.Sort();
            lower.Reverse();
            higher.Sort();
            var allProducts = lower.Concat(higher).ToList();
            for(int i = 0;i<allProducts.Count();i++)
            {
                if (allProducts[i] < 0)
                    allProducts.RemoveAt(i);
            }
            allProducts.Add(-1);
            sstf(allProducts);
        }

    }
        
}

