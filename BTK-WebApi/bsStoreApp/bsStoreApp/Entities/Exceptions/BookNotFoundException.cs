namespace Entities.Exceptions
{
    //sealed ifadesi ile artık hiçbir sınıf BookNotFound'u miras alamaz
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id)
            : base($"The book with id : {id} could not found.")
        {
        }
    }
}
