using Cielo.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace Cielo
{
    public class CieloClient
    {
        private string URL = "https://apisandbox.cieloecommerce.cielo.com.br/1/";

        public string MerchantId { get; protected set; }
        public string MerchantKey { get; protected set; }

        public CieloClient(string merchantId, string merchantKey)
        {
            AlterarMerchatId(merchantId);
            AlterarMerchantKey(merchantKey);
        }

        public void AlterarMerchatId(string merchantId)
        {
            if (string.IsNullOrEmpty(merchantId))
            {
                throw new ArgumentException("O campo MerchantId é obrigatório");
            }

            this.MerchantId = merchantId;
        }

        public void AlterarMerchantKey(string merchantKey)
        {
            if (string.IsNullOrEmpty(merchantKey))
            {
                throw new ArgumentException("O campo MerchantId é obrigatório");
            }

            this.MerchantKey = merchantKey;
        }

        public async Task<CieloSale> CreateSale(CieloSale cieloSale)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);

                var data = JsonConvert.SerializeObject(cieloSale);

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("sales", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error creating a sale");
                }

                cieloSale = JsonConvert.DeserializeObject<CieloSale>(await response.Content.ReadAsStringAsync());

                return cieloSale;
            }
        }
    }
}
