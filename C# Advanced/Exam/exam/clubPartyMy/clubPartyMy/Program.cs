using System;
using System.Collections;
using System.Collections.Generic;

namespace clubPartyMy
{
    class Program
    {
        static void Main(string[] args)
        {
            int macCapacyti = int.Parse(Console.ReadLine());
            string[] input  = Console.ReadLine().Split();
            Stack<string> elements = new Stack<string>(input);
            Queue<string> halls = new Queue<string>();
            List<int> allGroups = new List<int>();
            int currentCapacity = 0;
            while (elements.Count > 0)
            {
                string currentElement = elements.Pop();
                bool IsNumber = int.TryParse(currentElement, out int parsedNumber);
                if (!IsNumber)
                {
                    halls.Enqueue(currentElement); 
                }
                else
                {
                    if (halls.Count == 0)
                    {
                        continue;
                    }
                    if (currentCapacity + parsedNumber > macCapacyti)
                    {
                        Console.WriteLine($"{ halls.Dequeue()} -> { string.Join(",", allGroups)}");
                        allGroups.Clear();
                        currentCapacity = 0;
                    }
                    if (halls.Count >0)
                    {
                        allGroups.Add(parsedNumber);
                        currentCapacity += parsedNumber;
                    }
                   
                }
            }
        }
    }
}
