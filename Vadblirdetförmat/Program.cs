using System;
using System.Collections.Generic;

namespace Vadblirdetförmat
{

    class Program
    {
        static List<Choice> Choices = new List<Choice>();

        static void Main(string[] args)
        {
            StartApp(); //Välkomsthälsning
            ReadTextFile();
            EnterFoodDate(); // Användaren matar in datum 
            ShowDinnerPlaces(); // Användaren får alternativ på var maten
            ShowProteinSources();
            ShowCarbSources();
            ShowMenues();
            ShowRecepies();
            EndOfProgram();

        }
      
        private static void StartApp()
        {
            Console.WriteLine("Är du hungrig? Välkommen, här hittar du recept som passar dig!");
        }

        private static void ReadTextFile()
        {
            List<Meal> mealList = new List<Meal>();
            string[] textFile = File.ReadAllLines("Recepies.txt");
            foreach (string item in textFile)
            {
                string[] listOfMealEvent = item.Split(',');

                var s = new Meal
                {
                    Time = DateTime.Parse(textFile[0]),
                    Place = textFile[1],
                    Protein = textFile[2],
                    Carbohydrates = textFile[3],
                    Menu = textFile[4],
                    Receipe = textFile[5],
                    Instructions = textFile[6],
                    Difficulty = int.Parse(textFile[7]),
                    Flavoring = int.Parse(textFile[8]),
                    Vegetables = textFile[9],
                };
                mealList.Add(s);
            }
            return textFile;
        }
    }

        private static void EnterFoodDate()
        {
            Console.WriteLine("Vänligen skriv in dagens datum: ");
            Console.ReadLine();
        }

        private static void ShowDinnerPlaces()
        {
            int lastChoice = Choices.Count;
            string dateMonth = Choices[lastChoice - 1].DinnerDate.Month.ToString();
            string dateDay = Choices[lastChoice - 1].DinnerDate.Day.ToString();

            if ((dateMonth == "12" && dateDay == "24") || (dateMonth == "3" && dateDay == "24") || (dateMonth == "6" && dateDay == "21"))
            {
                GetHolidayDinner(dateMonth);
            }
            else if(int.Parse(dateDay) >= 25 && int.Parse(dateDay) <= 28)
            {
                int timeSlot = 1;
                ShowPlaces(timeSlot);
            }
            else if (int.Parse(dateDay) >= 29 && int.Parse(dateDay) <= 31 || int.Parse(dateDay) >= 1 && int.Parse(dateDay) <= 10)
            {
                int timeSlot = 2;
                ShowPlaces(timeSlot);
            }
            else if (int.Parse(dateDay) >= 11 && int.Parse(dateDay) <= 21)
            {
                int timeSlot = 3;
                ShowPlaces(timeSlot);
            }
            else
            {
                int timeSlot = 4;
                ShowPlaces(timeSlot);
            }
        }

        private static void ShowPlaces(int timeSlot)
        {
            var showPlaces = ReadTextFil.Select(x => x.Place).Distinct();
            PrintChoices(showPlaces);
            EnterChoice();
            FriendOrFamily();
        }

        private static void FriendOrFamily()
        {
            throw new NotImplementedException();
        }

        private static void GetHolidayDinner(string dateMonth)
        {
            string holidaydinner = "";

            switch (dateMonth)
            {
                case "12":
                    holidaydinner = "Julmiddag";
                    break;
                case "3":
                    holidaydinner = "Påskmiddag";
                    break;
                case "6":
                    holidaydinner = "Midsommarmat";
                    break;
            }

            Console.WriteLine($"Fantastiskt! det är {holidaydinner} som serveras!");
            Console.WriteLine("Välj var du vill inta din middag. Välj i listan:");
            ShowPlaces();
        }

        private static void ShowProteinSources()
        {
            EnterChoice();
            throw new NotImplementedException();
        }

        private static void EnterChoice()
        {
            if(3)

            
            throw new NotImplementedException();
        }

        private static void ShowCarbSources()
        {
            EnterChoice();
            throw new NotImplementedException();
        }
        private static void ShowMenues()
        {
            CreateNewMenue();
        }
        private static void ShowRecepies()
        {
            throw new NotImplementedException();
        }
        private static void PrintChoices(object showPlaces)
        {
            throw new NotImplementedException();
        }
        private static void EndOfProgram()
        {
            throw new NotImplementedException();
        }

    }

}
