using Azure;
using CountriesLibrary;
using EF_Countries.Context;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirst.LazyLoading
{
    class MainClass
    {
        static void Main()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1.  Show All Countries");
                    Console.WriteLine("2.  Show All Coutries Name");
                    Console.WriteLine("3.  Show All Countries Capital");
                    Console.WriteLine("4.  Show All European Countries");
                    Console.WriteLine("5.  Show All Countries with specific area");
                    Console.WriteLine("6.  Show All Coutries with a or e in name");
                    Console.WriteLine("7.  Show All Countries starting with 'A'");
                    Console.WriteLine("8.  Show All Countries by Area Range");
                    Console.WriteLine("9.  Show All Countries By Population");
                    Console.WriteLine("10. Add new Country");
                    Console.WriteLine("11. Edit Country");
                    Console.WriteLine("12. Remove Country");
                    Console.WriteLine("0.  Exit");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            ShowAllCountries();
                            break;
                        case 2:
                            ShowAllCoutriesName();
                            break;
                        case 3:
                            ShowAllCountriesCapital();
                            break;
                        case 4:
                            ShowAllEuropeanCountries();
                            break;
                        case 5:
                            ShowAllCountriesWithSpecificArea();
                            break;
                        case 6:
                            ShowAECountries();
                            break;
                        case 7:
                            ShowCountriesStartingWithA();
                            break;
                        case 8:
                            ShowCountriesByAreaRange();
                            break;
                        case 9:
                            ShowCountriesByPopulation();
                            break;
                        case 10:
                            AddNewCountry();
                            break;
                        case 11:
                            EditCountry();
                            break;
                        case 12:
                            RemoveCountry();
                            break;
                        case 0:
                            return;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RemoveCountry()
        {
            Console.Clear();
            using (var db = new Context())
            {
                Console.WriteLine("Enter Country Number to Edit: ");
                var countries = db.Countries.ToList();

                for (int i = 0; i < countries.Count; i++)
                {
                    Console.WriteLine($"Country #{i + 1} {countries[i].Name}; Capital: {countries[i].Capital}");
                }

                int number = int.Parse(Console.ReadLine()!) - 1;

                var country = (from cout in db.Countries
                               select cout).ToList()[number];
                db.Countries.RemoveRange(country);
                db.SaveChanges();
            }
        }

        private static void EditCountry()
        {
            Console.Clear();

            using (var db = new Context())
            {
                Console.WriteLine("Enter Country Number to Edit: ");
                var countries = db.Countries.ToList();

                for (int i = 0; i < countries.Count; i++)
                {
                    Console.WriteLine($"Country #{i + 1} {countries[i].Name}; Capital: {countries[i].Capital}");
                }

                int number = int.Parse(Console.ReadLine()!) - 1;

                if (number >= 0 && number < countries.Count)
                {
                    var countryToEdit = countries[number];

                    do
                    {
                        Console.WriteLine("Enter Country Name: ");
                        countryToEdit.Name = Console.ReadLine()!;
                    }
                    while (string.IsNullOrWhiteSpace(countryToEdit.Name));

                    do
                    {
                        Console.WriteLine("Enter Country Capital: ");
                        countryToEdit.Capital = Console.ReadLine()!;
                    }
                    while (string.IsNullOrWhiteSpace(countryToEdit.Capital));

                    Console.WriteLine("Enter Country Population: ");
                    countryToEdit.Popilation = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Enter Country Area: ");
                    countryToEdit.Area = double.Parse(Console.ReadLine()!);

                    Console.WriteLine("Select Continent:");

                    var continents = db.Continents.ToList();
                    Console.Clear();
                    for (int i = 0; i < continents.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {continents[i].Name}");
                    }

                    Console.WriteLine("Enter Number of Continent: ");
                    int continentIndex = int.Parse(Console.ReadLine()!) - 1;

                    if (continentIndex >= 0 && continentIndex < continents.Count)
                    {
                        countryToEdit.Continent = continents[continentIndex];
                        db.SaveChanges();
                    }
                    else Console.WriteLine("Invalid Continent Index.");

                }
                else Console.WriteLine("Invalid Country Index.");
            }
        }

        private static void AddNewCountry()
        {
            Console.Clear();

            string name, capital;

            using (var db = new Context())
            {
                do
                {
                    Console.WriteLine("Enter Country Name: ");
                    name = Console.ReadLine()!;
                }
                while (name.Trim().IsNullOrEmpty());
                do
                {
                    Console.WriteLine("Enter Country Capital: ");
                    capital = Console.ReadLine()!;
                }
                while (capital.Trim().IsNullOrEmpty());

                Console.WriteLine("Enter Country Population: ");
                int population = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Country Area: ");
                double area = double.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Number of Continent: ");
                var continents = db.Continents.ToList();

                for (int i = 0; i < continents.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {continents[i].Name}");
                }

                int number = int.Parse(Console.ReadLine()!);
                var continent = db.Continents.ToList()[number - 1];

                Country country = new Country
                {
                    Name = name,
                    Capital = capital,
                    Popilation = population,
                    Area = area,
                    Continent = continent
                };
                db.Countries?.Add(country);
                db.SaveChanges();
            }
        }

        static void ShowAECountries()
        {
            Console.Clear();
            using (var db = new Context())
            {
                var query = (from country in db.Countries
                             where country.Name.Contains("a") || country.Name.Contains("e")
                             select country).ToList();
                int iter = 0;
                foreach (var cou in query)
                {
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}; Continent: {cou.Continent?.Name}; Population: {cou.Popilation}; Area: {cou.Area}");
                }
            }
            Console.ReadKey();
        }

        static void ShowAllCountries()
        {
            Console.Clear();
            using (var db = new Context())
            {
                var query = (from country in db.Countries
                             select country).ToList();
                int iter = 0;
                foreach (var cou in query)
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}; Continent: {cou.Continent?.Name}; Population: {cou.Popilation}; Area: {cou.Area}");
            }
            Console.ReadKey();
        }

        static void ShowAllCoutriesName()
        {
            Console.Clear();
            using (var db = new Context())
            {
                var query = from country in db.Countries
                            select country;
                int iter = 0;
                foreach (var cou in query)
                    Console.WriteLine($"Country #{++iter} {cou.Name}");
            }
            Console.ReadKey();
        }

        static void ShowAllCountriesCapital()
        {
            Console.Clear();
            using (var db = new Context())
            {
                var query = from country in db.Countries
                            select country;
                int iter = 0;
                foreach (var cou in query)
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}");
            }
            Console.ReadKey();
        }

        static void ShowAllEuropeanCountries()
        {
            Console.Clear();
            using (var db = new Context())
            {
                var query = from country in db.Countries
                            where country.Continent.Name == "Europe"
                            select country;
                int iter = 0;
                foreach (var cou in query)
                {
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}");
                }
            }
            Console.ReadKey();
        }


        static void ShowAllCountriesWithSpecificArea()
        {
            Console.Clear();
            using (var db = new Context())
            {
                Console.WriteLine("Enter Minimum Area For Country: ");
                double number = double.Parse(Console.ReadLine()!);

                var query = (from country in db.Countries
                             where country.Area > number
                             select country).ToList();
                int iter = 0;
                foreach (var cou in query)
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}; Continent: {cou.Continent?.Name}; Population: {cou.Popilation}; Area: {cou.Area}");
            }
            Console.ReadKey();
        }

        static void ShowCountriesStartingWithA()
        {
            Console.Clear();
            using (var db = new Context())
            {
                var query = db.Countries
                    .AsEnumerable()
                    .Where(country => country.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                int iter = 0;
                foreach (var cou in query)
                {
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}; Continent: {cou.Continent?.Name}; Population: {cou.Popilation}; Area: {cou.Area}");
                }
            }

            Console.ReadKey();
        }

        static void ShowCountriesByAreaRange()
        {
            Console.Clear();
            Console.Write("Enter the minimum area: ");
            double minArea;
            while (!double.TryParse(Console.ReadLine(), out minArea))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the minimum area.");
                Console.Write("Enter the minimum area: ");
            }
            Console.Write("Enter the maximum area: ");
            double maxArea;
            while (!double.TryParse(Console.ReadLine(), out maxArea))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the maximum area.");
                Console.Write("Enter the maximum area: ");
            }
            Console.Clear();
            using (var db = new Context())
            {
                var query = db.Countries
                    .Where(country => country.Area >= minArea && country.Area <= maxArea)
                    .ToList();

                int iter = 0;
                foreach (var cou in query)
                {
                    Console.WriteLine($"Country #{++iter} {cou.Name}; Capital: {cou.Capital}; Continent: {cou.Continent?.Name}; Population: {cou.Popilation}; Area: {cou.Area}");
                }
            }
            Console.ReadKey();
        }

        static void ShowCountriesByPopulation()
        {
            Console.Clear();

            Console.Write("Enter the minimum population: ");
            int minPopulation;
            while (!int.TryParse(Console.ReadLine(), out minPopulation))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the minimum population.");
                Console.Write("Enter the minimum population: ");
            }

            using (var db = new Context())
            {
                var query = db.Countries
                    .Where(country => country.Popilation > minPopulation)
                    .Select(country => country.Name)
                    .ToList();

                Console.WriteLine($"Countries with population greater than {minPopulation}:");

                foreach (var countryName in query)
                {
                    Console.WriteLine(countryName);
                }
            }
            Console.ReadKey();
        }
    }
}