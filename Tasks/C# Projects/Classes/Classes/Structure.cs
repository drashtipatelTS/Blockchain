using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Structure
    {
        struct Car
        {
            public string Brand;
            public string Model;
            public int Year;
            public float Price;
        }

        struct Employee
        {
            public string Firstname;
            public string Lastname;
            public float HourlyWage;
            public bool CompletedTraining;
        }

        public void carData()
        {
            int num;
            int size;
           
            Console.WriteLine("Enter number of data you want to insert:");
            size = int.Parse(Console.ReadLine());

            Car[] cars = new Car[size];

            for (num = 0; num < size; num++)
            {
                Car car1;
                Console.WriteLine("What's the Brans?");
                car1.Brand = Console.ReadLine();

                Console.WriteLine("What's the Model?");
                car1.Model = Console.ReadLine();

                Console.WriteLine("What's the Year?");
                car1.Year = int.Parse(Console.ReadLine());

                Console.WriteLine("What's the Price?");
                car1.Price = float.Parse(Console.ReadLine());
                cars[num] = car1;
            }
            for(num = 0; num < size;num++)
            {
                Console.WriteLine("Brand:"+cars[num].Brand);
                Console.WriteLine("Model:"+cars[num].Model);
                Console.WriteLine("Year:"+cars[num].Year);
                Console.WriteLine("Price:"+cars[num].Price);
            }
        }

        public void employeeData()
        {
            Employee emp;
           
            emp.Firstname = "Drashti";
            
            emp.Lastname = "Patel";

            emp.HourlyWage = 35.7f;

            emp.CompletedTraining = true;
        }

    }
}
