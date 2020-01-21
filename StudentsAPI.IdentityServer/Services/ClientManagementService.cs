using IdentityServer4.Models;
using StudentsAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.IdentityServer.Services
{
    public class ClientManagementService : IClientManagementService

    {
        private readonly List<Client> clients;

        public ClientManagementService()
        {
            this.clients = new List<Client>
            {
                new Client()
                {
                    ClientId = "StudentAPIAdmin",

                    ClientSecrets = {
                        new Secret("admin-password".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes =
                    {
                        StudentsAPIScopes.Admin, StudentsAPIScopes.User, StudentsAPIScopes.RestrictedUser
                    }
                }
            };
        }

        public void Add(Client client)
        {
            lock (clients)
            {
                clients.Add(client);
            }
        }

        public bool Delete(string clientId)
        {
            int numberOfRows = 0;
            lock (clients)
            {
               numberOfRows= clients.RemoveAll(s => s.ClientId == clientId && (s.ClientId != "StudentAPIAdmin" ) );
            }
            if (numberOfRows>0) return true;
            else return false;
        }

        public IEnumerable<Client> Get()
        {
            return clients;
        }
    }
}
