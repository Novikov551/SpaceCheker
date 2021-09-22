using Microsoft.EntityFrameworkCore;
using SpaceObjectsApi.Models;

namespace SpaceObjectsApi.IntegrationTests
{
    class DatabaseFixture
    {
        public SpaceObjectsContext Context { get; }

        public DatabaseFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SpaceObjectsContext>();
            var options = optionsBuilder
                    .UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123123123aZ")
                    .Options;
            Context = new SpaceObjectsContext(options);
        }

    }
}
