namespace ApplicationCore.Entities.AppEntities.Cars
{
    public class Car : BaseEntity
    {
        public Car(int productionYear, string registrationCertificate, string carNumber)
        {
            ProductionYear = productionYear;
            RegistrationCertificate = registrationCertificate;
            CarNumber = carNumber;
        }
        public int ProductionYear { get; private set; }
        public string RegistrationCertificate { get; private set; }
        public string CarNumber { get; private set; }
        public CarBrand CarBrand { get; set; }
        public CarType CarType { get; set; }
        public CarColor CarColor { get; set; }
        public bool IsDeleted { get; private set; }

        

        public void DeleteCar()
        {
            IsDeleted = true;
        }
    }
}