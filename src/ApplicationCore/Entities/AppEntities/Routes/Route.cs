using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.AppEntities.Routes
{
    public class Route : BaseEntity
    {
        public Route(int id, int startCityId, int finishCityId)
        {
            Id = id;
            StartCityId = startCityId;
            FinishCityId = finishCityId;
        }
        
        public int StartCityId { get; private set; }

        public City StartCity { get; set;}
        public int FinishCityId { get; private set; }
        
        public City FinishCity { get; set;}
    }
}