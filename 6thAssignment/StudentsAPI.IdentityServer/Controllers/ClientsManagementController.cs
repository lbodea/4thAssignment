using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsAPIClient.Services;
using System;
using System.Collections.Generic;

namespace StudentsAPI.IdentityServer.Controllers
{
    [Route("apiclients/[controller]")]
    [ApiController]
    public class ClientsManagementController : ControllerBase
    {
        private readonly IStudentsAPIClients studentsAPIClients;

        public ClientsManagementController(IStudentsAPIClients studentsAPIClients)
        {
            this.studentsAPIClients = studentsAPIClients;
        }

        // POST: identityapi/ClientsManagement 
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult<ClientCredentials> Post([FromBody]ClientCredentials credentials)
        {
            if (credentials != null)
            {
                studentsAPIClients.Add(new Client() 
                { 
                    ClientId = credentials.ClientId,
                    ClientSecrets = new List<Secret> 
                    { 
                        new Secret(credentials.ClientSecret.Sha256()) 
                    },
                    AllowedScopes = new List<string> { credentials.Scope}
                });
            }
            return Ok();
        }
        
        // DELETE: identityapi/ClientsManagement/1
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete(long id)
        {    
            Client client = studentsAPIClients.GetAll()[Convert.ToInt32(id) - 1];
            if (client.ClientId.Equals("StudentAPIAdmin"))
            {
                return NotFound();
            }
            studentsAPIClients.Delete(client);
            return Ok();

        }
    }
}
