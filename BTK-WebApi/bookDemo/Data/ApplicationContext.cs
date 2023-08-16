using bookDemo.Models;

namespace bookDemo.Data
{
    public static class ApplicationContext
    {
        //In-Memory Data
        public static List<Book> Books { get; set; }
        static ApplicationContext()
        {
            Books = new List<Book>()
            {
                new Book(){ Id=1, Title="Beyaz Zambak Ülkesinde", Price=100},
                new Book(){ Id=1, Title="Keloğlan Masalları", Price=120},
                new Book(){ Id=1, Title="Şahmat", Price=80},
            };
        }
    }
}
