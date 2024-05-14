namespace Tennis.Repository
{
    public class UserRepository
    {
        public readonly TennisContext _context;
        public UserRepository(TennisContext context)
        {
            _context = context;
        }
    }
}
