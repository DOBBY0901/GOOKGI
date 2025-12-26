using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureEX
{
    internal class MyLinkedList
    {
        public MyNode head;

        public int count = 0;
        public void AddLast(int _data)
        {
            if (head == null)
                head = new MyNode(_data);

            MyNode currentNode = head;

             while(true)
            {
                if (currentNode.next == null)
                {
                    currentNode.next = new MyNode(_data);
                    break;
                }
                else
                {
                    currentNode = currentNode.next;
                }

            }    
            count++;
        }

        public void Search(int _value)
        {
            MyNode currentNode = head;
            while (true)
            {
                if (currentNode.data == _value)
                {
                    
                }
                else
                {
                    if (currentNode.next == null)
                        break;

                    currentNode = currentNode.next;
                }
            }

        }
    }
}
