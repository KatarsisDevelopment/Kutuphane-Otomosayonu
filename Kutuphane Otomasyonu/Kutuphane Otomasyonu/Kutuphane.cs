using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyonu
{
    public class Book
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public int NumberBooks { get; set; }
        public int NumberOfBorrowed { get; set; }

    }
    class Kutuphane
    {
        static void Main(string[] args)
        {
            Book book = new Book
            {
                ISBN = 1,
                Title = "Birinci",
                Writer = "Yazar",
                NumberBooks = 1,
                NumberOfBorrowed = 2,
            };
            Console.WriteLine(book.Title);
            Console.ReadLine();
        }
    }
}
