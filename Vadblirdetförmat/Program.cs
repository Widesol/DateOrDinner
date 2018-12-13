using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vadblirdetförmat
{

    class Program
    {
        static List<Choice> Choices = new List<Choice>();

        static void Main(string[] args)
        {
            StartApp(); //Välkomsthälsning
            List<Meal> mealList=ReadTextFile();
            EnterFoodDate(); // Användaren matar in datum 
            ShowDinnerPlaces(mealList); // Användaren får alternativ på var maten
            ShowProteinSources();
            //ShowCarbSources();
            ShowMenues(mealList);
            ShowRecepies();
            EndOfProgram();

        }
      
        private static void StartApp()
        {
            Console.WriteLine("Är du hungrig? Välkommen, här hittar du recept som passar dig!");
        }

        private static List <Meal>  ReadTextFile()
        {
            List<Meal> mealList = new List<Meal>();
            string[] textFile = File.ReadAllLines(@"C:\Users\Administrator\Desktop\Temp\DateOrDinner\Vadblirdetförmat\Recepies.txt");
            foreach (string item in textFile)
            {
                string[] listOfMealEvent = item.Split('|');

                if (int.TryParse(listOfMealEvent[0], out int result))
                {
                    var s = new Meal
                    {
                        Time = result,
                        Servis = listOfMealEvent[1],
                        Place = listOfMealEvent[2],
                        Protein = listOfMealEvent[3],
                        Carbohydrates = listOfMealEvent[4],
                        Menu = listOfMealEvent[5],
                        Receipe = listOfMealEvent[6],
                        Instructions = listOfMealEvent[7],
                        Difficulty = int.Parse(listOfMealEvent[8]),
                        Flavoring = listOfMealEvent[9],
                        Vegetables = listOfMealEvent[10],
                    };
                    mealList.Add(s);
                }
            }
            return mealList;
        }
    

        private static void EnterFoodDate()
        {
            Console.WriteLine("Vänligen skriv in dagens datum (ÅÅ-MM-DD): ");

            string x = Console.ReadLine();

            DateTime validatedDate = ValidateNumber(x);
            Choices.Add(new Choice() { DinnerDate = validatedDate });

        }

        private static DateTime ValidateNumber(string x)
        {
            while (true)
            {
                Match match = Regex.Match(x, @"^[0-9]\d[-][0-9]\d[-][0-9]\d$");

                if (match.Success)
                {
                    DateTime date = DateTime.Parse(x);
                    return date;
                }
                else
                {
                    Console.WriteLine("Datumet är i fel format, skriv in igen: ");
                    x = Console.ReadLine();
                    continue;
                }
            }

        }

        private static void ShowDinnerPlaces(List<Meal>mealList)
        {
            int lastChoice = Choices.Count;
            string dateMonth = Choices[lastChoice - 1].DinnerDate.Month.ToString();
            string dateDay = Choices[lastChoice - 1].DinnerDate.Day.ToString();

            if ((dateMonth == "12" && dateDay == "24") || (dateMonth == "3" && dateDay == "24") || (dateMonth == "6" && dateDay == "21"))
            {
                GetHolidayDinner(dateMonth, mealList);
            }
            else if(int.Parse(dateDay) >= 25 && int.Parse(dateDay) <= 28)
            {
                Choices.Last().TimeSlot = 1;
                ShowPlaces(mealList);
            }
            else if (int.Parse(dateDay) >= 29 && int.Parse(dateDay) <= 31 || int.Parse(dateDay) >= 1 && int.Parse(dateDay) <= 10)
            {
                Choices.Last().TimeSlot = 2;
                ShowPlaces(mealList);
            }
            else if (int.Parse(dateDay) >= 11 && int.Parse(dateDay) <= 21)
            {
                Choices.Last().TimeSlot = 3;
                ShowPlaces(mealList);
            }
            else
            {
                Choices.Last().TimeSlot = 4;
                ShowPlaces(mealList);
            }
        }

        private static void ShowPlaces(List<Meal> mealList)
        {
            var showPlacesHome = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Places.Hemma.ToString()).Select(x=>x.Servis).Distinct().ToList();
            var showPlacesAway = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Places.Ute.ToString()).Select(x =>x.Servis).Distinct().ToList();
            PrintChoices(showPlacesHome, showPlacesAway);
        }

        private static void FriendOrFamily()
        {
            throw new NotImplementedException();
        }

        private static void GetHolidayDinner(string dateMonth, List<Meal> mealList)
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
            var showPlaces = mealList.Where(x => x.Menu == "Julbord" || x.Menu == "Påskbord" || x.Menu == "Midsommarmiddag").Select(x => x.Place).Distinct().ToList();
            PrintChoices(showPlaces);
        }

        private static void ShowProteinSources()
        {
            EnterChoice();
            throw new NotImplementedException();
        }

        private static void EnterChoice()
        {
            

            
            throw new NotImplementedException();
        }

        private static void ShowCarbSources()
        {
            EnterChoice();
            throw new NotImplementedException();
        }
        private static void ShowMenues(List<Meal> mealList)
        {
            
        }
        private static void ShowRecepies()
        {
            throw new NotImplementedException();
        }
        private static void PrintChoices(List<string> showPlacesHome, List<string> showPlacesAway)
        {
            int counter = 1;
            string[] choiceList = new string[showPlacesHome.Count + showPlacesAway.Count];
            Console.WriteLine("Hemma");
            foreach (var place in showPlacesHome)
            {
                choiceList[counter - 1] = $" {counter}. {place}";
                Console.Write(choiceList[counter - 1]);
                counter++;
            }

            Console.WriteLine("Ute");
            foreach (var place in showPlacesAway)
            {
                choiceList[counter - 1] = $" {counter}. {place}";
                Console.Write(choiceList[counter - 1]);
                counter++;
            }
            Console.Write("Gör ditt val från listorna: ");
            string placeChoice = Console.ReadLine();

            foreach (var choice in choiceList)
            {
                string[] splitArray = choice.Split(" ");
                if (placeChoice == splitArray[0])
                    Choices[0].Place = (Places)Enum.Parse(typeof(Places),splitArray[1]);

            }
        }
        private static void PrintChoices(List<string> showPlacesHome)
        {
            int counter = 1;
            string[] choiceList = new string[showPlacesHome.Count];
            
            foreach (var place in showPlacesHome)
            {
                choiceList[counter - 1] = $" {counter}. {place}";
                Console.Write(choiceList[counter - 1]);
                counter++;
            }

            
            Console.Write("Gör ditt val från listorna: ");
            string placeChoice = Console.ReadLine();

            foreach (var choice in choiceList)
            {
                string[] splitArray = choice.Split(" ");
                if (placeChoice == splitArray[0])
                    Choices[0].Place = (Places)Enum.Parse(typeof(Places), splitArray[1]);

            }
        }
        private static void EndOfProgram()
        {
            throw new NotImplementedException();
        }

    }

}
