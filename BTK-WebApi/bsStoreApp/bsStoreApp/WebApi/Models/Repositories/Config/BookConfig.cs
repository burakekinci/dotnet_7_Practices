using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Models.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Hacivat ve Karagöz", Price = 75 },
                new Book { Id = 2, Title = "Satranç", Price = 175 },
                new Book { Id = 3, Title = "Zaman Tüneli", Price = 275 }
            );
        }
    }
}
