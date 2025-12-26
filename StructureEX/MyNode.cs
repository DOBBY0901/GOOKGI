using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureEX
{
    internal class MyNode
    {
        public int data;
        public MyNode next;

        public MyNode(int _data) 
        { 
            data = _data;
            next = null;
        }
    }
}
