using System;
using SE_training.Repositories;
using Xunit;

namespace Infrastructure.Tests;

public class MaterialRepositoryTests : IDisposable
{

    private readonly DatabaseContext _context;
    private readonly MaterialRepository _repository;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Unimplemented()
    {

    }
}