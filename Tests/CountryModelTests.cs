using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Xunit;

namespace Tests
{
    public class CountryModelTests
    {
        [Fact]
        public void TestSameNamesAreEqual()
        {
            int canadaPopulation1 = 35151728;
            int canadaPopulation2 = 37000000;

            Country country1 = new Country("Canada", canadaPopulation1);
            Country country2 = new Country("Canada", canadaPopulation2);

            Assert.Equal(country1, country2);
        }

        [Fact]
        public void TestDifferentNamesAreNotEqual()
        {
            int canadaPopulation = 3;
            int indiaPopulation = 1182105000;

            Country country1 = new Country("Canada", canadaPopulation);
            Country country2 = new Country("India", indiaPopulation);

            Assert.NotEqual(country1, country2);
        }

        [Fact]
        public void TestEqualityIgnoresCase()
        {
            int canadaPopulation1 = 35151728;
            int canadaPopulation2 = 37000000;

            Country country1 = new Country("Canada", canadaPopulation1);
            Country country2 = new Country("canada", canadaPopulation2);

            Assert.Equal(country1, country2);
        }

        [Fact]
        public void TestUnion()
        {
            int canadaPopulation1 = 35151728;
            int canadaPopulation2 = 37000000;
            int indiaPopulation = 1182105000;
            int armeniaPopulation = 3249482;

            List<Country> first = new List<Country>()
            {
                new Country("Canada", canadaPopulation1),
                new Country("India", indiaPopulation)
            };

            List<Country> second = new List<Country>()
            {
                new Country("Canada", canadaPopulation2),
                new Country("Armenia", armeniaPopulation)
            };

            List<Country> union = first.Union(second).ToList();

            Country canada = FindCountryByName(union, "Canada");
            Country india = FindCountryByName(union, "India");
            Country armenia = FindCountryByName(union, "Armenia");

            Assert.Equal(3, union.Count);

            Assert.NotNull(canada);
            Assert.Equal(canadaPopulation1, canada.Population);

            Assert.NotNull(india);
            Assert.Equal(indiaPopulation, india.Population);

            Assert.NotNull(armenia);
            Assert.Equal(armeniaPopulation, armenia.Population);
        }

        private Country FindCountryByName(List<Country> countries, string name)
        {
            return countries.Find(country => country.Name == name);
        }
    }
}
