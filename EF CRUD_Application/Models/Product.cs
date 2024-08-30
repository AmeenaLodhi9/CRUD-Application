using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CRUD_Application.Models
{
    public class Product
    {
        public int Id { get; set; }  // Maps to prod_id
        public string Name { get; set; }  // Maps to prod_name
        public int Price { get; set; }   // Maps to prod_price
    }
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
