using Tennis.Services.Interfaces;

namespace Tennis.Repository
{
    public class PersonRepository
    {
        public readonly TennisContext _context;
        public PersonRepository(TennisContext context)
        {
            _context = context;
        }
    }
}
