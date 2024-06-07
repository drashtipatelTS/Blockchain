using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class AccessModifiers
    {
        private DateTime _birthdate;

        public void SetBirthdate(DateTime birthdate) {  _birthdate = birthdate; }

        public DateTime GetBirthdate() { return _birthdate;}
    }
}
