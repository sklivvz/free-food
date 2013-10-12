using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using PayPal;
using PayPal.Api.Payments;

namespace FreeFoodUs.Models
{
    public class PayPalFacade
    {
        public static Tuple<string, string, PaypalTransaction> Pay(decimal value, string forWhat, string okUrl, string failUrl)
        {
            var tokenCredential = new OAuthTokenCredential("AWJSwhCGxY2l-VceOlwTqTc2AJilK0AaJqSIOQQACAwj-KdfGG2gOMeH8s4U",
                "EKq-ehBlniQi5j5OBk-o81JbK4H2j51XlYK8bJfXQ52f2Wh4FVVoLx5HNH5y");
            var accessToken = tokenCredential.GetAccessToken();
            Payment createdPayment = new Payment
             {
                 intent = "sale",
                 transactions =
                     new List<Transaction>
                    {
                        new Transaction
                        {
                            amount = new Amount
                            {
                                total = value.ToString("0.00"), 
                                currency = "GBP"
                            },
                            description = forWhat
                        }
                    },
                 redirect_urls = new RedirectUrls
                 {
                     cancel_url = failUrl,
                     return_url = okUrl
                 },
                 payer = new Payer
                 {
                     payment_method = "paypal"
                 }

             }.Create(accessToken);
            var url = createdPayment.links.First(l => l.rel == "approval_url").href;
            var token = new Uri(url).ParseQueryString()["token"];
            return Tuple.Create(url, token, new PaypalTransaction { Price = value, Description = forWhat });
        }
    }

    public class PaypalTransaction
    {
        public decimal Price { get; set; }
        public string Description { get; set; }

        public void Record()
        {
            //
        }
    }
}