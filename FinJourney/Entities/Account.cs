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

        public virtual void AddMoney (double amounnt) 
        {

            if (amounnt <= 0)
            {
                throw new ArgumentException("Valor deve ser maior que zero");
            }

            else 
            {
                Balance += amounnt;     
            }
        
        }
        
        public virtual void RemoveMoney (double amounnt)
        {

            if (amounnt < 0)
            {

                throw new ArgumentException("Valor deve ser maior que zero");

            }
            else if (amounnt > Balance)
            {
                throw new ArgumentException("Valor deve ser menor ou igual ao saldo atual");
            }

            else
            {
                Balance -= amounnt;
            }

        }
        
        
        
        // Futuramente adicionar tranferência entre contas.



    }
}
