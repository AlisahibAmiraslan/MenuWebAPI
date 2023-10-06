using MenuWebAPI.Models;

namespace MenuWebAPI.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCarById(int id);
        bool CreateCar(Car car);
        bool CategoryExists(int id);
    }
}
