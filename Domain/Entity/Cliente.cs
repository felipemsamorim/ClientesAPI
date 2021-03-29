using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entity
{
    public class Cliente
    {

        public Cliente(long id, string nome, string CPF, string telefone)
        {
            this.id = id;
            this.nome = nome;
            this.CPF = CPF;
            this.telefone = telefone;
        }

        public long id { get; private set; }
        public string nome { get; private set; }
        public string CPF { get; private set; }
        public string telefone { get; private set; }

    }
}
