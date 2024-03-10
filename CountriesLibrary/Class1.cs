using System.Diagnostics.Metrics;

namespace CountriesLibrary
{
    public class Country
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Capital { get; set; }
        public int? Popilation { get; set; }
        public double? Area { get; set; }
        public virtual Continent? Continent { get; set; }

    }

    public class Continent
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Country>? Countries { get; set; }
    }


}
