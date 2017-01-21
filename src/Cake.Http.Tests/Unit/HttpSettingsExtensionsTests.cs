using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cake.Http.Tests.Unit
{
    public sealed class HttpSettingsExtensionsTests
    {
        public sealed class TheAppendHeaderMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Settings_Parameter()
            {
                //Given                
                HttpSettings settings = null;
                string name = "Content-Type";
                string value = "applicaion/json";

                //When
                var record = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, name, value));

                //Then
                Assert.IsArgumentNullException(record, "settings");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Name_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string value = "applicaion/json";

                //When
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, null, value));
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, "", value));
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, "   ", value));

                //Then
                Assert.IsArgumentNullException(nullRecord, "name");
                Assert.IsArgumentNullException(emptyRecord, "name");
                Assert.IsArgumentNullException(spaceRecord, "name");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Value_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string name = "Content-Type";

                //When
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, name, null));
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, name, ""));
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.AppendHeader(settings, name, "   "));

                //Then
                Assert.IsArgumentNullException(nullRecord, "value");
                Assert.IsArgumentNullException(emptyRecord, "value");
                Assert.IsArgumentNullException(spaceRecord, "value");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Single_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();

                //When
                settings.AppendHeader("Content-Type", "application/json");

                //Then
                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Content-Type"));
                Assert.Equal(settings.Headers["Content-Type"], "application/json", StringComparer.OrdinalIgnoreCase);
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Replace_Existing_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings()
                {
                    Headers = new Dictionary<string, string>
                    {
                        ["Content-Type"] = "application/json",
                        ["Accept"] = "application/xml"
                    }
                };

                //When
                settings.AppendHeader("Content-Type", "text/xml");

                //Then
                Assert.Equal(settings.Headers["Content-Type"], "text/xml", StringComparer.OrdinalIgnoreCase);
            }
        }

        public sealed class TheAppendCookieMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Settings_Parameter()
            {
                //Given                
                HttpSettings settings = null;
                string name = "sessionid";
                string value = "1BA9481B-74C1-42B3-A1B9-0B914BAE0F05";

                //When
                var record = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));

                //Then
                Assert.IsArgumentNullException(record, "settings");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Name_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string name = null;
                string value = "1BA9481B-74C1-42B3-A1B9-0B914BAE0F05";

                //When
                name = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));
                name = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));
                name = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(name));
                Assert.IsArgumentNullException(emptyRecord, nameof(name));
                Assert.IsArgumentNullException(spaceRecord, nameof(name));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Value_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string name = "sessionid";
                string value = null;

                //When
                value = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));
                value = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));
                value = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.AppendCookie(settings, name, value));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(value));
                Assert.IsArgumentNullException(emptyRecord, nameof(value));
                Assert.IsArgumentNullException(spaceRecord, nameof(value));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Single_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string name = "sessionid";
                string value = "1BA9481B-74C1-42B3-A1B9-0B914BAE0F05";

                //When
                settings.AppendCookie(name, value);

                //Then
                Assert.NotNull(settings.Cookies);
                Assert.True(settings.Cookies.ContainsKey(name));
                Assert.Equal(settings.Cookies[name], value, StringComparer.OrdinalIgnoreCase);
            }
        }

        public sealed class TheSetAuthorizationMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Schema_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string parameter = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ0b3B0YWwuY29tIiwiZXhwIjoxNDI2NDIwODAwLCJodHRwOi8vdG9wdGFsLmNvbS9qd3RfY2xhaW1zL2lzX2FkbWluIjp0cnVlLCJjb21wYW55IjoiVG9wdGFsIiwiYXdlc29tZSI6dHJ1ZX0.yRQYnWzskCZUxPwaQupWkiUzKELZ49eM7oWxAQK_ZXw";

                //When
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetAuthorization(settings, null, parameter));
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetAuthorization(settings, "", parameter));
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetAuthorization(settings, "   ", parameter));

                //Then
                Assert.IsArgumentNullException(nullRecord, "schema");
                Assert.IsArgumentNullException(emptyRecord, "schema");
                Assert.IsArgumentNullException(spaceRecord, "schema");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Parameter_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string schema = "Bearer";

                //When
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetAuthorization(settings, schema, null));
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetAuthorization(settings, schema, ""));
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetAuthorization(settings, schema, "   "));

                //Then
                Assert.IsArgumentNullException(nullRecord, "parameter");
                Assert.IsArgumentNullException(emptyRecord, "parameter");
                Assert.IsArgumentNullException(spaceRecord, "parameter");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Authorization_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string schema = "Bearer";
                string parameter = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ0b3B0YWwuY29tIiwiZXhwIjoxNDI2NDIwODAwLCJodHRwOi8vdG9wdGFsLmNvbS9qd3RfY2xhaW1zL2lzX2FkbWluIjp0cnVlLCJjb21wYW55IjoiVG9wdGFsIiwiYXdlc29tZSI6dHJ1ZX0.yRQYnWzskCZUxPwaQupWkiUzKELZ49eM7oWxAQK_ZXw";

                //When
                settings.SetAuthorization(schema, parameter);

                //Then
                var expected = $"{schema} {parameter}";

                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Authorization"));
                Assert.Equal(settings.Headers["Authorization"], expected, StringComparer.OrdinalIgnoreCase);
            }
        }

        public sealed class TheUseBasicAuthorizationMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_UserName_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string userName = null;
                string password = "tiger";

                //When
                userName = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.UseBasicAuthorization(settings, userName, password));
                userName = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.UseBasicAuthorization(settings, userName, password));
                userName = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.UseBasicAuthorization(settings, userName, password));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(userName));
                Assert.IsArgumentNullException(emptyRecord, nameof(userName));
                Assert.IsArgumentNullException(spaceRecord, nameof(userName));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Password_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string userName = "scott";
                string password = null;

                //When
                password = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.UseBasicAuthorization(settings, userName, password));
                password = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.UseBasicAuthorization(settings, userName, password));
                password = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.UseBasicAuthorization(settings, userName, password));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(password));
                Assert.IsArgumentNullException(emptyRecord, nameof(password));
                Assert.IsArgumentNullException(spaceRecord, nameof(password));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Basic_Authorization_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string userName = "scott";
                string password = "tiger";

                //When
                settings.UseBasicAuthorization(userName, password);

                //Then
                var expected = "Basic c2NvdHQ6dGlnZXI=";

                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Authorization"));
                Assert.Equal(settings.Headers["Authorization"], expected, StringComparer.OrdinalIgnoreCase);
            }
        }

        public sealed class TheUseBearerAuthorizationMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Token_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string token = null;

                //When
                token = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.UseBearerAuthorization(settings, token));
                token = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.UseBearerAuthorization(settings, token));
                token = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.UseBearerAuthorization(settings, token));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(token));
                Assert.IsArgumentNullException(emptyRecord, nameof(token));
                Assert.IsArgumentNullException(spaceRecord, nameof(token));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Bearer_Authorization_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ0b3B0YWwuY29tIiwiZXhwIjoxNDI2NDIwODAwLCJodHRwOi8vdG9wdGFsLmNvbS9qd3RfY2xhaW1zL2lzX2FkbWluIjp0cnVlLCJjb21wYW55IjoiVG9wdGFsIiwiYXdlc29tZSI6dHJ1ZX0.yRQYnWzskCZUxPwaQupWkiUzKELZ49eM7oWxAQK_ZXw";

                //When
                settings.UseBearerAuthorization(token);

                //Then
                var expected = "Bearer " + token;

                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Authorization"));
                Assert.Equal(settings.Headers["Authorization"], expected);
            }
        }

        public sealed class TheSetContentTypeMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_ContentType_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string contentType = null;

                //When
                contentType = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetContentType(settings, contentType));
                contentType = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetContentType(settings, contentType));
                contentType = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetContentType(settings, contentType));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(contentType));
                Assert.IsArgumentNullException(emptyRecord, nameof(contentType));
                Assert.IsArgumentNullException(spaceRecord, nameof(contentType));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_ContentType_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string contentType = "application/json";

                //When
                settings.SetContentType(contentType);

                //Then
                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Content-Type"));
                Assert.Equal(settings.Headers["Content-Type"], contentType);
            }
        }

        public sealed class TheSetAcceptMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Accept_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string accept = null;

                //When
                accept = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetAccept(settings, accept));
                accept = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetAccept(settings, accept));
                accept = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetAccept(settings, accept));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(accept));
                Assert.IsArgumentNullException(emptyRecord, nameof(accept));
                Assert.IsArgumentNullException(spaceRecord, nameof(accept));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Accept_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string accept = "text/xml";

                //When
                settings.SetAccept(accept);

                //Then
                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Accept"));
                Assert.Equal(settings.Headers["Accept"], accept);
            }
        }

        public sealed class TheSetNoCacheMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_CacheControl_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();

                //When
                settings.SetNoCache();

                //Then
                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Cache-Control"));
                Assert.Equal(settings.Headers["Cache-Control"], "no-store");
            }
        }

        public sealed class TheSetOriginMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Url_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string url = null;

                //When
                url = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetOrigin(settings, url));
                url = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetOrigin(settings, url));
                url = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetOrigin(settings, url));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(url));
                Assert.IsArgumentNullException(emptyRecord, nameof(url));
                Assert.IsArgumentNullException(spaceRecord, nameof(url));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Origin_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string url = "www.google.com";

                //When
                settings.SetOrigin(url);

                //Then
                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Origin"));
                Assert.Equal(settings.Headers["Origin"], url);
            }
        }

        public sealed class TheSetRefererMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Url_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string url = null;

                //When
                url = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetReferer(settings, url));
                url = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetReferer(settings, url));
                url = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetReferer(settings, url));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(url));
                Assert.IsArgumentNullException(emptyRecord, nameof(url));
                Assert.IsArgumentNullException(spaceRecord, nameof(url));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Referer_Header()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string url = "http://www.google.com/gmail";

                //When
                settings.SetReferer(url);

                //Then
                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Referer"));
                Assert.Equal(settings.Headers["Referer"], url);
            }
        }

        public sealed class TheSetRequestBodyMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Settings_Parameter()
            {
                //Given                
                HttpSettings settings = null;
                string requestBody = "{ \"id\":0, \"name\": \"testing\"}";

                //When
                var record = Record.Exception(() => HttpSettingsExtensions.SetRequestBody(settings, requestBody));

                //Then
                Assert.IsArgumentNullException(record, "settings");
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_RequestBody_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                string requestBody = null;

                //When
                requestBody = null;
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetRequestBody(settings, requestBody));
                requestBody = string.Empty;
                var emptyRecord = Record.Exception(() => HttpSettingsExtensions.SetRequestBody(settings, requestBody));
                requestBody = "      ";
                var spaceRecord = Record.Exception(() => HttpSettingsExtensions.SetRequestBody(settings, requestBody));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(requestBody));
                Assert.IsArgumentNullException(emptyRecord, nameof(requestBody));
                Assert.IsArgumentNullException(spaceRecord, nameof(requestBody));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Add_Request_Body()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                string requestBody = "{ \"id\":0, \"name\": \"testing\"}";

                //When
                settings.SetRequestBody(requestBody);

                //Then
                Assert.NotNull(settings.RequestBody);
                Assert.Equal(requestBody, Encoding.UTF8.GetString(settings.RequestBody));
            }
        }

        public sealed class TheSetFormUrlEncodedRequestBodyMethod
        {
            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Settings_Parameter()
            {
                //Given                
                HttpSettings settings = null;
                var data = new Dictionary<string, string>();

                //When
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetFormUrlEncodedRequestBody(settings, data));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(settings));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Throw_On_Null_Or_Empty_Data_Parameter()
            {
                //Given                
                HttpSettings settings = new HttpSettings();
                IDictionary<string, string> data = null;

                //When
                var nullRecord = Record.Exception(() => HttpSettingsExtensions.SetFormUrlEncodedRequestBody(settings, data));

                //Then
                Assert.IsArgumentNullException(nullRecord, nameof(data));
            }

            [Fact]
            [Trait(Traits.TestCategory, TestCategories.Unit)]
            public void Should_Set_Request_Body_As_Url_Encoded()
            {
                //Given
                HttpSettings settings = new HttpSettings();
                IDictionary<string, string> data = new Dictionary<string, string>
                {
                    ["Id"] =  "123",
                    ["LastName"] = "Test",
                    ["FirstName"] = "John"
                };

                //When
                settings.SetFormUrlEncodedRequestBody(data);

                //Then
                var expected = "Id=123&LastName=Test&FirstName=John";

                Assert.NotNull(settings.Headers);
                Assert.True(settings.Headers.ContainsKey("Content-Type"));
                Assert.Equal(settings.Headers["Content-Type"], "application/x-www-form-urlencoded");

                Assert.NotNull(settings.RequestBody);                                
                Assert.Equal(expected, Encoding.UTF8.GetString(settings.RequestBody));
            }
        }
    }
}
