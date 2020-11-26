using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRate.Models;

namespace ExchangeRate.Service.Tests
{
    [TestClass()]
    public class ExchangeRateServiceTests
    {
        [TestMethod()]
        public async Task LoadExchangeRateCZAsyncTestAsync()
        {
            try
            {
                int count = 33;

                List<ExchangeRateModel> exchangeRateModelList = await ExchangeRateService.LoadExchangeRateCZAsync();

                Assert.IsNotNull(exchangeRateModelList);
                Assert.AreEqual(exchangeRateModelList.Count, count);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public async Task LoadExchangeRateByCodeAsyncTestAsync()
        {
            try
            {
                string code = "EUR";
                int count = 33;

                List<ExchangeRateModel> exchangeRateModelList = await ExchangeRateService.LoadExchangeRateByCodeAsync(code);

                Assert.IsNotNull(exchangeRateModelList);
                Assert.AreEqual(exchangeRateModelList.Count, count);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}