using System.Collections.Generic;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Routes;

namespace ApplicationCore.Entities.Values
{
    public class DriverAppDataInfo
    {
        public List<City> Cities { get; set; }
        public List<Kit> Kits { get; set; }
        public List<CarType> CarTypes { get; set; }
        public List<CarBrand> CarBrands { get; set; }
        public List<CarColor> CarColors { get; set; }
    }
}