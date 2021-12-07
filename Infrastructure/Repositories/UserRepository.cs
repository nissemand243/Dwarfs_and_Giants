namespace SE_training.Infrastructure;

public class UserRepository : IUserRepository
{
    readonly DatabaseContext context;

    public UserRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status status, UserDTO user)> PutAsync(CreateUserDTO user)
    {
        User entity;
        switch (user.Type)
        {
            case "Student":
                entity = new Student
                {
                    Name = user.Name,
                    Email = user.Email
                };
                break;
            case "Teacher":
                entity = new Teacher
                {
                    Name = user.Name,
                    Email = user.Email
                };
                break;
            default:
                return (BadRequest, null);
        }
        
        context.Users.Add(entity);
        await context.SaveChangesAsync();

        var details = new UserDTO(entity.Id, entity.Name, entity.Email, entity.TypeOfToString());
        return (Created, details);
    }

    public async Task<UserDTO> GetAsync(int userId)
    {
        var users = from u in context.Users
                    where u.Id == userId
                    select new UserDTO(u.Id, u.Name, u.Email, u.TypeOfToString());

        return await users.FirstOrDefaultAsync();
    }

    public async Task<UserDTO> GetAsync(string name)
    {
        var users = from u in context.Users
                    where u.Name == name
                    select new UserDTO(u.Id, u.Name, u.Email, u.TypeOfToString());

        return await users.FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyCollection<UserDTO>> GetAsync()
    {
        return (await context.Users
                             .Select(u => new UserDTO(u.Id, u.Name, u.Email, u.TypeOfToString()))
                             .ToListAsync()).AsReadOnly();
    }


    public async Task<Status> DeleteAsync(int userId)
    {
        var entity = await context.Users.FindAsync(userId);

        if (entity == null) return NotFound;

        context.Users.Remove(entity);
        await context.SaveChangesAsync();

        return Deleted;
    }
}