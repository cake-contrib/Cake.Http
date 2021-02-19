using Cake.Core;
using Cake.Http.Tests.Fixtures;
using System;
using Xunit;

namespace Cake.Http.Tests.Unit
{
    public sealed class HttpClientAliasesTests
    {
        /// <summary>
        /// Uses jsonplaceholder website as a place to run these as integration tests
        /// </summary>
        private const string RootAddress = "https://jsonplaceholder.typicode.com";

        [Collection(Traits.CakeContextCollection)]
        public sealed class TheHttpGetMethod
        {
            private readonly ICakeContext _Context;

            public TheHttpGetMethod(CakeContextFixture fixture)
            {
                _Context = fixture;
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpGet_Should_Throw_On_Null_Or_Empty_Context_Parameter()
            {
                //Given
                ICakeContext context = null;
                string address = RootAddress;
                HttpSettings settings = new HttpSettings();

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpGetAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(context));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpGet_Should_Throw_On_Null_Or_Empty_Address_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = null;

                //When
                address = null;
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpGetAsByteArray(context, address, settings));
                address = "";
                var emptyRecord = Record.Exception(() => HttpClientAliases.HttpGetAsByteArray(context, address, settings));
                address = "     ";
                var spaceRecord = Record.Exception(() => HttpClientAliases.HttpGetAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(address));
                CakeAssert.IsArgumentNullException(emptyRecord, nameof(address));
                CakeAssert.IsArgumentNullException(spaceRecord, nameof(address));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpGet_Should_Throw_On_Null_Settings_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                string address = RootAddress;
                HttpSettings settings = null;

                //When
                var record = Record.Exception(() => HttpClientAliases.HttpGetAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(record, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpGet_Should_Return_Json_Result()
            {
                //Given
                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts/1";
                HttpSettings settings = new HttpSettings();

                //When
                var actual = HttpClientAliases.HttpGet(context, address);

                //Then
                var expected = "{\r\n  \"userId\": 1,\r\n  \"id\": 1,\r\n  \"title\": \"sunt aut facere repellat provident occaecati excepturi optio reprehenderit\",\r\n  \"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\"\r\n}";

                Assert.NotNull(actual);
                Assert.Equal(expected, actual, ignoreCase: true, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
            }
        }

        [Collection(Traits.CakeContextCollection)]
        public sealed class TheHttpPostMethod
        {
            private readonly ICakeContext _Context;

            public TheHttpPostMethod(CakeContextFixture fixture)
            {
                _Context = fixture;
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPost_Should_Throw_On_Null_Or_Empty_Context_Parameter()
            {
                //Given
                ICakeContext context = null;
                string address = RootAddress;
                HttpSettings settings = new HttpSettings();

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpPostAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(context));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPost_Should_Throw_On_Null_Or_Empty_Address_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = null;

                //When
                address = null;
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpPostAsByteArray(context, address, settings));
                address = "";
                var emptyRecord = Record.Exception(() => HttpClientAliases.HttpPostAsByteArray(context, address, settings));
                address = "     ";
                var spaceRecord = Record.Exception(() => HttpClientAliases.HttpPostAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(address));
                CakeAssert.IsArgumentNullException(emptyRecord, nameof(address));
                CakeAssert.IsArgumentNullException(spaceRecord, nameof(address));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPost_Should_Throw_On_Null_Settings_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                string address = RootAddress;
                HttpSettings settings = null;

                //When
                var record = Record.Exception(() => HttpClientAliases.HttpPostAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(record, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpPost_Should_Post_And_Return_Json_Result()
            {
                //Given
                var postData = "{\r\n    title: 'foo',\r\n    body: 'bar',\r\n    userId: 1\r\n  }";

                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts";
                HttpSettings settings = new HttpSettings();

                settings.SetRequestBody(postData);

                //When
                var actual = HttpClientAliases.HttpPost(context, address, settings);

                //Then
                var expected = "{\n  \"id\": 101\n}";

                Assert.NotNull(actual);
                Assert.Equal(expected, actual, ignoreCase: true, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
            }
        }

        [Collection(Traits.CakeContextCollection)]
        public sealed class TheHttpPutMethod
        {
            private readonly ICakeContext _Context;

            public TheHttpPutMethod(CakeContextFixture fixture)
            {
                _Context = fixture;
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPut_Should_Throw_On_Null_Or_Empty_Context_Parameter()
            {
                //Given
                ICakeContext context = null;
                string address = RootAddress;
                HttpSettings settings = new HttpSettings();

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpPutAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(context));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPut_Should_Throw_On_Null_Or_Empty_Address_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = null;

                //When
                address = null;
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpPutAsByteArray(context, address, settings));
                address = "";
                var emptyRecord = Record.Exception(() => HttpClientAliases.HttpPutAsByteArray(context, address, settings));
                address = "     ";
                var spaceRecord = Record.Exception(() => HttpClientAliases.HttpPutAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(address));
                CakeAssert.IsArgumentNullException(emptyRecord, nameof(address));
                CakeAssert.IsArgumentNullException(spaceRecord, nameof(address));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPut_Should_Throw_On_Null_Settings_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                string address = RootAddress;
                HttpSettings settings = null;

                //When
                var record = Record.Exception(() => HttpClientAliases.HttpGetAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(record, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpPut_Should_Put_And_Return_Json_Result()
            {
                //Given
                var putData = "{\r\n    title: 'foo',\r\n    body: 'bar',\r\n    userId: 1\r\n  }";

                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts/1";
                HttpSettings settings = new HttpSettings();

                settings.SetRequestBody(putData);

                //When
                var actual = HttpClientAliases.HttpPut(context, address, settings);

                //Then
                var expected = "{\n  \"id\": 1\n}";

                Assert.NotNull(actual);
                Assert.Equal(expected, actual, ignoreCase: true, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
            }

        }

        [Collection(Traits.CakeContextCollection)]
        public sealed class TheHttpPatchMethod
        {
            private readonly ICakeContext _Context;

            public TheHttpPatchMethod(CakeContextFixture fixture)
            {
                _Context = fixture;
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPatch_Should_Throw_On_Null_Or_Empty_Context_Parameter()
            {
                //Given
                ICakeContext context = null;
                string address = RootAddress;
                HttpSettings settings = new HttpSettings();

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpPatchAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(context));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPatch_Should_Throw_On_Null_Or_Empty_Address_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = null;

                //When
                address = null;
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpPatchAsByteArray(context, address, settings));
                address = "";
                var emptyRecord = Record.Exception(() => HttpClientAliases.HttpPatchAsByteArray(context, address, settings));
                address = "     ";
                var spaceRecord = Record.Exception(() => HttpClientAliases.HttpPatchAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(address));
                CakeAssert.IsArgumentNullException(emptyRecord, nameof(address));
                CakeAssert.IsArgumentNullException(spaceRecord, nameof(address));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpPatch_Should_Throw_On_Null_Settings_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                string address = RootAddress;
                HttpSettings settings = null;

                //When
                var record = Record.Exception(() => HttpClientAliases.HttpPatchAsByteArray(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(record, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpPatch_Should_Patch_And_Return_Json_Result()
            {
                //Given
                var patchData = "{\r\n    title: 'foo',\r\n    body: 'bar',\r\n    userId: 1\r\n  }";

                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts/1";
                HttpSettings settings = new HttpSettings();

                settings.SetRequestBody(patchData);

                //When
                var actual = HttpClientAliases.HttpPatch(context, address, settings);

                //Then
                var expected = "{\n  \"userId\": 1,\n  \"id\": 1,\n  \"title\": \"sunt aut facere repellat provident occaecati excepturi optio reprehenderit\",\n  \"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\"\n}";

                Assert.NotNull(actual);
                Assert.Equal(expected, actual, ignoreCase: true, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
            }

        }

        [Collection(Traits.CakeContextCollection)]
        public sealed class TheHttpDeleteMethod
        {
            private readonly ICakeContext _Context;

            public TheHttpDeleteMethod(CakeContextFixture fixture)
            {
                _Context = fixture;
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpDelete_Should_Throw_On_Null_Or_Empty_Context_Parameter()
            {
                //Given
                ICakeContext context = null;
                string address = RootAddress;
                HttpSettings settings = new HttpSettings();

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpDelete(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(context));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpDelete_Should_Throw_On_Null_Or_Empty_Address_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = null;

                //When
                address = null;
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpDelete(context, address, settings));
                address = "";
                var emptyRecord = Record.Exception(() => HttpClientAliases.HttpDelete(context, address, settings));
                address = "     ";
                var spaceRecord = Record.Exception(() => HttpClientAliases.HttpDelete(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(address));
                CakeAssert.IsArgumentNullException(emptyRecord, nameof(address));
                CakeAssert.IsArgumentNullException(spaceRecord, nameof(address));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpDelete_Should_Throw_On_Null_Settings_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                string address = RootAddress;
                HttpSettings settings = null;

                //When
                var record = Record.Exception(() => HttpClientAliases.HttpDelete(context, address, settings));

                //Then
                CakeAssert.IsArgumentNullException(record, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpDelete_Should_Delete_Return_Void()
            {
                //Given
                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts/1";
                HttpSettings settings = new HttpSettings() { EnsureSuccessStatusCode = true };

                //When
                HttpClientAliases.HttpDelete(context, address, settings);

                //Then
                //???
            }
        }

        [Collection(Traits.CakeContextCollection)]
        public sealed class TheHttpSendMethod
        {
            private readonly ICakeContext _Context;

            public TheHttpSendMethod(CakeContextFixture fixture)
            {
                _Context = fixture;
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpSend_Should_Throw_On_Null_Or_Empty_Context_Parameter()
            {
                //Given
                ICakeContext context = null;
                string address = RootAddress;
                string httpMethod = "POST";
                HttpSettings settings = new HttpSettings();

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpSend(context, address, httpMethod, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(context));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpSend_Should_Throw_On_Null_Or_Empty_Address_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = null;
                string httpMethod = "POST";

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpSend(context, address, httpMethod, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(address));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpSend_Should_Throw_On_Null_Or_Empty_HttpMethod_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = RootAddress;
                string httpMethod = null;

                //When
                var nullRecord = Record.Exception(() => HttpClientAliases.HttpSend(context, address, httpMethod, settings));

                //Then
                CakeAssert.IsArgumentNullException(nullRecord, nameof(httpMethod));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void HttpSend_Should_Throw_On_Null_Settings_Parameter()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = null;
                string address = RootAddress;
                string httpMethod = "POST";

                //When
                var record = Record.Exception(() => HttpClientAliases.HttpSend(context, address, httpMethod, settings));

                //Then
                CakeAssert.IsArgumentNullException(record, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpSend_Should_Post_And_Return_Json_Result()
            {
                //Given
                ICakeContext context = _Context;
                HttpSettings settings = new HttpSettings();
                string address = $"{ RootAddress }/posts";
                string httpMethod = "POST";

                settings.SetRequestBody("{ \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1}");

                //When
                var actual = HttpClientAliases.HttpSend(context, address, httpMethod, settings);

                //Then
                var expected = "{\n  \"id\": 101\n}";

                Assert.NotNull(actual);
                Assert.Equal(expected, actual, ignoreCase: true, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpSend_Should_Throw_Exception_If_Request_Times_Out()
            {
                //Given
                var settings = new HttpSettings();
                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts";
                //When
                settings
                    .SetTimeout(TimeSpan.FromMilliseconds(1));

                var record = Record.Exception(() => _Context.HttpGet(address, settings));
                CakeAssert.IsExceptionWithMessage<TimeoutException>(record.InnerException, "The operation was canceled.");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Integration)]
            public void HttpSend_Should_Get_And_Return_Json_Result()
            {
                //Given
                var settings = new HttpSettings();
                ICakeContext context = _Context;
                string address = $"{ RootAddress }/posts/1";
                //When
                settings
                    .SetTimeout(TimeSpan.FromSeconds(100));

                var actual = _Context.HttpGet(address, settings);

                //Then
                var expected = "{\r\n  \"userId\": 1,\r\n  \"id\": 1,\r\n  \"title\": \"sunt aut facere repellat provident occaecati excepturi optio reprehenderit\",\r\n  \"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\"\r\n}";

                Assert.NotNull(actual);
                Assert.Equal(expected, actual, ignoreCase: true, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
            }
        }
    }
}
