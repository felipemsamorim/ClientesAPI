using Dapper;
using Domain.Entity;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConnectionFactory connection;

        public ClienteRepository(IConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<Cliente>> GetCliente()
        {
            string sql = "select * from [clientes_db].[dbo].[Cliente]";

            IList<Cliente> listCliente = new List<Cliente>();
            
            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryAsync<dynamic>(sql);

                if (result.Any())
                {
                    foreach (var item in result.ToList())
                    {
                        var cliente = new Cliente(item.id, item.nome, item.CPF, item.telefone );
                        listCliente.Add(cliente);
                    }
                }
            }
            return listCliente;
        }

        public async Task<Cliente> InsertCliente(Cliente cliente)
        {
            string sql = "Insert into [clientes_db].[dbo].[Cliente] ([nome],[CPF],[telefone]) values (@nome, @CPF, @telefone)";
            
            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var clienteResult = await connectionDb.ExecuteAsync(sql,
                    new
                    {
                        id = cliente.id,
                        nome = cliente.nome,
                        CPF = cliente.CPF,
                        telefone = cliente.telefone
                    });

                return cliente;
            }
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente)
        {
            string sql = "UPDATE [clientes_db].[dbo].[Cliente] SET[nome] = @nome, [CPF]= @CPF WHERE [id] = @id;";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var clenteResult = await connectionDb.ExecuteAsync(sql,
                     new
                     {
                         id = cliente.id,
                         nome = cliente.nome,
                         CPF = cliente.CPF,
                         telefone = cliente.telefone
                     });

                return cliente;
            }
        }

        public async Task<Cliente> GetCliente(long id)
        {
            string sql = "select * from [clientes_db].[dbo].[Cliente] where [id] = @id";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryFirstOrDefaultAsync<Cliente>(sql,
                     new
                     {
                         id = id,
                     });

                return result;
            }
        }

        public async Task<bool> DeleteCliente(long id)
        {
            string sql = "Delete [clientes_db].[dbo].[Cliente] where [id] = @id";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.QueryAsync<Cliente>(sql,
                    new
                    {
                        id = id
                    });

                return true;
            }
        }
    }
}
