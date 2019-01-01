using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using MyHomeBar.Api.Controllers;
using MyHomeBar.Api.IntegrationTest.Infrastructure;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static MyHomeBar.Api.Controllers.MyHomeBarController;

namespace MyHomeBar.Api.IntegrationTest.Specs
{
    [TestFixture]
    public class MyHomeBarControllerTests
    {
        private TestHostFixture fixture;

        [OneTimeSetUp]
        public void Init()
        {
            fixture = new TestHostFixture();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            fixture.Dispose();
        }

        [Test]
        [Ignore("Uses MyTestsAuthenticationHandler")]
        public async Task Check_if_user_can_view_drink()
        {
            HttpResponseMessage response = await fixture.Server.CreateRequest(MyHomeBarAPI.View.Drink("Vodka")).GetAsync();

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Only_Users_Over_The_Allowed_Age()
        {            
            RequestBuilder builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.AddDrink(new AddDrinkModel()));

            HttpResponseMessage response = await builder
                .WithIdentity(Identities.Menor)
                .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task Only_Users_That_Are_Not_Banned()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.ViewDrink("Vodka"));

            var response = await builder
                .WithIdentity(Identities.IsBanned)
                .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task If_HasTemporaryPass_CanDoActions()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.ViewDrink("Zuica"));

            var response = await builder
                .WithIdentity(Identities.TemporaryPass)
                .GetAsync();

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task AddDrink_Is_Authorized_For_Admin()
        {
            var drinkDTO = new AddDrinkModel() { Id = 1};
            RequestBuilder builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.AddDrink(drinkDTO));

            HttpResponseMessage response = await builder
                .WithIdentity(Identities.Admin)
                .PostAsync();

            await response.IsSuccessStatusCodeOrThrow();
        }

        [Test]
        public async Task AddDrink_Is_Authorized_For_Vendor()
        {
            var drinkDTO = new AddDrinkModel() { Id = 1 };
            RequestBuilder builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.AddDrink(drinkDTO));

            HttpResponseMessage response = await builder
                .WithIdentity(Identities.Vendor)
                .PostAsync();

            await response.IsSuccessStatusCodeOrThrow();
        }

        [Test]
        public async Task AddDrink_Is_NotAuthorized_For_Guest()
        {
            RequestBuilder builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.AddDrink(new AddDrinkModel()));

            HttpResponseMessage response = await builder
                .WithIdentity(Identities.Guest)
                .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task ViewDrink_Is_Authorized_For_Admin()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.ViewDrink("Vodka"));

            var response = await builder
                .WithIdentity(Identities.Admin)
                .GetAsync();

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ViewDrink_Is_NotAuthorized_For_Vendor()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.ViewDrink("Vodka"));

            var response = await builder
                .WithIdentity(Identities.Vendor)
                .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task ViewDrink_Is_Authorized_For_Guest_If_Not_SpecialDrink()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.ViewDrink("Vodka"));

            var response = await builder
                .WithIdentity(Identities.Guest)
                .GetAsync();

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ViewDrink_Is_NotAuthorized_For_Guest_If_SpecialDrink()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.ViewDrink("Zuica"));

            var response = await builder
                .WithIdentity(Identities.Guest)
                .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task MakeParties_Is_Authorized_For_Admin()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.MakeParties("Vodka"));

            var response = await builder
                .WithIdentity(Identities.Admin)
                .PostAsync();

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task MakeParties_Is_NotAuthorized_For_Vendor()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.MakeParties("Vodka"));

            var response = await builder
                .WithIdentity(Identities.Vendor)
                .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task MakeParties_Is_NotAuthorized_For_Guest()
        {
            var builder = fixture.Server.CreateHttpApiRequest<MyHomeBarController>(controller => controller.MakeParties("Vodka"));

            var response = await builder
                .WithIdentity(Identities.Guest)
                .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
