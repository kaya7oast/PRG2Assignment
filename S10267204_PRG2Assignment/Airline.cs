using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"Name: {Name,-19} Code: {Code,-3}";
        }
    }
}
