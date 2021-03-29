using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClienteService 
    {
        Task<IEnumerable<Cliente>> GetCliente();
        Task<Cliente> InsertCliente(Cliente Cliente);
        Task<Cliente> UpdateCliente(Cliente Cliente);
        Task<Cliente> GetCliente(long ClienteId);
        Task<bool> DeleteCliente(long ClienteId);
    }
}
