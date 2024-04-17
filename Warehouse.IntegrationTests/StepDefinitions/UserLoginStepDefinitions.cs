using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Warehouse.Api.Models.Responses.LoginResponses;
using Warehouse.Domain.Entities.Users;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.IntegrationTests.StepDefinitions
{
    [Binding]
    public class UserLoginStepDefinitions 
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private LoginRequest _loginRequest;
        private WarehouseDbContext _dbContext;
        private User _testUser;

        //public UserLoginStepDefinitions(WarehouseDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        [BeforeScenario]
        public void BeforeScenario()
        {
            
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateDefaultClient(new Uri($"http://localhost/"));

            //using (var scope = _factory.Services.CreateScope())
            //{
            //    _dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();

            //    _testUser = new User { Email = "test@test.com", Password = "testuserpassword" };
            //    _dbContext.Users.Add(_testUser);
            //    _dbContext.SaveChangesAsync();
            //}

            //_testUser = new User { Email = "test@test.com", Password="testuserpassword" };
            //_dbContext.Users.Add(_testUser);
            //_dbContext.SaveChanges();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_testUser != null)
            {
                _dbContext.Users.Remove(_testUser);
                _dbContext.SaveChanges();
            }

            _factory.Dispose();
            _client.Dispose();
        }

        [Given(@"a valid login request with correct password")]
        public void GivenAValidLoginRequestWithCorrectPassword()
        {
            _loginRequest = new LoginRequest { Email = "john.doe@example.com", Password = "password123" };
            //_loginRequest = new LoginRequest { Email = "test@test.com", Password = "testuserpassword" };
        }

        [Given(@"a valid login request with wrong password")]
        public void GivenAValidLoginRequestWithWrongPassword()
        {
            _loginRequest = new LoginRequest { Email = "john.doe@example.com", Password = "password1234" };
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
            loginResponse?.Token.Should().NotBeNull().And.NotBeEmpty();
        }


        [Then(@"the response should not contain a JWT token")]
        public async Task ThenTheResponseShouldNotContainAJWTToken()
        {
            var loginResponse = await _response.Content.ReadFromJsonAsync<LoginResponse>();
            loginResponse.Should().NotBeNull();
            loginResponse.Token.Should().BeNull("because the login attempt failed and no JWT token should be returned");
        }

    }
}
