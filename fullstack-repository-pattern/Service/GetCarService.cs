using System.ComponentModel.DataAnnotations;
using fullstack_repository_pattern.Domain;

namespace fullstack_repository_pattern
{
    public class CarService 
    {
        private List<Car> cars = new()
        {
            new Car { Color = "Red", Make = "Ferrari", Model = "Italia", Year = 2012 },
            new Car { Color = "Black", Make = "Chevrolet", Model = "Camaro Z", Year = 2020 },
            new Car { Color = "Black", Make = "Audi", Model = "TT", Year = 2016 },
        };

        public List<Car> GetCars()
        {

            return cars;
        }

        public bool AddCar(Car car, out List<string> errors)
        {   
            errors = new List<string>();
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(car);

            // Valida o objeto Car
            bool isValid = Validator.TryValidateObject(car, context, validationResults, true);

            if (!isValid)
            {
                errors.AddRange(validationResults.Select(v => v.ErrorMessage!));
                return false;
            }

            cars.Add(car);
            return true;
        }
    }
}
