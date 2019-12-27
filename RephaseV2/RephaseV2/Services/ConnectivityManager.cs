using System.Net;
using RephaseV2.Services.Interfaces;

namespace RephaseV2.Services
{
    public class ConnectivityManager : IConnectivityManager
    {
        /// <summary>
        /// Makes a http request to google domain to test if 
        /// device has internet connectivity.
        /// </summary>
        /// <returns>Either true or false based on whether device has internet connectivity.</returns>
        public bool IsInternetAccessable()
        {
            string CheckUrl = "http://google.com";

            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                httpRequest.Timeout = 5000;

                WebResponse httptResponse = httpRequest.GetResponse();
                httptResponse.Close();

                return true;
            }
            catch (WebException)
            {
                return false;
            }

        }
    }
}
