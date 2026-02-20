using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinJourney.Entities;
namespace FinJourney
{
    public class Program
    {
        static void Main(string[] args)
        {      

            // Corrigir o método que exibe informações, ele está trazendo somente o saldo, porém o nome e id aparecem zerados.

            BankAccount account = new BankAccount(1, "Conta Corrente");

            Menu(account);

        }

        public static void Menu(BankAccount account)
        {

            bool running = true;

            while (running)
            {
                Console.WriteLine("Escolha a operação que deseja realizar:");
                Console.WriteLine("1 - Adicionar dinheiro");
                Console.WriteLine("2 - Retirar dinheiro");
                Console.WriteLine("3 - Informações da conta");

                string input = Console.ReadLine();
                int chose;
                if(!int.TryParse(input, out chose))
                {
                    Console.WriteLine("Número digitado inválido");
                    continue;
                }

                switch (chose)
                {
                    case 1:
                        AdicionarDinheiro(account);
                        break;

                    case 2:
                        RetirarDinheiro(account);
                        break;

                    case 3:
                        InfoConta(account);
                        break;

                    case 0:
                        running = false;
                        break;

                    default: Console.WriteLine("Opção inválida!"); break;

                }
            }
        }

        public static void AdicionarDinheiro(BankAccount account)
        {
            Console.WriteLine("Insira o valor que deseja adicionar:");
            double value = double.Parse(Console.ReadLine());
            account.AddMoney(value);
        }

        public static void RetirarDinheiro(BankAccount account)
        {
            Console.WriteLine("Insira o valor que deseja retirar:");
            double value = double.Parse(Console.ReadLine());
            account.RemoveMoney(value);
        }


        public static void InfoConta(BankAccount account)
        {
            Console.WriteLine($"ID: {account.Id}");
            Console.WriteLine($"Nome:{account.Name}");
            Console.WriteLine($"Saldo atual: {account.Balance}");
        }

    }
}
