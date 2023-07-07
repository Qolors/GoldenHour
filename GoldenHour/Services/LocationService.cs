using GoldenHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenHour.Services
{
    public class LocationService : ILocationService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        public async Task<LocationHandler> GetLocationAsync()
        {
            try
            {

                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    _isCheckingLocation = false;
                    return new LocationHandler(location.Longitude, location.Latitude, location.Altitude);
                }

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
                
            }

            return null;
        }

        public async Task<LocationHandler> GetCachedLocationAsync()
        {
            try
            {
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                {
                    _isCheckingLocation = false;
                    return new LocationHandler(location.Longitude, location.Latitude, location.Altitude);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Debug.WriteLine($"Error getting location: {fnsEx}");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Debug.WriteLine($"Error getting location: {fneEx}");
            }
            catch (PermissionException pEx)
            {
                Debug.WriteLine($"Error getting location: {pEx}");
            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine($"Error getting location: {ex}");
            }

            return null;
        }
    }
}
