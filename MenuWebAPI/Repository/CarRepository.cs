using MenuWebAPI.Interfaces;
using MenuWebAPI.Models;

namespace MenuWebAPI.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return Save();
        }

        public ICollection<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool CategoryExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
