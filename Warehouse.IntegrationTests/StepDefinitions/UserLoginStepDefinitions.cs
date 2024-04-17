using Azure;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Warehouse.Api.Models.Responses.LoginResponses;

namespace Warehouse.IntegrationTests.StepDefinitions
{
    [Binding]
    public class UserLoginStepDefinitions : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private LoginRequest _loginRequest;

        public UserLoginStepDefinitions(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateDefaultClient(new Uri($"http://localhost/"));
        }

        [Given(@"a valid login request with correct password")]
        public void GivenAValidLoginRequestWithCorrectPassword()
        {
            _loginRequest = new LoginRequest { Email = "john.doe@example.com", Password = "password123" };
        }

        [When(@"the user submits the login request")]
        public async Task WhenTheUserSubmitsTheLoginRequest()
        {
            
            _response = await _client.PostAsJsonAsync("api/authentication/login", _loginRequest);
        }

        [Then(@"the response status code should be (.*) OK")]
        public void ThenTheResponseStatusCodeShouldBeOK(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }

        [Then(@"the response should contain a valid JWT token")]
        public async Task ThenTheResponseShouldContainAValidJWTToken()
        {
            var loginResponse = await _response.Content.ReadFromJsonAsync<LoginResponse>();
            loginResponse.Should().NotBeNull();
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
