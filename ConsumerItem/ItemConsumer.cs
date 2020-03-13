using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelLb;

namespace ConsumerItem
{
   public class ItemConsumer
    {
        public void Start()
        {
            Console.WriteLine("Start metode kald");
            List<Item> items = new List<Item>();
            itemlist = (List<Item>) await GetAllItemsAsync();
            foreach (var item in itemlist)
            {
                Console.WriteLine(item);
            }
        }

        public async Task<List<Item>> GetAllItemAsync()
        {

        }
    }
}
