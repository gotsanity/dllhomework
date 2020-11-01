using System;
using System.Collections.Generic;
using System.Text;

namespace DLLHomework
{
    class Node
    {
        public Node Prev;
        public Node Next;
        public Metadata Data;

        public Node(Metadata data)
        {
            Prev = null;
            Next = null;
            Data = data;
        }

    }
}
