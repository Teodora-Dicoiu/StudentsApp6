using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.IdentityServer.Services;

namespace StudentsAPI.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class ClientsManagementController : ControllerBase
    {
        private readonly IClientManagementService clientsService;

        public ClientsManagementController(IClientManagementService clientManagementService)
        {
            this.clientsService = clientManagementService;
        }

        // GET: api/ClientsManagement
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return clientsService.Get().ToList();
        }


        // POST: api/ClientsManagement

        [HttpPost]
        public ActionResult Post([FromBody] Client client)
        {
            clientsService.Add(client);
            return CreatedAtAction(nameof(Get), new { id = client.ClientId }, client);
        }

        // DELETE: api/ClientsManagement/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (clientsService.Delete(id)) return Ok();
            else return Forbid();
            
        }

    }
}