using System;
using System.Collections.Generic;

namespace Vadblirdetförmat
{
    List<Choice> Choices = new List<Choice>();
    class Program
    {
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

        private static void EnterFoodDate()
        {
            Console.WriteLine("Vänligen skriv in dagens datum: ");
            Console.ReadLine();
        }

        private static void ShowDinnerPlaces()
        {
            if (false)
            {
                GetHolidayDinner();
            }
            else if(false)
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
