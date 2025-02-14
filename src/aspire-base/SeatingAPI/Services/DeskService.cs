
public class DeskService
{
    private readonly AppDbContext _context;
    public DeskService(AppDbContext context)
    {
        _context = context;
    }
}