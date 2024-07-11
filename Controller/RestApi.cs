using Ejercicio2_1.Models;
using Newtonsoft.Json.Linq;

namespace Ejercicio2_1.Controller
{
    public class RestApi
    {
        public static string endpoint = "https://restcountries.com/v3.1/region/";

        public RestApi()
        {

        }

        public async Task<List<Countries>> GetCountries(string region)
        {
            try
            {
                List<Countries> countries = new List<Countries>();

                using (HttpClient client = new HttpClient())
                {
                    var resp = await client.GetAsync(endpoint + region);

                    if (resp.IsSuccessStatusCode)
                    {
                        var content = resp.Content.ReadAsStringAsync().Result;

                        JArray results = JArray.Parse(content);

                        Countries temp = null;

                        foreach (var item in results)
                        {
                            temp = new Countries();

                            var curTemp = new List<Countries.Currencies>();
                            foreach (var current in item["currencies"].Values())
                            {
                                try
                                {
                                    curTemp.Add(new Countries.Currencies()
                                    {
                                        name = current["name"].ToString(),
                                        symbol = current["symbol"].ToString()
                                    });
                                }
                                catch
                                {
                                    curTemp.Add(new Countries.Currencies()
                                    {
                                        name = current["name"].ToString(),
                                        symbol = "--"
                                    });
                                }
                            }

                            var languTemp = new List<string>();
                            foreach (var current in item["languages"].Values())
                                languTemp.Add(current.ToString());

                            var timeTemp = new List<string>();
                            foreach (var current in item["timezones"].Values())
                                timeTemp.Add(current.ToString());

                            var contTemp = new List<string>();
                            foreach (var current in item["continents"].Values())
                            {
                                contTemp.Add(current.ToString());
                            }

                            temp.NameCountry = new Countries.Name()
                            {
                                common = item["name"]["common"].ToString(),
                                official = item["name"]["official"].ToString()

                            };

                            try
                            {
                                temp.independent = item["independent"].ToString(); //Region de EUROPA
                            }
                            catch
                            {
                                temp.independent = "--";
                            }

                            temp.status = item["status"].ToString();
                            temp.currencies = curTemp;

                            try
                            {
                                temp.capital = item["capital"][0].ToString(); // region de ASIA
                            }
                            catch
                            {
                                temp.capital = "--";
                            }

                            temp.region = item["region"].ToString();
                            temp.subregion = item["subregion"].ToString();
                            temp.languages = languTemp;
                            temp.latlng = new List<double>() { (double)item["latlng"][0], (double)item["latlng"][1] };
                            temp.flag = item["flag"].ToString();
                            temp.maps = new Countries.Maps()
                            {
                                googleMaps = item["maps"]["googleMaps"].ToString(),
                                openStreetMaps = item["maps"]["openStreetMaps"].ToString()
                            };

                            temp.population = (int)item["population"];
                            temp.timezones = timeTemp;
                            temp.continents = contTemp;

                            temp.flags = new Countries.Flags()
                            {
                                png = item["flags"]["png"].ToString(),
                                svg = item["flags"]["svg"].ToString()
                            };

                            temp.startOfWeek = item["startOfWeek"].ToString();

                            try
                            {
                                temp.idd = new Countries.Idd()
                                {
                                    root = item["idd"]["root"].ToString(),
                                    suffixes = item["idd"]["suffixes"][0].ToString()
                                };
                            }
                            catch
                            {
                                temp.idd = new Countries.Idd()
                                {
                                    root = "-",
                                    suffixes = "---"
                                };

                            }
                            countries.Add(temp);
                        }
                    }

                }
                return countries.OrderBy(x => x.NameCountry.common).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }

        }
    }
}
