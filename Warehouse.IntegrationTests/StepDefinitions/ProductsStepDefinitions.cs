using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Warehouse.Api.Models.Requests;
using Warehouse.Api.Models.Requests.Product;
using Warehouse.Api.Models.Responses.ProductResponses;

namespace Warehouse.IntegrationTests.StepDefinitions
{
    [Binding]
    public class ProductsStepDefinitions
    {
        private CustomWebApplicationFactory _factory;
        private ScenarioContext _scenarioContext;
        private string _jwtToken;
        private HttpResponseMessage _response;
        private HttpResponseMessage _productCreateResponse;
        private HttpClient _client;
        private ProductCreateRequest _productRequest;

        public ProductsStepDefinitions(ScenarioContext scenarioContext)
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

        [Given(@"a product is prepared for recording")]
        public void GivenAProductIsPreparedForRecording()
        {
            _productRequest = new ProductCreateRequest
            {
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "Digitall Product2256",
                Description = "Test Product",
                Price = 41,
                GroupIds = new List<Guid>
                {
                    Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Guid.Parse("99999999-9999-9999-9999-999999999999")
                },
                Sizes = new List<SizeInformationRequest>
                {
                    new SizeInformationRequest { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Quantity = 31 },
                    new SizeInformationRequest { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Quantity = 32 },
                    new SizeInformationRequest { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Quantity = 33 }
                }
            };
        }

        [When(@"user send request on Create Product API endpoint")]
        public async Task WhenUserSendRequestOnCreateProductAPIEndpoint()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _productCreateResponse = await _client.PostAsJsonAsync("api/Products", _productRequest);
        }

        [Then(@"the response contains created product information")]
        public async Task ThenTheResponseContainsCreatedProductInformation()
        {
            var content = await _productCreateResponse.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            var productsResponse = JsonConvert.DeserializeObject<ProductCreateResponse>(content);

            productsResponse.Should().NotBeNull();
        }
    }
}