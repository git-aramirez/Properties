using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Mvc;
using Properties.Tests.Integration.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Properties.Domain.Entities;
using FluentAssertions;
using Docker.DotNet;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Properties.Domain.DTOs.Owner;
using Properties.Domain.DTOs.Property;
using Properties.Domain.DTOs.PropertyImage;
using Newtonsoft.Json;
using Properties.Domain.DTOs.PropertyTrace;

namespace Properties.Tests.Integration.Tests
{
    public class Test : IClassFixture<ApiFactory>, IAsyncLifetime
    {
        private readonly HttpClient _httpClient;
        private Func<Task> _resetDatabase;

        public Test(ApiFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.HttpClient;
            _resetDatabase = webApplicationFactory.ResetDatabaseAsync;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "anderson04211@gmail.com", "Mx24as435vXsad7Zx6a")));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        [Fact]
        public async Task GivenValidOwner_CreatesOwner()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Anderson",
                Address = "Cr 17 # 45 -34",
                Birthday = DateTime.Now
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await response.Content.ReadFromJsonAsync<OwnerResponse>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createdOwner.Should().NotBeNull();
            createdOwner!.Name.Should().Be(owner.Name);
            createdOwner!.Address.Should().Be(owner.Address);
            createdOwner!.Birthday.Should().Be(owner.Birthday);
        }

        [Fact]
        public async Task GivenValidPropertyImage_CreatesPropertyImage()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Darwin",
                Address = "Cr 16 # 21 -34",
                Birthday = DateTime.Now
            };
            var responseOwner = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await responseOwner.Content.ReadFromJsonAsync<OwnerResponse>();

            var property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town 2",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2023
            };

            var responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            var createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            var propertyImage = new CreatePropertyImageResquest
            {
                PropertyId = createdProperty.PropertyId,
                File = "https://file_2",
                Enabled = true
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/PropertyImage", propertyImage);
            var createdPropertyImage = await response.Content.ReadFromJsonAsync<CreatePropertyImageResquest>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createdPropertyImage.Should().NotBeNull();
            createdPropertyImage!.PropertyId.Should().Be(propertyImage.PropertyId);
            createdPropertyImage!.File.Should().Be(propertyImage.File);
            createdPropertyImage!.Enabled.Should().Be(propertyImage.Enabled);
        }

        [Fact]
        public async Task GivenValidChangePrice_ChangePrice()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Camilo",
                Address = "Cr 16 # 21 -34",
                Birthday = DateTime.Now
            };
            var responseOwner = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await responseOwner.Content.ReadFromJsonAsync<OwnerResponse>();

            var property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2023
            };
            var responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            var createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            // Act
            var responseChancePrice = await _httpClient.PatchAsync($"/api/Property/{createdProperty!.PropertyId}/{111}", null);
            var responseGetProperty = await _httpClient.GetAsync($"/api/Property/{createdProperty.PropertyId}");
            var createdPropertyChanged = await responseGetProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            // Assert
            responseGetProperty.StatusCode.Should().Be(HttpStatusCode.OK);
            responseChancePrice.StatusCode.Should().Be(HttpStatusCode.OK);
            createdPropertyChanged.Should().NotBeNull();
            createdPropertyChanged!.Name.Should().Be(property.Name);
            createdPropertyChanged!.Address.Should().Be(property.Address);
            createdPropertyChanged!.Price.Should().Be(111);
            createdPropertyChanged!.CodeInternal.Should().Be(property.CodeInternal);
            createdPropertyChanged!.Year.Should().Be(property.Year);
        }

        [Fact]
        public async Task GivenValidProperty_CreatesProperty()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Camilo",
                Address = "Cr 16 # 21 -34",
                Birthday = DateTime.Now
            };
            var responseOwner = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await responseOwner.Content.ReadFromJsonAsync<OwnerResponse>();

            var property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2023
            };

            // Act
            var responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            var createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            // Assert
            responseProperty.StatusCode.Should().Be(HttpStatusCode.OK);
            createdProperty.Should().NotBeNull();
            createdProperty!.Name.Should().Be(property.Name);
            createdProperty!.Address.Should().Be(property.Address);
            createdProperty!.Price.Should().Be(property.Price);
            createdProperty!.CodeInternal.Should().Be(property.CodeInternal);
            createdProperty!.Year.Should().Be(property.Year);
        }

        [Fact]
        public async Task GivenValidListProperty_ListProperty()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Camilo",
                Address = "Cr 16 # 21 -34",
                Birthday = DateTime.Now
            };
            var responseOwner = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await responseOwner.Content.ReadFromJsonAsync<OwnerResponse>();

            var property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2011
            };

            var responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            var createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2015
            };

            responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            // Act
            var responsePropertyList = await _httpClient.GetAsync($"/api/Property/year-between/{2010}/{2016}");
            var createdPropertyString = await responsePropertyList.Content.ReadFromJsonAsync<List<PropertyResponse>>();

            // Assert
            responsePropertyList.StatusCode.Should().Be(HttpStatusCode.OK);
            createdPropertyString.Should().NotBeNull();
            createdPropertyString!.Count.Should().Be(2);
        }

        [Fact]
        public async Task GivenValidUpdatedProperty_UpdateProperty()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Camilo",
                Address = "Cr 16 # 21 -34",
                Birthday = DateTime.Now
            };
            var responseOwner = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await responseOwner.Content.ReadFromJsonAsync<OwnerResponse>();

            var property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2023
            };
            var responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            var createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            var propertytoUpdatate = new UpdatePropertyRequest
            {
                PropertyId = createdProperty!.PropertyId,
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town",
                Address = " Cr 14 # 45 - 67",
                Price = 789,
                CodeInternal = 78,
                Year = 555
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(propertytoUpdatate), Encoding.UTF8, "application/json");
            var responseUpdate = await _httpClient.PutAsync($"/api/Property", content);
            var responseGetProperty = await _httpClient.GetAsync($"/api/Property/{createdProperty.PropertyId}");
            var createdPropertyUpdated = await responseGetProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            // Assert
            responseGetProperty.StatusCode.Should().Be(HttpStatusCode.OK);
            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK);
            createdPropertyUpdated.Should().NotBeNull();
            createdPropertyUpdated!.Name.Should().Be(property.Name);
            createdPropertyUpdated!.Address.Should().Be(property.Address);
            createdPropertyUpdated!.Price.Should().Be(789);
            createdPropertyUpdated!.CodeInternal.Should().Be(property.CodeInternal);
            createdPropertyUpdated!.Year.Should().Be(555);
        }

        [Fact]
        public async Task GivenValidPropertyTrace_CreatesPropertyTrace()
        {
            // Arrange
            var owner = new CreateOwnerRequest
            {
                Name = "Darwin",
                Address = "Cr 16 # 21 -34",
                Birthday = DateTime.Now
            };
            var responseOwner = await _httpClient.PostAsJsonAsync("/api/Owner", owner);
            var createdOwner = await responseOwner.Content.ReadFromJsonAsync<OwnerResponse>();

            var property = new CreatePropertyRequest
            {
                OwnerId = createdOwner!.OwnerId,
                Name = "High Town 2",
                Address = " Cr 14 # 45 - 67",
                Price = 78978,
                CodeInternal = 78,
                Year = 2023
            };

            var responseProperty = await _httpClient.PostAsJsonAsync("/api/Property", property);
            var createdProperty = await responseProperty.Content.ReadFromJsonAsync<PropertyResponse>();

            var propertyTrace = new CreatePropertyTraceRequest
            {
                PropertyId = createdProperty!.PropertyId,
                DateSale = DateTime.Now,
                Name = "Property Trace 1",
                Value = 789897,
                Tax = 789879
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/PropertyTrace", propertyTrace);
            var createdPropertyTrace = await response.Content.ReadFromJsonAsync<CreatePropertyTraceRequest>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createdPropertyTrace.Should().NotBeNull();
            createdPropertyTrace!.PropertyId.Should().Be(propertyTrace.PropertyId);
            createdPropertyTrace!.DateSale.Should().Be(propertyTrace.DateSale);
            createdPropertyTrace!.Name.Should().Be(propertyTrace.Name);
            createdPropertyTrace!.Value.Should().Be(propertyTrace.Value);
            createdPropertyTrace!.Tax.Should().Be(propertyTrace.Tax);
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public Task DisposeAsync() => _resetDatabase();
    }
}
