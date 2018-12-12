using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vadblirdetförmat
{
    class Program : Choice
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
            throw new NotImplementedException();
        }

        public static void EnterFoodDate()
        {
            var dateFormat = "dd-MM-yyyy";
            Console.WriteLine("Vänligen skriv in dagens datum: ");
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

            //DateTime dt;

            //bool isValid = DateTime.TryParseExact(
            //    "dd-MM-yyyy",
            //    out dt);

        }

        private static void ShowDinnerPlaces()
        {
            if (false)
            {
                GetHolidayDinner();
            }
            else if (false)
            {
                ShowPlaces();
            }
            else if (false)
            {
                ShowPlaces();
            }
            else if (false)
            {
                ShowPlaces();
            }
            else
            {
                ShowPlaces();
            }
        }

        private static void ShowPlaces()
        {
            //var showPlaces = ReadTextFil.Select(x => x.Place).Distinct();
            PrintChoices(showPlaces);
            EnterChoice();
            FriendOrFamily();
        }

        private static void FriendOrFamily()
        {
            throw new NotImplementedException();
        }

        private static void GetHolidayDinner()
        {
            if (true)
            {
                ShowPlaces();
                //Om det är Jul, Påsk eller Midsommar.

            }
        }

        private static void ShowProteinSources()
        {
            EnterChoice();
            throw new NotImplementedException();
        }

        private static void EnterChoice()
        {
            if (3)


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
