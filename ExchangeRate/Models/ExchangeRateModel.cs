using System;

namespace ExchangeRate.Models
{
    public class ExchangeRateModel
    {
        public int id { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public int amount { get; set; }
        public string code { get; set; }
        public decimal rate { get; set; }

        public ExchangeRateModel(int id, string country, string currency, int amount, string code, decimal rate) {
            this.id = id;
            this.country = country;
            this.currency = currency;
            this.amount = amount;
            this.code = code;
            this.rate = rate;
        }
    }
}