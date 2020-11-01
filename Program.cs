using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DLLHomework
{
    class Program
    {
        public static Stopwatch sw = new Stopwatch();
        public static DoubleLinkedList dll = new DoubleLinkedList();

        static void Main(string[] args)
        {
            StartTimer("Started loading data from file");
            LoadDataFromFile();
            ShowExecutionTime();

            int userInput = 0;
            do
            {
                try
                {
                    userInput = PrintMenu();
                }
                catch
                {
                    userInput = 0;
                }

                Console.WriteLine();

                switch (userInput)
                {
                    case 1: // search by name
                        SearchByName();
                        break;
                    case 2: // Display current count all
                        SeeCurrentCount();
                        break;
                    case 3: // see female
                        SeeCurentCountByGender('f');
                        break;
                    case 4: // see male count
                        SeeCurentCountByGender('m');
                        break;
                    case 5: // add a name
                        AddName();
                        break;
                    case 6:
                        FindMostPopularNameByGender();
                        break;
                    case 7: // exit
                        break;
                    default: // handle invalid
                        Console.WriteLine("Invalid Input, Please use numeric choices.");
                        break;
                }

                Console.WriteLine();

            } while (userInput != 7);
        }

        public static int PrintMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine();
            Console.WriteLine("1. Search by name");
            Console.WriteLine("2. See count of total items");
            Console.WriteLine("3. See count of all female entries");
            Console.WriteLine("4. See count of all male entries");
            Console.WriteLine("5. Add a name");
            Console.WriteLine("6. Find the most popular name by gender");
            Console.WriteLine("7. Exit");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        public static void StartTimer(string message)
        {
            Console.WriteLine(message);
            sw.Restart();
        }

        public static void ShowExecutionTime()
        {
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Time to complete: {0}", time.ToString());
        }

        public static void SearchByName()
        {
            Console.Write("What name would you like to search for? ");
            string term = Console.ReadLine();
            StartTimer("Starting Binary Search");
            Node target = dll.Search(term);
            ShowExecutionTime();

            if (target != null)
            {
                Console.WriteLine("Search found a record matching the following: Name: {0}, Gender: {1}, Rank: {2}", target.Data.Name, target.Data.Gender, target.Data.Rank);
            }
            else
            {
                Console.WriteLine("No entries found.");
            }

            StartTimer("Starting linear search");
            target = dll.SlowSearch(term);
            ShowExecutionTime();
            if (target != null)
            {
                Console.WriteLine("Search found a record matching the following: Name: {0}, Gender: {1}, Rank: {2}", target.Data.Name, target.Data.Gender, target.Data.Rank);
            }
            else
            {
                Console.WriteLine("No entries found.");
            }
        }

        public static void SeeCurrentCount()
        {
            Console.WriteLine("There are currently {0} nodes.", dll.GetNodeCount());
            Console.WriteLine(dll.Print());
            Console.WriteLine(dll.PrintReverse());
        }

        public static void SeeCurentCountByGender(char Gender)
        {
            if (Gender == 'm')
            {
                Console.WriteLine("There are currently {0} male names in the list.", dll.GetMaleCount());
            }
            else
            {
                Console.WriteLine("There are currently {0} femmale names in the list.", dll.GetFemaleCount());
            }
        }

        public static void AddName()
        {
            Console.Write("What is the name you want to add? ");
            string name = Console.ReadLine();
            Console.Write("What is the person's gender? ");
            string gender = Console.ReadLine();
            Console.Write("What rank? ");
            string rank = Console.ReadLine();

            Metadata data = new Metadata(name, gender.ToCharArray()[0], int.Parse(rank));

            if (dll.SearchExact(data) != null)
            {
                Console.Write("Name collision detected. Do you want to rename {0} to {0}_1? ", data.Name);
                char answer = Console.ReadLine().ToLower().ToCharArray()[0];

                if (answer == 'y')
                {
                    data.Name += "_1";
                }
                else
                {
                    Console.WriteLine("Cancelling insert.");
                    return;
                }
            }

            Node temp = dll.Add(data);

            if (temp == null)
            {
                Console.WriteLine("Unable to insert node.");
            }
            else
            {
                Console.WriteLine("Inserted new node with a name of {0}.", temp.Data.Name);
            }
        }

        public static void FindMostPopularNameByGender()
        {
            Console.Write("Would you like to see the most popular male or female names? ");
            char gender = Console.ReadLine().ToCharArray()[0];
            switch (gender) {
                case 'm':
                    Metadata male = dll.GetMostPopularByGender('m');
                    Console.WriteLine("The most popular male name is {0} at rank {1}.", male.Name, male.Rank);
                    break;
                case 'f':
                    Metadata female = dll.GetMostPopularByGender('f');
                    Console.WriteLine("The most popular female name is {0} at rank {1}.", female.Name, female.Rank);
                    break;
                default:
                    Console.WriteLine("Missing a proper input for most popular by gender.");
                    break;
            }
        }

        private static void LoadDataFromFile()
        {
            var nameData = File.ReadAllLines(@"..\..\..\yob2019.txt");
            var nameList = from item in nameData
                           let data = item.Split(',')
                           select new
                           {
                               Name = data[0],
                               Gender = data[1],
                               Rank = data[2]
                           };

            foreach (var item in nameList)
            {
                dll.Add(new Metadata(item.Name, item.Gender.ToCharArray()[0], int.Parse(item.Rank)));
            }
        }
    }
}
