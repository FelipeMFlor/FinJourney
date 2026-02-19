using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinJourney.Entities;
namespace FinJourney
{0
    public class Program
    {
        static void Main(string[] args)
        {      
            BankAccount account = new BankAccount(1, "Conta Corrente");

            // Ajustar para que o menu funcione normalmente e os testes possam prosseguir.


            Menu();

            Console.ReadKey();
        }

        public static void Menu()
        {
            Console.WriteLine("Escolha a operação que deseja realizar:");
            Console.WriteLine("1 - Adicionar dinheiro");
            Console.WriteLine("2 - Retirar dinheiro");
            Console.WriteLine("3 - Informações da conta");

            Console.ReadLine();
          int chose = int.Parse(Console.ReadLine());
          
          switch (chose)
            {
                case 1: AdicionarDinheiro();
                    break;

                case 2: RetirarDinheiro();
                    break;

                case 3: InfoConta();
                    break;

                default: Console.WriteLine("Obrigado por utilizar o sistema"); break;

            }
        }   




        public static void AdicionarDinheiro(BankAccount account, double value)
        {
            Console.WriteLine("Insira o valor que deseja adicionar:");
            account.AddMoney(value);
        }

        public static void RetirarDinheiro(BankAccount account, double value)
        {
            Console.WriteLine("Insira o valor que deseja retirar:");
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
