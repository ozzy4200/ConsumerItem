using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ModelLb;
using Newtonsoft.Json;

namespace CoonsumerItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World ItemCostumer!");
            Console.ReadKey();
            string ItemWebApi = "Https://itemwebservicef20.azurewebsites.net/api/localitems";

            GetAndPrintItems(ItemWebApi);
            //PostNewItem();
            Console.ReadLine();
        }

        private static void GetAndPrintItems(string ItemWebApi)
        {
            Console.WriteLine("******GET ALL ITEMS*******");
            List<Item> items = new List<Item>();
            try
            {
                Task<List<Item>> callTask = Task.Run(() => GetItems(ItemWebApi));
                callTask.Wait();
                items = callTask.Result;
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(items[i].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<List<Item>> GetItems(string ItemWebApi)
        {
            using (HttpClient client = new HttpClient())
            {
                string eventsJsonString = await client.GetStringAsync(ItemWebApi);
                if (eventsJsonString != null)
                    return (List<Item>) JsonConvert.DeserializeObject(eventsJsonString, typeof(List<Item>));
                return null;

            }
        }

        public static void PostNewItem()
        {
            Console.WriteLine("******** POST A NEW ITEM");
            List<Item> items = new List<Item>();
            try
            {
                Task<List<Item>> callTask = Task.Run(() => PostItemHttpTask());
                callTask.Wait();
                items = callTask.Result;
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(items[i].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static async Task<List<Item>> PostItemHttpTask()
        {
            string ItemWebApiBase = "Https://itemwebservicef20.azurewebsites.net/api/localitems";
            Item newItem = new Item(11, "mmmm", "Low", 22);
            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ItemWebApiBase);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("api/localitems", content);

                Console.WriteLine("***** AN ITEM POSTED TO SERVICE **********");
                Console.WriteLine("**** RESPONCE IS" + response + "************");
                response.EnsureSuccessStatusCode();
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
            }

            Console.WriteLine("******** GET ALL ITEMS FOR  VERFICATION**********");
            string ItemWebApi = "Https://itemwebservicef20.azurewebsites.net/api/localitems";
            return await GetItems(ItemWebApi);
            //private static void GetAllItems(ItemConsumer getConsumer)
            //{
            //    try
            //    {
            //        Console.WriteLine("******************GET ALL*************");
            //        List<Item> items = new List<Item>();
            //        Task<List<Item>> callTask = Task.Run(() => .GetItemsHttpTask());
            //        callTask.Wait();
            //        items = callTask.Result;
            //        for (int i = 0; i < items.Count; i++)
            //        {
            //            Console.WriteLine(items[i].ToString());
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
        }
    }
}

