﻿using MyHomeBar.Api.IntegrationTest.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeBar.Api.IntegrationTest.Specs
{
    [TestFixture]
    public class MyHomeBarControllerTests
    {
        private TestHostFixture fixture;

        [OneTimeSetUp]
        public void Init()
        {
            this.fixture = new TestHostFixture();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            fixture.Dispose();
        }

        [Test]
        public async Task Only_Users_Over_The_Allowed_Age()
        {
            var response = await fixture.Server.CreateRequest(MyHomeBarAPI.View.Drink("Vodka")).GetAsync();

            response.EnsureSuccessStatusCode();
        }

        
    }
}