using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRate.Models;
using ExchangeRate.Service;

namespace ExchangeRate.Controllers
{
    public class ExchangeRateController : Controller
    {
        private const string euro = "EUR";

        public async Task<IActionResult> Index(List<ExchangeRateModel> exchangeRateModelList)
        {

            return View(exchangeRateModelList);








            /*
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt");
            request.Method = HttpMethod.Get;
            //request.Headers.Add("x-api-key", "FpXDOs88bI1MzN9ScussK6QrmT9hNW1E3zlR2sHP");
            //request.Headers.Add("Content-Type", "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            Task<string> result = response.Content.ReadAsStringAsync();
            string result2 = response.Content.ReadAsStringAsync().Result;
            var statusCode = response.StatusCode;


            List<ExchangeRateModel> exchangeRateModelList = new List<ExchangeRateModel>();


            var test = await response.Content.ReadAsStreamAsync();
            StreamReader theStreamReader = new StreamReader(test);
            string line = null;
            int count = 0;
            while ((line = theStreamReader.ReadLine()) != null) {
                if (count > 1) {
                    exchangeRateModelList.Add(ExchangeRateService.ParseExchangeRate(line, count));
                }
                count++;
            }
            return View();*/
        }

        public async Task<IActionResult> Cz()
        {
            List<ExchangeRateModel> exchangeRateModelList = await ExchangeRateService.LoadExchangeRateCZAsync();
            
            return View("Index", exchangeRateModelList);
        }

        public async Task<IActionResult> Eur()
        {
            List<ExchangeRateModel> exchangeRateModelList = await ExchangeRateService.LoadExchangeRateByCodeAsync(euro);

            return View("Index", exchangeRateModelList);
        }
    }
}
