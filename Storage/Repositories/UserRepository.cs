namespace SE_training.Repositories;

public class UserRepository : IUserRepository
{
    readonly DatabaseContext context;

    public UserRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async Task<(Status, UserDTO)> PutAsync(CreateUserDTO user)
    {
        var entity = new User
        {
            Name = user.name,
            Email = user.email
        };
        context.Users.Add(entity);
        await context.SaveChangesAsync();

        var details = new UserDTO(entity.UserId, entity.Name, entity.Email, null); //TODO exstract the subclass
        return (Created, details);
    }

    public async Task<UserDTO> GetAsync(int userId)
    {
        var users = from u in context.Users
                    where u.UserId == userId
                    select new UserDTO(u.UserId, u.Name, u.Email, null); //TODO extract the subclass

        return await users.FirstOrDefaultAsync();
    }

    public async Task<UserDTO> GetAsync(string name)
    {
        var users = from u in context.Users
                    where u.Name == name
                    select new UserDTO(u.UserId, u.Name, u.Email, null); //TODO extract the subclass

        return await users.FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyCollection<UserDTO>> GetAsync()
    {
        return (await context.Users
                             .Select(u => new UserDTO(u.UserId, u.Name, u.Email, null)) //TODO extract the subclass)
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