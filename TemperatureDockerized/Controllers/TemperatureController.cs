using Microsoft.AspNetCore.Mvc;
using KataTemperature;

namespace TemperatureDockerized.Controllers;


[ApiController]
public class TemperatureController : ControllerBase
{
    private readonly char[] spearator = { '+'};

    // GET
    [HttpGet("api/Temps")]
    public IActionResult Temps([FromQuery] string Temperature)
    {
        try
        {
            String[] strlist = Temperature.Split(spearator);
            Temperature aNewTemp = null;  
        
            switch (strlist[1].ToLower())
            {
                case "f":
                    aNewTemp= new Temperature(Convert.ToInt32(strlist[0]), TemperatureScale.Fahrenheit);
                    break;
                case "c":
                    aNewTemp = new Temperature(Convert.ToInt32(strlist[0]), TemperatureScale.Celsius); 
                    break;
                case "k":
                    aNewTemp = new Temperature(Convert.ToInt32(strlist[0]), TemperatureScale.Kelvin);  
                    break;
                default: 
                    return BadRequest("La escala no es valida"); 
                    break;
            }
        
        
            switch (strlist[3].ToLower())
            {
                case "f":
                    return Ok(aNewTemp.ToFahrenheit().Value + " " + aNewTemp.ToFahrenheit().Scale ); 
                    break;
                case "c":
                    return Ok(aNewTemp.toCelsius().Value+ " " + aNewTemp.ToFahrenheit().Scale);  
                    break;
                case "k":
                    return Ok(aNewTemp.toKelvin().Value+ " " + aNewTemp.ToFahrenheit().Scale);   
                    break;
                default:
                    return BadRequest("La escala no es valida");
                    break;
            }
        }
        catch ( System.FormatException e)
        {
            return BadRequest("El formato del valor a convertir no es valido");
            throw;
        }
         
    }
}