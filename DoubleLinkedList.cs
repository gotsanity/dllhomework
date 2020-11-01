using System;
using System.Text;

namespace DLLHomework
{
    class DoubleLinkedList
    {
        public static int NodeCount = 0;
        public static int MaleCount = 0;
        public static int FemaleCount = 0;
        public static Metadata MostPopularMale;
        public static Metadata MostPopularFemale;
        private Node Head;
        private Node Tail;

        public int GetNodeCount()
        {
            return NodeCount;
        }

        public int GetFemaleCount()
        {
            return FemaleCount;
        }

        public int GetMaleCount()
        {
            return MaleCount;
        }

        public Metadata GetMostPopularByGender(char gender)
        {
            if (gender == 'm')
            {
                return MostPopularMale;
            }
            else
            {
                return MostPopularFemale;
            }
        }

        public Node Add(Metadata data)
        {
            if (data.Gender.ToString().ToLower() == "m")
            {
                if (MaleCount == 0 || MostPopularMale.Rank < data.Rank)
                {
                    MostPopularMale = data;
                }
                
                MaleCount++;
            }
            else
            {
                if (FemaleCount == 0 || MostPopularFemale.Rank < data.Rank)
                {
                    MostPopularFemale = data;
                }

                FemaleCount++;
            }

            if (Head == null)
            {
                Head = new Node(data);
                NodeCount++;
                Tail = Head;
                return Head;
            }
            else if (Head.Data >= data)
            {
                Node temp = new Node(data);
                temp.Next = Head;
                temp.Next.Prev = temp;
                Head = temp;
                NodeCount++;
                return Head;
            }
            else 
            {
                Node current = Head;
                Node temp = new Node(data);

                while (current.Next != null && current.Next.Data < temp.Data)
                {
                    current = current.Next;
                }

                temp.Next = current.Next;

                if (current.Next != null)
                {
                    temp.Next.Prev = temp;
                }
                else
                {
                    Tail = temp;
                }
                current.Next = temp;
                temp.Prev = current;
                NodeCount++;
                return temp;
            }
        }

        public Node Search(string searchTerm)
        {
            Node searchHead = Head;
            Node searchTail = null;

            do
            {
                Node mid = SeekMiddleNode(searchHead, searchTail);

                if (mid == null)
                {
                    return null;
                }

                if (mid.Data.Name.ToLower().CompareTo(searchTerm.ToLower()) == 0)
                {
                    return mid;
                }
                else if (mid.Data.Name.ToLower().CompareTo(searchTerm.ToLower()) < 0) 
                {
                    searchHead = mid.Next;
                }
                else
                {
                    searchTail = mid;
                }
            } while (searchTail == null || searchTail != searchHead);

            return null;
        }

        public Node SearchExact(Metadata searchTerm)
        {
            Node searchHead = Head;
            Node searchTail = null;

            do
            {
                Node mid = SeekMiddleNode(searchHead, searchTail);

                if (mid == null)
                {
                    return null;
                }

                if (mid.Data == searchTerm)
                {
                    return mid;
                }
                else if (mid.Data < searchTerm)
                {
                    searchHead = mid.Next;
                }
                else
                {
                    searchTail = mid;
                }
            } while (searchTail == null || searchTail != searchHead);

            return null;
        }

        public Node SeekMiddleNode(Node searchHead, Node searchTail)
        {
            if (searchHead == null)
            {
                return null;
            }

            Node slow = searchHead;
            Node fast = searchHead.Next;

            while (fast != searchTail)
            {
                fast = fast.Next;
                if (fast != searchTail)
                {
                    slow = slow.Next;
                    fast = fast.Next;
                }
            }
            return slow;
        }

        public Node SlowSearch(string name)
        {
            Node current = Head;
            int counter = 0;
            while (current != null)
            {
                counter++;
                if (current.Data.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine(counter);
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public string Print()
        {
            Node current = Head;
            StringBuilder sb = new StringBuilder();
            while (current != null)
            {
                sb.Append(current.Data.Name + " - " + current.Data.Gender + "\n");

                current = current.Next;
            }

            return sb.ToString();
        }

        public string PrintReverse()
        {
            Node current = Tail;
            StringBuilder sb = new StringBuilder();
            while (current != null)
            {
                sb.Append(current.Data.Name + " - " + current.Data.Gender + "\n");

                current = current.Prev;
            }

            return sb.ToString();
        }
    }
}
