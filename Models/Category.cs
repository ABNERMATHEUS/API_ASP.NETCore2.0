using API.NETcore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.NETcore2
{
    public class Category


    {

        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}
