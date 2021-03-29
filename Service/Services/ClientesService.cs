using Domain.Entity;
using Repository.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository repository;

        public ClienteService(IClienteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Cliente> GetCliente(long id)
        {
            return await repository.GetCliente(id);
        }

        public async Task<IEnumerable<Cliente>> GetCliente()
        {
            return await repository.GetCliente();
        }

        public async Task<Cliente> UpdateCliente(Cliente Cliente)
        {
            return await repository.UpdateCliente(Cliente);
        }
        public async Task<Cliente> InsertCliente(Cliente Cliente)
        {
            return await repository.InsertCliente(Cliente);
        }

        public async Task<bool> DeleteCliente(long id)
        {
            return await repository.DeleteCliente(id);
        }
    }
}
