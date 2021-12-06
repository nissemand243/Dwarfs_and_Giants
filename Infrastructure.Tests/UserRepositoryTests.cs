using Xunit;

namespace Infrastructure.Tests;

public class UserRepositoryTests : IDisposable
{
    private readonly DatabaseContext context;
    private readonly UserRepository repo;
    private bool disposed;

    [Fact]
    public void Test1()
    {

    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}