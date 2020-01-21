using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace StudentsAPI.IntegrationTests
{
    public class IntegrationTestsBase : IClassFixture<StudentsApiWebApplicationFactory<Startup>>
    {
        protected readonly StudentsApiWebApplicationFactory<Startup> factory;
        protected readonly HttpClient client;

        public IntegrationTestsBase(StudentsApiWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Add("api-version", "2.0");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkI2MjJDQTQyMjVGQ0FENzIzMjYyRURBOTczMUI0MzE3RjAxMzdBNTgiLCJ0eXAiOiJhdCtqd3QiLCJ4NXQiOiJ0aUxLUWlYOHJYSXlZdTJwY3h0REZfQVRlbGcifQ.eyJuYmYiOjE1Nzk2MTM2NDMsImV4cCI6MTU3OTYxNzI0MywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6IlN0dWRlbnRBUEkiLCJjbGllbnRfaWQiOiJTdHVkZW50QVBJQWRtaW4iLCJzY29wZSI6WyJzdHVkZW50YXBpLmFkbWluIl19.aSZlZqg7GfYMu08nbE8V_-UzD6OHETIpDI6ScB4Scipg6iHwNKmCXaoHPM4YgEYmg5VzPVfVqx_aW3CMWg4hvm6YpVlo0E5JUBJsrya_qGFQFa2AkiomjUJdd96a2JhtNsN9hNBN9Lv2OYRCpz_o77iCmw83zWHLwSRW0tvfIcr5fOa6RDsMt8pueqBCtNtR9bqi0uLCJ9Y_CqgFxo31wnwgUbc05nIDH0a4l-6RxBLY7cNEsZCm8GBx6zBsPt9PEI0hbP_UIGRHERvsXCjTjkhvm2AKweJ2TcG4B534SDaokGm29960RIVmIM-XD5CIXgVqkowO7cvI1WYJ3ofUKw");
            client.DefaultRequestHeaders.Add("x-api-key", "123456");
            this.factory = factory;
        }

    }
}
