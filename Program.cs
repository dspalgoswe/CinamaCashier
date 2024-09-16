using System;
using System.Collections.Generic;

namespace BiografKassan
{
    // Beklagar ett fult slut på koden. Vågar inte peta i det. Nämligen då allt hängde sig. / RA

    // Klass avser kund
    public class Customer
    {
        public int Age { get; set; }

        // Konstruktor utifrån ålder
        public Customer(int age)
        {
            Age = age;
        }
    }

    // Klass f. biljettpris
    public class TicketPricing
    {
        // Metod, beräkna biljettpriser utifrån en lista av kunder
        public decimal CalculateTotalPrice(List<Customer> customers)
        {
            decimal total = 0;

            foreach (var customer in customers)
            {
                total += GetTicketPrice(customer.Age);
            }

            return total;
        }

        // Metod f. biljettpris utifrån ålder
        public decimal CalculateSinglePrice(int age)
        {
            return GetTicketPrice(age);
        }

        private decimal GetTicketPrice(int age)
        {
            if (age < 6)
            {
                return 0; // De minsta går gratis
            }
            else if (age > 99)
            {
                return 0; // Hundraåringar går gratis
            }
            else // Ålder mellan 6 och 99
            {
                if (age < 18)
                {
                    return 80; // Ungdomspris
                }
                else
                {
                    // Här är en ny if-sats inuti else
                    if (age >= 65)
                    {
                        return 90; // Pensionärspris
                    }
                    else
                    {
                        return 120; // Standardpris
                    }
                }
            }
        }
    }

    // Klass för att hantera meny och användargränssnitt
    public class Menu
    {
        public void Show()
        {
            Console.WriteLine("Välkommen! Du har flera alternativ");
            Console.WriteLine("1. Beräkna biljettpriser för en person");
            Console.WriteLine("2. Beräkna biljettpriser för flera personer");
            Console.WriteLine("3. Upprepa ord");
            Console.WriteLine("4. Hämta det tredje ordet i en mening");
            Console.WriteLine("0. Avsluta");
        }

        public int GetChoice()  // Nedan en s.k. "ternary operator" som felkoll
        {
            Console.Write("Välj ett alternativ: ");
            string input = Console.ReadLine();
            return int.TryParse(input, out int choice) ? choice : -1; // Felinmatning ger -1 t. switch
        }
    }

    // Huvudprogram, dvs "Main"
    public class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            TicketPricing ticketPricing = new TicketPricing();
            List<Customer> customers = new List<Customer>();
            bool continueProgram = true;

            while (continueProgram)
            {
                menu.Show();
                int choice = menu.GetChoice();

                // Menyalternativ med switch-sats
                switch (choice)
                {
                    case 1:
                        // För en person
                        Console.Write("Ange ålder för personen: ");
                        int age = int.Parse(Console.ReadLine());
                        decimal singlePrice = ticketPricing.CalculateSinglePrice(age); // Exempelmetod
                        Console.WriteLine($"Biljettpriset för personen är: {singlePrice} kr");
                        break;

                    case 2:
                        // För flera personer
                        Console.Write("Ange antal personer: ");
                        int numberOfCustomers = int.Parse(Console.ReadLine());
                        customers.Clear(); // Rensa listan för nya kunder

                        for (int i = 0; i < numberOfCustomers; i++)
                        {
                            Console.Write($"Ange ålder för person {i + 1}: ");
                            int customerAge = int.Parse(Console.ReadLine());
                            customers.Add(new Customer(customerAge)); // Lägg till kund i listan
                        }

                        decimal totalPrice = ticketPricing.CalculateTotalPrice(customers);
                        Console.WriteLine($"Sammanlagt biljettpris för {numberOfCustomers} personer: {totalPrice} kr");
                        break;

                    case 3:
                        // Upprepa ord
                        Console.Write("Ange en godtycklig text: ");
                        string userInput = Console.ReadLine();
                        Console.WriteLine("Upprepade ord: ");

                        // Använd en for-loop för att skriva ut texten tio gånger
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write(userInput);
                            if (i < 9) // För att inte få kommatecken efter sista ordet
                            {
                                Console.Write(", "); // Sätt ett komma mellan varje upprepning
                            }
                        }
                        Console.WriteLine(); // Gå till nästa rad efter upprepningar
                        break;

                    case 4:
                        // Det tredje ordet
                        Console.Write("Ange en mening med minst tre ord: ");
                        string sentence = Console.ReadLine();
                        string[] words = sentence.Split(' '); // Dela upp strängen på varje mellanslag

                        // Kontrollera om det finns minst tre ord
                        if (words.Length < 3)
                        {
                            Console.WriteLine("Mening måste innehålla minst tre ord. Försök igen!");
                        }
                        else
                        {
                            // Plocka ut det tredje ordet
                            string thirdWord = words[2];
                            Console.WriteLine($"Det tredje ordet är: {thirdWord}");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Tack för ditt besök!");
                        continueProgram = false; // Avsluta programmet
                        break;

                    default:
                        Console.WriteLine("Felaktig input. Ange '1' för att beräkna biljettpriser för en person, '2' för flera personer, '3' för att upprepa ord, '4' för att hämta det tredje ordet, eller '0' för att avsluta. Försök igen!");
                        break;
                }
            }
        }
    }
}
