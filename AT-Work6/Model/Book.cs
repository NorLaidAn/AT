using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Work6.Model
{
    internal class Book
    {
        public Guid? id { get; set; }

        public string? title { get; set; }

        public string? author { get; set; }

        public string? isbn { get; set; }

        public DateTime publishedDate { get; set; }
    }
}
