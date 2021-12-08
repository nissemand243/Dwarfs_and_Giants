namespace SE_training.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, UserDTO? user)> PutAsync(CreateUserDTO user)
    {
        User entity;
        switch (user.Type)
        {
            case "Student":
                entity = new Student() {
                    Name = user.Name,
                    Email = user.Email
                };
                break;
            case "Teacher":
                entity = new Teacher() {
                    Name = user.Name,
                    Email = user.Email
                };
                break;
            default:
                return (BadRequest, null);
        }
        
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        var details = new UserDTO(entity.Id, entity.Name, entity.Email, entity.TypeOfToString());
        return (Created, details);
    }

    public async Task<UserDTO?> GetAsync(int userId)
    {
        var user = from u in _context.Users
                    where u.Id == userId
                    select new UserDTO(u.Id, u.Name, u.Email, u.TypeOfToString());

        return await user.FirstOrDefaultAsync();
    }

    public async Task<UserDTO?> GetAsync(string name)
    {
        var users = from u in _context.Users
                    where u.Name == name
                    select new UserDTO(u.Id, u.Name, u.Email, u.TypeOfToString());

        return await users.FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyCollection<UserDTO>> GetAsync()
    {
         var users = (await _context.Users
                             .Select(u => new UserDTO(u.Id, u.Name, u.Email, u.TypeOfToString()))
                             .ToListAsync()).AsReadOnly();

        return users;
    }


    public async Task<Status> DeleteAsync(int userId)
    {
        var entity = await _context.Users.FindAsync(userId);

        if (entity == null) return NotFound;

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }
}