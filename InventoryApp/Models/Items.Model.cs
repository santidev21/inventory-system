using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    public class ItemsModel
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int quantityItem { get; set; }
        public string brandItem { get; set; }
        public string categoryItem { get; set; }
        public string createdByItem { get; set; }
    }
}
