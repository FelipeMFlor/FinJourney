using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinJourney.Entities
{
    public abstract class Account
    {

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public double Balance { get; protected set; }
        
        protected Account(int id, string name)
        {
            Id = Id;
            Name = Name;
            Balance = 0;
        }

        // DECLARAR FUNÇÕES DE ADICIONAR DINHEIRO REMOVER DINHEIRO, TRANSFERIR DINHEIRO, ETC   ....



    }
}
