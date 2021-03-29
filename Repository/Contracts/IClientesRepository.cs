using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetCliente();
        Task<Cliente> InsertCliente(Cliente Cliente);
        Task<Cliente> UpdateCliente(Cliente cliente);
        Task<Cliente> GetCliente(long Id);
        Task<bool> DeleteCliente(long Id);
    }
}
