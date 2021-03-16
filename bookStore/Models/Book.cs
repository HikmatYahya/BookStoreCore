using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

       
        public string Title { get; set; }

       
        public string Descrption { get; set; }

        public string  Imageurl { get; set; }
        public Author Author { get; set; }
    }
}
