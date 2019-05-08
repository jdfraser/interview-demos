using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Data
{
    class CountryRepository
    {
        private DbConnection _connection;
        private IStatService _service;

        public CountryRepository(DbConnection connection, IStatService service)
        {
            _connection = connection;
            _service = service;
        }

        public List<Country> GetAllCountries()
        {
            List<Country> dbCountries = GetCountriesFromDb();
            List<Country> apiCountries = GetCountriesFromApi();

            return dbCountries.Union(apiCountries).ToList();
        }

        private List<Country> GetCountriesFromDb()
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT CountryName, SUM(City.Population) FROM COUNTRY " +
                                  "INNER JOIN State USING(CountryId) " +
                                  "INNER JOIN City USING(StateId) " +
                                  "GROUP BY CountryId";

            DbDataReader reader = command.ExecuteReader();

            List<Country> countries = new List<Country>();
            while (reader.Read())
            {
                string name = Convert.ToString(reader.GetValue(0));
                int population = Convert.ToInt32(reader.GetValue(1));

                countries.Add(new Country(name, population));
            }

            return countries;
        }

        private List<Country> GetCountriesFromApi()
        {
            List<Tuple<string, int>> countryPopulations = _service.GetCountryPopulations();

            List<Country> countries = new List<Country>();

            foreach (Tuple<string, int> countryPopulation in countryPopulations)
            {
                string name = countryPopulation.Item1;
                int population = countryPopulation.Item2;

                countries.Add(new Country(name, population));
            }

            return countries;
        }
    }
}
