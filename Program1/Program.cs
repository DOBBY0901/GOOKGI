using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Program1
{
    internal class Program
    {
        

        static void Main(string[] args)
        {

            string str = "";
            string space = " ";
            string str1 = "";
            string space1 = " ";

            for (int i = 0; i < 10; i++)
            {
                for (int j = 10; i < j; j--)
                {
                    space += " ";
                }
                str += "▲";
                Console.WriteLine(space + str);
                space = " ";
            }

            for (int i = 0; i < 10; i++)
            {

                for (int j = 10; i < j; j--)
                {
                    str1 += "▼";
                    
                }

                space += " ";
                space1 += " ";
                Console.WriteLine(space + str1 + space1);
            }



        }
    
       

    }
}
