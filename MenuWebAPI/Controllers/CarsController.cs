using AutoMapper;
using MenuWebAPI.DTOs;
using MenuWebAPI.Interfaces;
using MenuWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Car>> GetCars()
        {
            return Ok(_carRepository.GetCars());
        }

        [HttpGet("carId")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int carId)
        {
            if(!_carRepository.CategoryExists(carId))
            {
                return NotFound($"{carId} is not Found");
            }

            var car = _carRepository.GetCarById(carId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult <List<Car>> CreateCar(Car newCar) 
        {
            try
            {
                _carRepository.CreateCar(newCar);

                return Ok(newCar);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

         
        }
    }
}
