using System.Collections.Generic;
namespace ClienteCore.Models
{
    public interface IClienteDAL
    {
        IEnumerable<Cliente> GetAllClientes();
        void AddCliente(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        Cliente GetCliente(int? id);
        void DeleteCliente(int? id);
    }
}