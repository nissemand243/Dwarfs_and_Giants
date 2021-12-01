namespace Repositories;

public class RatingRepository
{
    readonly DatabaseContext context;

    public RatingRepository(DatabaseContext _context)
    {
        context = _context;
    }

    public async void Put(RatingDTO rating)
    {
        context.Ratings.Add(rating);
        await context.SaveChangesAsync();
    }

    public IEnumerable<RatingDTO> Get(int materialID)
    {
        return from r in context.Ratings
               where r.materialID == materialID
               select new RatingDTO(r.materialID, r.userID, r.value);
    }

    public async void Post(int userID, int newValue)
    {
        var rating = await context.Ratings.FindAsync(userID);

        //TODO switch out a value (delete and put?)

        await context.SaveChangesAsync();
    }

    public async void Delete(int materialID, int userID)
    {
        var rating = await context.Ratings.FindAsync(materialID, userID);
        if (rating != null)
        {
            context.Ratings.Remove(rating);
        }
        await context.SaveChangesAsync();
    }
}