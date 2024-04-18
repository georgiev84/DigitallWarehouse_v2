using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Warehouse.Api.Models.Responses.ProductResponses;
using Warehouse.Domain.Entities.Products;

namespace Warehouse.IntegrationTests.StepDefinitions
{
    [Binding]
    public class AccessProductsStepDefinitions
    {
        private CustomWebApplicationFactory _factory;
        private ScenarioContext _scenarioContext;
        private string _jwtToken;
        private LoginRequest _loginRequest;
        private HttpResponseMessage _response;
        private HttpClient _client;

        public AccessProductsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {

            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateDefaultClient(new Uri($"http://localhost/"));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [Given(@"user is logged")]
        public void GivenUserIsLogged()
        {
            _jwtToken = _scenarioContext.Get<string>("UserJWTToken");
        }

        [When(@"the user requests access to the products API endpoint")]
        public async Task WhenTheUserRequestsAccessToTheProductsAPIEndpoint()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _response = await _client.GetAsync("api/Products");
        }

        [Then(@"the response contains a non-empty array of products")]
        public async Task ThenTheResponseContainsANon_EmptyArrayOfProducts()
        {
            var content = await _response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            var productsResponse = JsonConvert.DeserializeObject<ProductDetailedResponse>(content);

            productsResponse.Should().NotBeNull();
        }
    }
}
