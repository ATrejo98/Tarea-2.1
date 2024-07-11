namespace Ejercicio2_1.Models
{
    public class Countries
    {
        public Name NameCountry { get; set; }
        public string independent { get; set; }
        public string status { get; set; }
        public IList<Currencies> currencies { get; set; }
        public string capital { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public IList<string> languages { get; set; }
        public IList<double> latlng { get; set; }
        public string flag { get; set; }
        public Maps maps { get; set; }
        public Idd idd { get; set; }
        public int population { get; set; }
        public IList<string> timezones { get; set; }
        public IList<string> continents { get; set; }
        public Flags flags { get; set; }
        public string startOfWeek { get; set; }
        public IList<double> capitalInfo { get; set; }

        public string CodeCountry
        {
            get { return idd.root + idd.suffixes; }
        }

        public string NameCodeCountry
        {
            get { return NameCountry.official + " (" + idd.root + idd.suffixes + ")"; }
        }

        public class Name
        {
            public string common { get; set; }
            public string official { get; set; }
        }

        public class Currencies
        {
            public string name { get; set; }
            public string symbol { get; set; }
        }

        public class Maps
        {
            public string googleMaps { get; set; }
            public string openStreetMaps { get; set; }
        }
        public class Flags
        {
            public string png { get; set; }
            public string svg { get; set; }
        }

        public class Idd
        {
            public string root { get; set; }
            public string suffixes { get; set; }
        }
    }
}
