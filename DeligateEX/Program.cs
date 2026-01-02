using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEX
{
    public  abstract class Unit
    {
        public abstract void TestFunc();
    }

    public class Monster : Unit
    {
        public override void TestFunc()
        {
                
        }

    }

    public class Item
    {
        private string m_name;
        private int m_price;
        public int Price
        {
            get { return m_price; }
        }

        private int m_att;
        private int m_range;

        public delegate void ItemDelegate();
        public void TestDelegate (ItemDelegate dele)
        {
            dele();
        }

        //딜리게이트를 통해서 만들어짐
        
        // 액션 - 리턴을 안함
        public Action TestAction;
        //펑션 - 리턴을 한다
        public Func<int> TestFunc;

        public void TestConsoleWrite()
        {
            Console.WriteLine("매서드");
        }

        public delegate void OnButton();

        public void Button(OnButton button)
        {
            button();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> list = new List<Item>();
            list.Add(new Item());
            list.Add(new Item());
            list.Add(new Item());

            list[0].TestDelegate(() => 
            {
                Console.WriteLine("딜리게잇또");
                
            });

            list.Sort((a , b)=> { return a.Price.CompareTo(b.Price);});
           
            list[0].TestConsoleWrite();

            list[0].TestAction = () => { Console.WriteLine("액션"); };
            list[0].TestAction.Invoke();
            list[0].TestAction.Invoke();
            list[0].TestAction.Invoke();

            Console.ReadKey();
        }
    }
}
