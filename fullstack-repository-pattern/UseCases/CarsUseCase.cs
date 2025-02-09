using fullstack_repository_pattern.Domain;

namespace fullstack_repository_pattern 
{
    public class CarsUseCase
    {
        readonly CarService carService = new();
        public List<Car> GetGarageCars()
        {
            
            return carService.GetCars();
        }

        public bool AddCar(Car car, out List<string> errors)
        {

            return carService.AddCar(car, out errors);
        }
    }
}
   