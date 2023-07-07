using GoldenHour.Services.Interfaces;

namespace GoldenHour.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string sunRiseTime;
    [ObservableProperty]
    private string sunSetTime;

    private readonly IWeatherService _weatherService;
    private readonly ILocationService _locationService;

    public HomeViewModel(IWeatherService weatherService, ILocationService locationService)
    {
        _weatherService = weatherService;
        _locationService = locationService;
        SunRiseTime = "OK";
        SunSetTime = "OK";
    }

    public async Task LoadUpData()
    {
        var location = await _locationService.GetLocationAsync();
        var weather = await _weatherService.GetSunsetSunrise(location.Longitude, location.Latitude);

        if (weather.Status == "OK")
        {
            SunRiseTime = GetLocalTime(weather.Results.Sunrise);
            SunSetTime = GetLocalTime(weather.Results.Sunset);
        }
    }

    public string GetLocalTime(string responseDate)
    {
        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        DateTime sunriseUtc = DateTime.Parse(responseDate);
        DateTime sunriseLocal = TimeZoneInfo.ConvertTimeFromUtc(sunriseUtc, cstZone);

        return sunriseLocal.ToShortTimeString();
    }

}
