using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExchangeRate.Models;

namespace ExchangeRate.Service
{
    public static class ExchangeRateService
    {
        private static decimal exchangeRate;

        public static async Task<List<ExchangeRateModel>> LoadExchangeRateByCodeAsync(string code)
        {
            List<ExchangeRateModel> exchangeRateCZModelList = await LoadExchangeRateCZAsync();
            List<ExchangeRateModel> result = findAndDeleteExchangeRateByCode(exchangeRateCZModelList, code);
            result = addCzAndSort(result);
            result = chandeRateAndAmount(result);
            
            return result;
        }

        private static List<ExchangeRateModel> findAndDeleteExchangeRateByCode(List<ExchangeRateModel> exchangeRateModelList, string code)
        {
            foreach (var item in exchangeRateModelList)
            {
                if (item.code == code)
                {
                    exchangeRate = item.rate;
                    exchangeRateModelList.Remove(item);
                    break;
                }
            }
            return exchangeRateModelList;
        }

        private static List<ExchangeRateModel> chandeRateAndAmount(List<ExchangeRateModel> exchangeRateModelList)
        {
            foreach (var item in exchangeRateModelList)
            {
                item.rate /= exchangeRate;

                while (item.rate < 1)
                {
                    item.amount *= 10;
                    item.rate *= 10;
                }
                while (item.rate > 100)
                {
                    item.amount /= 10;
                    item.rate /= 10;
                }
            }
            return exchangeRateModelList;
        }

        private static List<ExchangeRateModel> addCzAndSort(List<ExchangeRateModel> exchangeRateModelList)
        {
            if (exchangeRateModelList.Count == 0)
            {

            }


            int id = exchangeRateModelList[exchangeRateModelList.Count - 1].id + 1;
            exchangeRateModelList.Add(new ExchangeRateModel(id, "Česko", "koruna", 1, "CZK", 1));

            exchangeRateModelList.Sort(delegate (ExchangeRateModel x, ExchangeRateModel y)
            {
                return x.country.CompareTo(y.country);
            });

            return exchangeRateModelList;
        }

        public static async Task<List<ExchangeRateModel>> LoadExchangeRateCZAsync()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt");
            request.Method = HttpMethod.Get;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            Stream stream = await response.Content.ReadAsStreamAsync();
            return parseExchangeRateFromStream(stream);
        }

        private static List<ExchangeRateModel> parseExchangeRateFromStream(Stream stream)
        {
            List<ExchangeRateModel> result = new List<ExchangeRateModel>();
            StreamReader streamReader = new StreamReader(stream);
            string line = null;
            int count = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (count > 1)
                {
                    result.Add(parseExchangeRateLine(line, count));
                }
                count++;
            }
            return result;
        }

        private static ExchangeRateModel parseExchangeRateLine(string exchangeRateStr, int id)
        {
            string[] exchangeRateSplit = exchangeRateStr.Split('|');
            string country = exchangeRateSplit[0];
            string currency = exchangeRateSplit[1];
            int amount = int.Parse(exchangeRateSplit[2]);
            string code = exchangeRateSplit[3];
            decimal rate = decimal.Parse(exchangeRateSplit[4]);

            return new ExchangeRateModel(id, country, currency, amount, code, rate);
        }
    }
}
