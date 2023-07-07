using GoldenHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenHour.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        
        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherHandler> GetSunsetSunrise(double longitude, double latitude)
        {
            var response = await _httpClient.GetAsync($"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}");

            Debug.WriteLine($"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&date=2023-06-07");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherHandler>(content);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Deserialization error: {ex}");
                    return null;
                }
            }
            else
            {
                Debug.WriteLine("NOT A GOOD RESPONSE");
                // TODO --> HANDLE ERROR
                return null;
            }
        }

        
    }
}
