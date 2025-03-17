using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using UsersManagement.Senders.Domain;
using UsersManagement.Shared.Senders.Domain.Requests;
using UsersTests.UsersManagement.Senders.Domain;

namespace UsersTests.UsersAPI.Controllers.Senders.Post;
[Collection("Tests collection")]
public class SenderControllerTest : SenderModuleControllerUnitTestCase
{
    public SenderControllerTest(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldReturnOk_WhenAllIsOk()
    {
        // GIVEN
        Send send = SendMother.CreateRandom();
        SenderRequest request = new SenderRequest(
            send.To.To, send.Subject.Subject, send.Message.Message);
        // WHEN
        HttpResponseMessage response = await this._client.PostAsJsonAsync("Sender", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    } 
}