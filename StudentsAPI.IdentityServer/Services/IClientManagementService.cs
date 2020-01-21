using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.IdentityServer.Services
{
    public interface IClientManagementService
    {
        IEnumerable<Client> Get();
        void Add(Client client);
        bool Delete(string client);
    }
}
