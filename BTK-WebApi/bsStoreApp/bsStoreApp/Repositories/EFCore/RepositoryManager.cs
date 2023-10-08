using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        //Lazy ifadesi ile sadece gerektiği, ihtiyaç duyulması durumunda newlenebilmesini sağlıyoruz
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_repositoryContext));
        }

        //Bunu da Yukarıdaki gibi Inject edebilirdik fakat IoC için sadece Manager
        //ekleme işini yapmak istediğimizden burada bu şekilde new'liyoruz
        public IBookRepository Book => _bookRepository.Value;

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
