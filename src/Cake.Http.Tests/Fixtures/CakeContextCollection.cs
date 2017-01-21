using Xunit;

namespace Cake.Http.Tests.Fixtures
{
    [CollectionDefinition(Traits.CakeContextCollection)]
    public class CakeContextCollection : ICollectionFixture<CakeContextFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<CakeContextFixture> interfaces.
    }
}
