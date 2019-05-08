using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Country
    {
        private readonly string _name;
        private readonly int _population;

        public string Name { get { return _name; } }

        public int Population { get { return _population; } }

        public Country(string name, int population)
        {
            _name = name;
            _population = population;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((Country)obj);
        }

        public bool Equals(Country other)
        {
            return Name.ToLower() == other.Name.ToLower();
        }

        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode();
        }

        public static bool operator ==(Country a, Country b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Country a, Country b)
        {
            return !a.Equals(b);
        }
    }
}
