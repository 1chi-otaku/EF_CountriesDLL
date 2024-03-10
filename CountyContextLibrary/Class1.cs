using CountriesLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Countries.Context
{

    public class Context : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }


        public Context()
        {

            if (Database.EnsureCreated())
            {
                Continent europe = new Continent { Name = "Europe" };
                Continent asia = new Continent { Name = "Asia" };
                Continent africa = new Continent { Name = "Africa" };
                Continent northAmerica = new Continent { Name = "North America" };
                Continent southAmerica = new Continent { Name = "South America" };
                Continent antarctica = new Continent { Name = "Antarctica" };
                Continent australia = new Continent { Name = "Australia" };

                Continents?.Add(europe);
                Continents?.Add(asia);
                Continents?.Add(africa);
                Continents?.Add(northAmerica);
                Continents?.Add(southAmerica);
                Continents?.Add(antarctica);
                Continents?.Add(australia);


                Countries?.Add(new Country { Name = "Ukraine", Capital = "Kyiv", Popilation = 40000000, Area = 603.628, Continent = europe });
                Countries?.Add(new Country { Name = "Germany", Capital = "Berlin", Popilation = 83000000, Area = 357.022, Continent = europe });
                Countries?.Add(new Country { Name = "China", Capital = "Beijing", Popilation = 1400000000, Area = 9596.960, Continent = asia });
                Countries?.Add(new Country { Name = "Nigeria", Capital = "Abuja", Popilation = 206000000, Area = 923.768, Continent = africa });
                Countries?.Add(new Country { Name = "United States", Capital = "Washington, D.C.", Popilation = 331000000, Area = 9833.517, Continent = northAmerica });
                Countries?.Add(new Country { Name = "Brazil", Capital = "Brasília", Popilation = 213993437, Area = 8515.767, Continent = southAmerica });
                Countries?.Add(new Country { Name = "Australia", Capital = "Canberra", Popilation = 25700000, Area = 7692.024, Continent = australia });
                Countries?.Add(new Country { Name = "India", Capital = "New Delhi", Popilation = 1393409038, Area = 3287.260, Continent = asia });
                Countries?.Add(new Country { Name = "South Africa", Capital = "Pretoria", Popilation = 60000000, Area = 1221.037, Continent = africa });
                Countries?.Add(new Country { Name = "Canada", Capital = "Ottawa", Popilation = 37700000, Area = 9976.140, Continent = northAmerica });



                SaveChanges();
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=PECHKA\SQLEXPRESS;Database=CountriesEF;Integrated Security=SSPI;TrustServerCertificate=true");

        }
    }
}
