namespace SE_training.Infrastructure;

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
            Name = user.Name,
            Email = user.Email
        };
        context.Users.Add(entity);
        await context.SaveChangesAsync();

        var details = new UserDTO(entity.Id, entity.Name, entity.Email, null); //TODO exstract the subclass
        return (Created, details);
    }

    public async Task<UserDTO> GetAsync(int UserId)
    {
        var users = from u in context.Users
                    where u.Id == UserId
                    select new UserDTO(u.Id, u.Name, u.Email, null); //TODO extract the subclass

        return await users.FirstOrDefaultAsync();
    }

    public async Task<UserDTO> GetAsync(string name)
    {
        var users = from u in context.Users
                    where u.Name == name
                    select new UserDTO(u.Id, u.Name, u.Email, null); //TODO extract the subclass

        return await users.FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyCollection<UserDTO>> GetAsync()
    {
        return (await context.Users
                             .Select(u => new UserDTO(u.Id, u.Name, u.Email, null)) //TODO extract the subclass)
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