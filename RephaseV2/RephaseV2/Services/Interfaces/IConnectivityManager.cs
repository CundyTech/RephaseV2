namespace RephaseV2.Services.Interfaces
{
    public interface IConnectivityManager
    {
        /// <summary>
        /// Makes a http request to google domain to test if 
        /// device has internet connectivity.
        /// </summary>
        /// <returns>Either true or false based on whether device has internet connectivity.</returns>
        bool IsInternetAccessable();
    }
}