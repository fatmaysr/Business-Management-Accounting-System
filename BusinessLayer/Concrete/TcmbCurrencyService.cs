using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace BusinessLayer.Concrete
{
    public class TcmbRateDto
    {
        public string CurrencyCode { get; set; } // USD, EUR, GBP vb.
        public string CurrencyName { get; set; }
        public decimal ForexBuying { get; set; }  // Döviz Alış
        public decimal ForexSelling { get; set; } // Döviz Satış
    }

    public class TcmbCurrencyService
    {
        private const string TcmbXmlUrl = "https://www.tcmb.gov.tr/kurlar/today.xml";

        public List<TcmbRateDto> GetTodayRates()
        {
            var rateList = new List<TcmbRateDto>();

            try
            {
                XDocument xmlDoc = XDocument.Load(TcmbXmlUrl);

                // DÜZELTME: TCMB XML kök etiketi 'Tarih_Date' şeklindedir.
                var currencies = xmlDoc.Root?.Elements("Currency");

                if (currencies == null) return rateList;

                foreach (var element in currencies)
                {
                    string code = element.Attribute("CurrencyCode")?.Value;

                    // Takip etmek istediğimiz kurlar
                    if (code == "USD" || code == "EUR" || code == "GBP" || code == "CHF")
                    {
                        string buyingStr = element.Element("ForexBuying")?.Value;
                        string sellingStr = element.Element("ForexSelling")?.Value;

                        decimal buying = 0;
                        decimal selling = 0;

                        // TCMB XML'de ondalık ayırıcı noktadır (örn: 32.1450), en-US kültürüyle okuyoruz
                        var culture = CultureInfo.InvariantCulture;

                        if (!string.IsNullOrEmpty(buyingStr))
                            decimal.TryParse(buyingStr, NumberStyles.Any, culture, out buying);

                        if (!string.IsNullOrEmpty(sellingStr))
                            decimal.TryParse(sellingStr, NumberStyles.Any, culture, out selling);

                        rateList.Add(new TcmbRateDto
                        {
                            CurrencyCode = code,
                            CurrencyName = element.Element("Isim")?.Value,
                            ForexBuying = buying,
                            ForexSelling = selling
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TCMB Kur Çekme Hatası: " + ex.Message);
            }

            return rateList;
        }
    }
}