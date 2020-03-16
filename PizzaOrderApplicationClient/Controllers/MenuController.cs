using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaOrderApplication.Core.ValueObjects.Result;
using PizzaOrderApplicationClient.Models;

namespace PizzaOrderApplicationClient.Controllers
{
    public class MenuController : Controller
    {
        private readonly INotifier notifier;
        string OrderAPIUrl = "http://localhost:56495/";

        public MenuController(INotifier notifier)
        {
            this.notifier = notifier;
        }

        // GET: Contact
        public async Task<ActionResult> Index()
        {
            IEnumerable<ProductResult> Contactslist = null;
            using (
                
                var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(OrderAPIUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("api/Product/GetMenu");

                if (Response.IsSuccessStatusCode)
                {
                    var ResultSet = Response.Content.ReadAsStringAsync().Result;
                    Contactslist = JsonConvert.DeserializeObject<List<ProductResult>>(ResultSet);
                }


                return View(Contactslist);
            }
        }

        internal static HttpRequestMessage CreateRequest<TRequest>(HttpMethod verb, Uri uri, TRequest requestContent)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = uri,
                Method = verb
            };

            string content = JsonConvert.SerializeObject(requestContent);
            request.Content = new StringContent(content);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { }
            base.Dispose(disposing);
        }
    }

}