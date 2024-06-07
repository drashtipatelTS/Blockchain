// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

/*var a = 10;
var b = 3;
var c = 4;

Console.WriteLine((float)a / (float)b);
Console.WriteLine(a + b * c);
Console.WriteLine((a + b) * c);
Console.WriteLine(a > b);
Console.WriteLine(a == b);
Console.WriteLine(a != b);
Console.WriteLine(!(a != b));
Console.WriteLine(c > b && c > a);*/

/*try
{
    string str = "true";
    bool i = Convert.ToBoolean(str);
    Console.WriteLine(i);
}
catch (Exception)
{
    Console.WriteLine("string not converted!");
}
*/
/*try
{
    var number = "1234";
    byte i = Convert.ToByte(number);
    Console.WriteLine(i);
}
catch (Exception)
{
    Console.WriteLine("The number could not be converted to a byte");
}*/

/* Console.WriteLine("Hello, World!");
var number=1;
var count = 10;
var totalPrice = 20.5f;
var Character = 'D';
var firstName = "Drashti";
var isWorking = false;
Console.WriteLine(number);
Console.WriteLine(count);
Console.WriteLine(totalPrice);
Console.WriteLine(Character);
Console.WriteLine(firstName);
Console.WriteLine(isWorking);*/

/*Console.WriteLine("{0} {1}",byte.MinValue,byte.MaxValue);
Console.WriteLine("{0} {1}",float.MinValue,float.MaxValue);*/

/*const float Pi = 3.14f;
Console.WriteLine(Pi);*/

/*byte b = 1;
int i = b;
Console.WriteLine(i);*/

/*int i = 1000;
byte b = (byte)i;
Console.WriteLine(b);*/


namespace FirstProgram
{
    internal class SecondProgram
    {
        public static void Main(string[] args)
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            var smallests = GetSmallests(numbers, 3);

            foreach (var number in smallests)
                Console.WriteLine(number);  
        }

        public static List<int> GetSmallests(List<int> list, int count)
        {
            var smallests = new List<int>();

            while (smallests.Count < count)
            {
                var min = GetSmallest(list);
                smallests.Add(min);
                list.Remove(min);
            }

            return smallests;
        }

        public static int GetSmallest(List<int> list)
        {
            // Assume the first number is the smallest
            var min = list[0];
            for (var i = 1; i < list.Count; i++)
            {
                if (list[i] > min)
                    min = list[i];
            }
            return min;
        }
    }
}

