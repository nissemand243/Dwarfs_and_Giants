namespace SE_training.Infrastructure;

public class UserRepository : IUserRepository
{
    readonly IDatabaseContext context;

    public UserRepository(IDatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status, UserDTO)> PutAsync(CreateUserDTO user)
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
                entity = new Student
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

        var details = new UserDTO(entity.UserId, entity.Name, entity.Email, entity.TypeOfToString());
        return (Created, details);
    }

    public async Task<UserDTO> GetAsync(int UserId)
    {
        var users = from u in context.Users
                    where u.UserId == UserId
                    select new UserDTO(u.UserId, u.Name, u.Email, u.TypeOfToString());

        return await users.FirstOrDefaultAsync();
    }

    public async Task<UserDTO> GetAsync(string name)
    {
        var users = from u in context.Users
                    where u.Name == name
                    select new UserDTO(u.UserId, u.Name, u.Email, u.TypeOfToString());

        return await users.FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyCollection<UserDTO>> GetAsync()
    {
        return (await context.Users
                             .Select(u => new UserDTO(u.UserId, u.Name, u.Email, u.TypeOfToString()))
                             .ToListAsync()).AsReadOnly();
    }


    public async Task<Status> DeleteAsync(int UserId)
    {
        var entity = await context.Users.FindAsync(UserId);

        if (entity == null) return NotFound;

        context.Users.Remove(entity);
        await context.SaveChangesAsync();

        return Deleted;
    }
}