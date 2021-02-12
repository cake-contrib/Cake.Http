using Cake.Core;
using NSubstitute;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Cake.Http.Tests.Unit
{
    public class CakeHttpClientHandlerTests
    {
        [Fact]
        [Trait(Traits.TestCategory, TestCategories.Unit)]
        public void Should_Add_Client_Certificates_From_Settings_To_Property()
        {
            //Given
            var cakeContext = Substitute.For<ICakeContext>();
            var settings = new HttpSettings
            {
                ClientCertificates =
                {
                    Substitute.For<X509Certificate2>(),
                    Substitute.For<X509Certificate2>()
                }
            };

            //When
            var handler = new CakeHttpClientHandler(cakeContext, settings);

            //Then
            Assert.Equal(settings.ClientCertificates[0], handler.ClientCertificates[0]);
            Assert.Equal(settings.ClientCertificates[1], handler.ClientCertificates[1]);
        }
    }
}
