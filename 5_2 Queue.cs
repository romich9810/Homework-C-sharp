using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_2_Queue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minSizeOfQueue = 1;
            int maxSizeOfQueue = 10;
            int minValueOfSumClient = 100;
            int maxValueOfSumClient = 100000;
            int sizeOfQueue = random.Next(minSizeOfQueue, maxSizeOfQueue + 1);

            int totalProfit = 0;

            int conditionOfFinishSum = 0;

            Queue<int> totalSumsOfServiceClients = new Queue<int>();

            for (int i = 0; i < sizeOfQueue; i++)
            {
                totalSumsOfServiceClients.Enqueue(random.Next(minValueOfSumClient, maxValueOfSumClient + 1));
            }

            ServeClient(totalSumsOfServiceClients, totalProfit, conditionOfFinishSum);          
        }

        static void ServeClient (Queue<int> totalSumsOfServiceClients,int totalProfit, int conditionOfFinishSum)
        {
            int lengthOFQueue = totalSumsOfServiceClients.Count();
            int numberOfClient = 0;
            int sumBeforeServising;

            while (totalSumsOfServiceClients.Count() != conditionOfFinishSum)
            {
                numberOfClient++;
                lengthOFQueue--;
                Console.WriteLine($"{numberOfClient}-й клиент. В очереди ещё {lengthOFQueue} клиентов\n\n");

                sumBeforeServising = totalProfit;
                totalProfit += totalSumsOfServiceClients.Dequeue();
                Console.WriteLine($"Сумма до обслуживания {numberOfClient}-го клиента: {sumBeforeServising}\nПолученная сумма с клиентов: {totalProfit}");

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
