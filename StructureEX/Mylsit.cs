using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace StructureEX
{
    internal class MyLsit<T>
    {
        public T[] array;
               private int count;
        public int Count { get { return count; } }
        
        private int capacity;
        public int Capacity { get { return capacity; } }
        
       

        public  MyLsit()
        {
            array = new T[4];
            count = 0;
            capacity = array.Length;
        }

        public void Add(T _data)
        {
           if(count >=  capacity)
            {
                Resize();
            }

            array[count] = _data;
            count++;
        }

        private void Resize()
        {
            T[] tempArray = new T[count * 2];
            for (int i = 0; i < count; i++)
            {
                tempArray[i] = array[i];
            }
            array = tempArray;
            capacity = array.Length;
        }

        public void Insert(int _index, T _data)
        {
            for (int i = count; i > _index; i--)
            {
                array[i] = array[i - 1];               
            }
            
            array[_index] = _data;
            count++;
        }

        public void Remove(T _data)
        {
            for(int i = 0; i < count;i++)
            {
                if (_data.Equals(array[i]))
                {   
                    for(int j = i; j< count-1; j++)
                       array[j] = array[j+1];
                    count--;  
                    break;
                }
            }
        }

        public void RemoveAt(int _index)
        {
            for(int i = _index;  i < count; i++)
                array[i] = array[i+1];
            count--;
        }

    }
}
