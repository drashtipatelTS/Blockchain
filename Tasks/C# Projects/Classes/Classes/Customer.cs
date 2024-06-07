using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class Customer
    {
            public int num;
            public string add;
            readonly public List<Order> Orders = new List<Order>();

            public Customer(int num)
            {
                this.num = num;
            }

            public Customer(int num, string add) : this(num)
            {
                this.add = add;
            }

            public void Promote()
            {
                /*Orders = new List<Order>();*/
            }
    }
}
