using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;

namespace Backend
{
    class CountryPopulationApp
    {
        public CountryPopulationApp()
        {

        }

        public void Run()
        {
            try
            {
                CountryRepository repository = GetRepository();

                foreach (Country country in repository.GetAllCountries())
                {
                    Console.WriteLine($"{country.Name}: {country.Population}");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }

        private CountryRepository GetRepository()
        {
            Console.WriteLine("Started");
            Console.WriteLine("Getting DB Connection...");

            IDbManager db = new SqliteDbManager();
            DbConnection connection = db.getConnection();

            if (connection == null)
            {
                throw new DataException("Failed to get connection");
            }

            IStatService service = new ConcreteStatService();

            return new CountryRepository(connection, service);
        }
    }
}
