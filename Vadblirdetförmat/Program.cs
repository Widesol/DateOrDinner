﻿using System;
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
            ShowProteinSources(mealList);
           (int placeChoice, string[] menueArray)= ShowMenues(mealList);
            ShowRecepies(mealList);
            EndOfProgram();

        }
      
        private static void StartApp()
        {
            Console.WriteLine("Är du hungrig? Välkommen, här hittar du recept som passar dig!");
        }

        private static List <Meal>  ReadTextFile()
        {
            List<Meal> mealList = new List<Meal>();
            string[] textFile = File.ReadAllLines(@"Recepies.txt");
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
                        Menu = listOfMealEvent[4],
                        Receipe = listOfMealEvent[5],
                        Instructions = listOfMealEvent[6],
                        Difficulty = int.Parse(listOfMealEvent[7]),
                        Flavoring = listOfMealEvent[8],
                        Vegetables = listOfMealEvent[9],
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
            var enteredChoice = PrintChoices(showPlacesHome, showPlacesAway);
            EnterChoice(enteredChoice.Item1, enteredChoice.Item2);
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
            var enteredChoice = PrintChoices(showPlaces);
            EnterChoice(enteredChoice.Item1, enteredChoice.Item2);
        }

        private static void ShowProteinSources(List<Meal> mealList)
        {
            var showProtein = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Choices.Last().Place.ToString()).Select(x => x.Protein).Distinct().ToList();
           (string placeChoice, string[] choiceList) = PrintChoices(showProtein);
            EnterChoice(placeChoice, choiceList); 
        }

        private static (string placeChoice, string[] menueArray) ShowMenues(List<Meal> mealList)
        {
            var showMenues = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Choices.Last().Place.ToString()&& x.Protein==Choices.Last().Proteinsource.ToString()).Select(x => x.Menu).Distinct().ToList();
            int counter;
            
            
           
            string[] menueArray = new string[4];
            for (int i = 0; i < 3; i++)
            {
                menueArray[i]=($"{i}. {showMenues[i]}");
                Console.WriteLine(menueArray[i]);

            }
            menueArray[4] = "4. Nytt menyförslag";
                Console.WriteLine($"4. Nytt menyförslag");


            Console.WriteLine("Välj ett av de förslag framtagna just för dig och din aktuella livssituation");

            int placeChoice = int.Parse(Console.ReadLine());
            return (placeChoice, menueArray);




        }

        private static void ShowRecepies(List<Meal> mealList)
        {
            var recepie = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Choices.Last().Place.ToString() && x.Protein == Choices.Last().Proteinsource.ToString() /*&& x.Menu == Choices.Last().Menu.Tostring()*/).Select(x => new { x.Receipe, x.Instructions }).ToArray();

            Console.WriteLine("Recept: ");
            Console.WriteLine($"{recepie[0]}");
            Console.WriteLine();
            Console.WriteLine("Tilllagning: ");
            Console.WriteLine($"{recepie[1]}");
            Console.WriteLine("Smaklig måltid!");
        }

        private static (string, string[]) PrintChoices(List<string> showPlacesHome, List<string> showPlacesAway)
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
            return (placeChoice, choiceList);
        }

        private static (string, string[]) PrintChoices(List<string> showPlacesHome)
        {
            int counter = 1;
            string[] choiceList = new string[showPlacesHome.Count];
            
            foreach (var place in showPlacesHome)
            {
                choiceList[counter - 1] = $" {counter}. {place}";
                Console.Write(choiceList[counter - 1]);
                counter++;
                //gör en array lika lång som listan vi skiskat in och tilldelar alternativen en plats i arrayen

            }

            
            Console.Write("Gör ditt val från listorna: ");
            string placeChoice = Console.ReadLine();
            return (placeChoice, choiceList);


        }

        private static void EnterChoice(string placeChoice, string[] choiceList) 
        {
            foreach (var choice in choiceList)
            {
                string[] splitArray = choice.Split(" ");
                if (placeChoice == splitArray[0])
                    Choices.Last().Place = (Places)Enum.Parse(typeof(Places), splitArray[1]);

            }
        }

        private static void EndOfProgram()
        {
            throw new NotImplementedException();
        }

    }

}
