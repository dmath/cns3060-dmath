using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedSim
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Data> myList = new List<Data>();
            Console.WriteLine("Enter Data, when done enter a blank line");
            bool done = false;
            while (!done)
            {
                string temp = Console.ReadLine();
                if (temp == "")
                    break;
                string[] split = temp.Split();
                Data tempData = new Data(double.Parse(split[0]), double.Parse(split[1]), double.Parse(split[2]));
                myList.Add(tempData);
            }
            //for testing
            //Data tempData = new Data(1, 0, 6);
            //myList.Add(tempData);
            //tempData = new Data(2, 3, 2);
            //myList.Add(tempData);
            //tempData = new Data(3, 5, 1);
            //myList.Add(tempData);
            //tempData = new Data(4, 9, 7);
            //myList.Add(tempData);
            //tempData = new Data(5, 10, 5);
            //myList.Add(tempData);
            //tempData = new Data(6, 12, 3);
            //myList.Add(tempData);
            //tempData = new Data(7, 14, 4);
            //myList.Add(tempData);
            //tempData = new Data(8, 16, 5);
            //myList.Add(tempData);
            //tempData = new Data(9, 17, 7);
            //myList.Add(tempData);
            //tempData = new Data(10, 19, 2);
            //myList.Add(tempData);
            Console.WriteLine("Doing fcfs:");
            fcfs(myList);
            Console.WriteLine("Doing sstf:");
            sstf(myList);
            Console.WriteLine("Doing sstn:");
            sstn(myList);
            Console.WriteLine("Doing Round Robin");
            rr(myList);
            Console.WriteLine("Doing Round Robin with Context Switches");
            rrWc(myList);

              Console.ReadLine();
        }
        static void fcfs(List<Data> dList)
        {
            double cTime = 0;
            double count = 0;
            double tWait = 0;
            double tTurn = 0;
            List<Data> fcfsList = new List<Data>();
            foreach (Data d in dList)
            {
                fcfsList.Add(d);
                count++;
            }
            Console.WriteLine("PID:\t Wait \t TurnAround");
            for (int i = 0; i < count; i++)
            {
                Data curr = fcfsList[i];
                while(cTime < curr.aTime)
                {
                    cTime++;
                }
                curr.wait = cTime - curr.aTime;
                cTime += curr.ttc;
                tWait += curr.wait;
                curr.tAround = curr.wait + curr.ttc;
                tTurn += curr.tAround;
                Console.WriteLine("{0} \t {1} \t {2}", curr.pid, curr.wait, curr.tAround);
            }
            Console.WriteLine("Average Wait: {0} Average Turnaround: {1}",tWait/count,tTurn/count);
        }
        static void sstf(List<Data> dList)
        {
            List<Data> dispalyList = new List<Data>();
            double cTime = 0;
            double count = 0;
            double tWait = 0;
            double tTurn = 0;
            Data curr = new Data(0, 0, 0) ;
            List<Data> sstfList = new List<Data>();
            foreach (Data d in dList)
            {
                Data temp = new Data(d.pid, d.aTime, d.ttc);
                sstfList.Add(temp);
                count++;
            }
            Console.WriteLine("PID:\t Wait \t TurnAround");
            for (int i = 0; i < count; i++)
            {
                double cTTC = double.MaxValue;
                foreach (Data d in sstfList)
                {
                    if (d.aTime <= cTime && d.done == false && d.ttc < cTTC)
                    {
                        curr = d;
                        cTTC = curr.ttc;
                    }
                }
                curr.done = true;
                curr.wait = cTime - curr.aTime;
                cTime += curr.ttc;
                tWait += curr.wait;
                curr.tAround = curr.wait + curr.ttc;
                tTurn += curr.tAround;
                dispalyList.Add(curr);
                //Console.WriteLine("{0} \t {1} \t {2}", curr.pid, curr.wait, curr.tAround);
            }
            dispalyList.Sort((x1,x2) => x1.pid.CompareTo(x2.pid));
            //List<Data> gList = dispalyList.OrderBy(x => x.pid);
            foreach (Data d in dispalyList)
            {
                Console.WriteLine("{0} \t {1} \t {2}", d.pid, d.wait, d.tAround);
            }
            Console.WriteLine("Average Wait: {0} Average Turnaround: {1}", tWait / count, tTurn / count);
        }
        static void sstn(List<Data> dList)
        {
            
            double cTime = 0;
            double count = 0;
            double tWait = 0;
            double tTurn = 0;
            bool allDone = false;
            List<Data> sstnList = new List<Data>();
            Data curr = new Data(0, 0, 0);
            foreach (Data d in dList)
            {
                Data temp = new Data(d.pid, d.aTime, d.ttc);
                sstnList.Add(temp);
                count++;
                //d.done = false;
            }
            while (!allDone)
            {
                double cTTC = double.MaxValue;
                allDone = true;
                foreach (Data d in sstnList)
                {
                    if (d.ttc > 0)
                    {
                        allDone = false;
                        if (d.aTime <= cTime && d.ttc < cTTC)
                        {
                            //Console.WriteLine("allDone = false;");
                            curr = d;
                            cTTC = curr.ttc;
                        }
                    }
                }
                if (curr.done == false)
                {
                    curr.wait = cTime - curr.aTime;
                    tWait += curr.wait;
                    curr.done = true;
                    curr.sTime = cTime;
                }
                curr.ttc--;
                if (curr.ttc == 0)
                {
                    curr.fTime = cTime;
                }
                cTime++;
            }
            foreach (Data d in sstnList)
            {
                d.tAround = (d.fTime - d.sTime) + d.wait + 1;
                tTurn += d.tAround;
            }
            sstnList.Sort((x1, x2) => x1.pid.CompareTo(x2.pid));
            //List<Data> gList = dispalyList.OrderBy(x => x.pid);
            foreach (Data d in sstnList)
            {
                Console.WriteLine("{0} \t {1} \t {2}", d.pid, d.wait, d.tAround);
            }
            Console.WriteLine("Average Wait: {0} Average Turnaround: {1}", tWait / count, tTurn / count);

        }
        static void rr(List<Data> dList)
        {
            double cTime = 0;
            double count = 0;
            double tWait = 0;
            double tTurn = 0;
            bool allDone = false;
            List<Data> rrList = new List<Data>();
            Data curr = new Data(0, 0, 0);
            foreach (Data d in dList)
            {
                Data temp = new Data(d.pid, d.aTime, d.ttc);
                rrList.Add(temp);
                count++;
            }
            while (!allDone)
            {
                allDone = true;
                for (int i = 0; i < count; i++)
                {
                    if (rrList[i].aTime <= cTime && rrList[i].ttc > 0)
                    {
                        curr = rrList[i];
                        allDone = false;
                        if (curr.done == false)
                        {
                            curr.wait = cTime - curr.aTime;
                            tWait += curr.wait;
                            curr.done = true;
                            curr.sTime = cTime;
                        }
                        if (curr.ttc == 4)
                        {
                            cTime += 4;
                            curr.ttc -= 4;
                            curr.fTime = cTime;
                        }
                        else if (curr.ttc < 4)
                        {
                            cTime += curr.ttc;
                            curr.ttc = 0;
                            curr.fTime = cTime;
                        }
                        else
                        {
                            cTime += 4;
                            curr.ttc -= 4;
                        }
                    }
                    
                }
                
            }
            foreach (Data d in rrList)
            {
                d.tAround = (d.fTime - d.sTime) - 1;
                tTurn += d.tAround;
            }
            foreach (Data d in rrList)
            {
                Console.WriteLine("{0} \t {1} \t {2}", d.pid, d.wait, d.tAround);
            }
            Console.WriteLine("Average Wait: {0} Average Turnaround: {1}", tWait / count, tTurn / count);

        }
        static void rrWc(List<Data> dList)
        {
            double cTime = 0;
            double count = 0;
            double tWait = 0;
            double tTurn = 0;
            bool allDone = false;
            List<Data> rrList = new List<Data>();
            Data curr = new Data(0, 0, 0);
            foreach (Data d in dList)
            {
                Data temp = new Data(d.pid, d.aTime, d.ttc);
                rrList.Add(temp);
                count++;
            }
            while (!allDone)
            {
                allDone = true;
                for (int i = 0; i < count; i++)
                {
                    if (rrList[i].aTime <= cTime && rrList[i].ttc > 0)
                    {
                        curr = rrList[i];
                        allDone = false;
                        if (curr.done == false)
                        {
                            curr.wait = cTime - curr.aTime;
                            tWait += curr.wait;
                            curr.done = true;
                            curr.sTime = cTime;
                        }
                        if (curr.ttc == 4)
                        {
                            cTime += 4;
                            curr.ttc -= 4;
                            curr.fTime = cTime;
                            cTime += .4;
                        }
                        else if (curr.ttc < 4)
                        {
                            cTime += curr.ttc;
                            curr.ttc = 0;
                            curr.fTime = cTime;
                            cTime += .4;
                        }
                        else
                        {
                            cTime += 4;
                            curr.ttc -= 4;
                            cTime += .4;
                        }
                    }

                }

            }
            foreach (Data d in rrList)
            {
                d.tAround = d.fTime - d.sTime;
                tTurn += d.tAround;
            }
            foreach (Data d in rrList)
            {
                Console.WriteLine("{0} \t {1} \t {2}", d.pid, d.wait, d.tAround);
            }
            Console.WriteLine("Average Wait: {0} Average Turnaround: {1}", tWait / count, tTurn / count);

        }
    }
    

     class Data
    {
        public double pid;
        public double aTime;
        public double ttc;
        public double wait;
        public double tAround;
        public bool done;
        public double sTime;
        public double fTime;
         public Data(double pid, double aTime, double ttc)
         {
             this.pid = pid;
             this.aTime = aTime;
             this.ttc = ttc;
             wait = 0;
             tAround = 0;
             done = false;
             sTime = 0;
             fTime = 0;
         }
    }
}

