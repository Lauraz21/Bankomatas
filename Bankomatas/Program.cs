using System.Collections.Generic;
using System.Text.Json;

namespace Bankomatas
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("B A N K O M A T A S");
            Console.WriteLine("--------------------");
            Console.WriteLine("Prasome ivesti Jusu korteles ID:");

            List<Card> cards = ReadCardsFromFile();

            Guid usersCardGuid = Guid.Parse(Console.ReadLine());
            Card currentUserCard = cards.Single(card => card.ID == usersCardGuid);
            Console.Clear();

            bool isPinCorrect = false;
            int maxAttempts = 3;

            while (!isPinCorrect)
            {
                Console.WriteLine("Iveskite PIN koda:");

                string usersInputPin = Console.ReadLine();
                Console.Clear();

                if (usersInputPin == currentUserCard.Pin)
                {
                    Console.WriteLine($"Sekmingai prisijungete!");
                    isPinCorrect = true;
                }
                else
                {
                    maxAttempts--;
                    if (maxAttempts == 0)
                    {
                        Console.WriteLine("Prisijungimo sesija baigesi.");
                        return;
                    }
                    Console.WriteLine($"Ivestas PIN kodas neteisingas. Jums liko {maxAttempts} bandymas.");
                }
            }
            bool showMenu = true;
            while (showMenu)
            {
                Console.WriteLine("\nPasirinkite: \n\n1. Jusu saskaitos likutis \n2. Paskutiniu 5 transakciju istorija " +
                    "\n3. Grynuju pinigu isemimas \n4. Grazinti kortele");

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (choice)
                {
                    case '1':
                        Console.WriteLine($"Jusu saskaitos likutis: {currentUserCard.Money} Eur");
                        break;

                    case '2':
                        Console.WriteLine($"Transakciju istorija: {GetTransactionHistory()}");
                        break;

                    case '3':
                        GetMoneyBalance(currentUserCard);
                        break;

                    case '4':
                        Console.WriteLine("Operacija baigta. Aciu, kad naudojates musu paslaugomis");
                        showMenu = false;
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
            WriteToFile(cards);
        }


        public static string GetTransactionHistory()
        {
            return "2023.10.05";
        }

        public static void GetMoneyBalance(Card currentUserCard)
        {
            Console.WriteLine($"Iveskite pinigu suma: ");
            int cashOut = Convert.ToInt32(Console.ReadLine());

            if (currentUserCard.CashOutAttemps <= 10)
            {
                if (currentUserCard.CashOutSum + cashOut <= 1000)
                {
                    currentUserCard.Money -= cashOut;
                    currentUserCard.CashOutSum += cashOut;
                    currentUserCard.CashOutAttemps++;
                    Console.WriteLine($"Operacija ivykdyta sekmingai. Jusu pinigu likutis: {currentUserCard.Money} Eur.");
                }
                else
                {
                    Console.WriteLine("Atsiprasome, deja operacija nepavyko. Pasiektas sios dienos pinigu isemimo limitas.");
                }
            }
            else
            {
                Console.WriteLine("Atsiprasome, deja operacija nepavyko. Pasiektas sios dienos transakciju limitas.");
            }
        }
        public static void WriteToFile(List<Card> cards)
        {
            string jsonString = JsonSerializer.Serialize(cards, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("cards.json", jsonString);
        }
        public static List<Card> ReadCardsFromFile()
        {
            string jsonString = File.ReadAllText("cards.json");
            return JsonSerializer.Deserialize<List<Card>>(jsonString);
        }


    }
}