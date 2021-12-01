namespace Repositories;

public class UserRepository
{
    readonly DatabaseContext context;

    public UserRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async void Put(UserDTO user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<UserDTO?> GetByUsername(string username)
    {
        var users = from u in context.Users
                    where u.userName == username
                    select new UserDTO(u.userID, u.userName, u.email, u.type);

        return await users.FirstOrDefaultAsync();
    }

    public async Task<UserDTO?> GetByEmail(string email)
    {
        var users = from u in context.Users
                    where u.email == email
                    select new UserDTO(u.userID, u.userName, u.email, u.type);

        return await users.FirstOrDefaultAsync();
    }

    public async void Delete(int userID)
    {
        var user = await context.Users.FindAsync(userID);
        if (user == null) return;
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}