// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System;

namespace Classes
{
    public class Person
    {
        public string Name;
        public int Id;

        public Person(int id,string name) { 
            this.Id =  id; 
            this.Name = name;
        }


        public void Introduce(string to)
        {
            Console.WriteLine("Hi {0}, I am {1}", to, Name);
        }

        public Person Parse(string str)
        {
            var person = new Person(1,"jeel");
            person.Name = str;
            return person;
        }
    }

    public class Calculator
    {
        public int Add(params int[] numbers)
        {
            var sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }
            return sum;
        }
    }

     class Program
    {
        static void Main(string[] args)
        {
            //class
            Person p = new Person(1, "jeel");
            var per = p.Parse("Dra");
            p.Name = "Drashti";
            p.Introduce("Sweet");
            Console.WriteLine();


            //fields
            var cust = new Customer(1);
            cust.Orders.Add(new Order());
            cust.Orders.Add(new Order());
            cust.Orders.Add(new Order());
            cust.Promote();
            Console.WriteLine(cust.Orders.Count);

            try
            {
                var num = int.Parse("abc");
            }
            catch (Exception)
            {

                Console.WriteLine("Conversion Failed.");
            }


            int number;
            var res = int.TryParse("abc", out number);

            if (res) { Console.WriteLine(number); } else { Console.WriteLine("Conversion failed."); }

            //Structure
            var str = new Structure();
            str.carData();
            str.employeeData();


            //AccessModifiers

            var AccessModifiers = new AccessModifiers();
            AccessModifiers.SetBirthdate(new DateTime(2000, 12, 6));
            Console.WriteLine(AccessModifiers.GetBirthdate());

        }

        static void UseParams()
        {
            //Method
            var cal = new Calculator();
            Console.WriteLine(cal.Add(1, 2));
            Console.WriteLine(cal.Add(1, 2, 3));
            Console.WriteLine(cal.Add(1, 2, 3, 4));
            Console.WriteLine(cal.Add(new int[] { 1, 2, 3, 4, 5 }));
        }
        static void UsePoints()
        {
            try
            {
                //Methods
                var point = new Point(10, 20);
                point.Move(null);
                Console.WriteLine("Point is at ({0},{1})", point.x, point.y);

                point.Move(100, 200);
                Console.WriteLine("Point is at {0},{1}", point.x, point.y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occured.");
            }
        }
    }
}

