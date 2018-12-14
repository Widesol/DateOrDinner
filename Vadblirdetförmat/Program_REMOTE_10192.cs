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
            while (true)
            {
                try
                {
                    StartApp(); //Välkomsthälsning
                    List<Meal> mealList=ReadTextFile();
                    EnterFoodDate(); // Användaren matar in datum 
                    ShowDinnerPlaces(mealList); // Användaren får alternativ på var maten
                    ShowProteinSources(mealList);
                    (string placeChoice, string[] menueArray, int numberChoice)= ShowMenues(mealList);
                    EnterChoice(placeChoice, menueArray, numberChoice);
                    ShowRecepies(mealList);
                    bool oneMoreTime = EndOfProgram();
                    if (oneMoreTime == true)
                        break;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Oooops... Ring POOLIA!! Nej nej nej AW menar jag. Tyvärr du måste börja om.");
                    Console.ForegroundColor = ConsoleColor.White;

                }

            }

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
                        Servis = listOfMealEvent[2],
                        Place = listOfMealEvent[1],
                        Protein = listOfMealEvent[3],
                        Menu = listOfMealEvent[4],
                        Recepie = listOfMealEvent[5],
                        Instructions = listOfMealEvent[6],
                        Difficulty = listOfMealEvent[7],
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
            int numberOfChoice = Choices.Count;
            var showPlacesHome = mealList.Where(x => x.Time == Choices[numberOfChoice - 1].TimeSlot && x.Place == Places.Hemma.ToString()).Select(x=>x.Servis).Distinct().ToList();
            var showPlacesAway = mealList.Where(x => x.Time == Choices[numberOfChoice - 1].TimeSlot && x.Place == Places.Ute.ToString()).Select(x =>x.Servis).Distinct().ToList();
            var enteredChoice = PrintChoices(showPlacesHome, showPlacesAway);
            EnterChoice(enteredChoice.Item1, enteredChoice.Item2, enteredChoice.Item3);
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
            EnterChoice(enteredChoice.Item1, enteredChoice.Item2, enteredChoice.Item3);
        }

        private static void ShowProteinSources(List<Meal> mealList)
        {
            var showProtein = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Choices.Last().Place.ToString()).Select(x => x.Protein).Distinct().ToList();
           (string placeChoice, string[] choiceList, int numberchoice) = PrintChoices(showProtein);
            EnterChoice(placeChoice, choiceList, numberchoice); 
        }

        private static (string placeChoice, string[] menueArray, int numberChoice) ShowMenues(List<Meal> mealList)
        {
            var showMenues = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Choices.Last().Place.ToString()&& x.Protein==Choices.Last().Proteinsource.ToString()).Select(x => x.Menu).Distinct().ToList();

            int countMenues = showMenues.Count;
           
            string[] menueArray = new string[countMenues + 1];
            for (int i = 0; i < countMenues; i++)
            {
                menueArray[i]=($"{i + 1} {showMenues[i]}");
                Console.WriteLine(menueArray[i]);

            }
            menueArray[countMenues] = $"{countMenues} Nytt menyförslag";
                Console.WriteLine(menueArray[countMenues]);


            Console.WriteLine("Välj ett av de förslag framtagna just för dig och din aktuella livssituation");


            string placeChoice = Console.ReadLine();
            return (placeChoice, menueArray, 3);




        }

        private static void ShowRecepies(List<Meal> mealList)
        {
           var recepie = mealList.Where(x => x.Time == Choices.Last().TimeSlot && x.Place == Choices.Last().Place.ToString() && x.Protein == Choices.Last().Proteinsource.ToString() && x.Menu == Choices.Last().Menues).Select(x => x).ToList();
            Console.WriteLine("Recept: ");
            Console.WriteLine($"{recepie[0].Recepie}");
            Console.WriteLine();
            Console.WriteLine("Tilllagning: ");
            Console.WriteLine($"{recepie[0].Instructions}");
            Console.WriteLine("Smaklig måltid!");
        }

        private static (string, string[], int) PrintChoices(List<string> showPlacesHome, List<string> showPlacesAway)
        {
            int counter = 1;
            string[] choiceList = new string[showPlacesHome.Count + showPlacesAway.Count];
            Console.WriteLine();
            Console.WriteLine("Vill du kanske äta hemma idag?");
            foreach (var place in showPlacesHome)
            {
                choiceList[counter - 1] = $"{counter} {place}";
                Console.WriteLine(choiceList[counter - 1]);
                counter++;
            }
            Console.WriteLine();
            Console.WriteLine("Eller vill du lyxa till det och äta ute?");
            foreach (var place in showPlacesAway)
            {
                choiceList[counter - 1] = $"{counter} {place}";
                Console.WriteLine(choiceList[counter - 1]);
                counter++;
            }
            Console.WriteLine();
            
            Console.Write("Gör ditt val från listorna: ");
            string placeChoice = Console.ReadLine();
            return (placeChoice, choiceList, 1);
        }

        private static (string, string[], int) PrintChoices(List<string> showPlacesHome)
        {
            int counter = 1;
            string[] choiceList = new string[showPlacesHome.Count];
            
            foreach (var place in showPlacesHome)
            {
                choiceList[counter - 1] = $"{counter} {place}";
                Console.WriteLine(choiceList[counter - 1]);
                counter++;
                //gör en array lika lång som listan vi skickat in och tilldelar alternativen en plats i arrayen

            }

            
            Console.Write("Välj från listan nedan: ");
            string placeChoice = Console.ReadLine();
            return (placeChoice, choiceList, 2);


        }

        private static void EnterChoice(string placeChoice, string[] choiceList, int numberChoice) 
        {
            foreach (var choice in choiceList)
            {
                string[] splitArray = choice.Split(" ");
                if (placeChoice == splitArray[0] && numberChoice == 1)
                    Choices.Last().Servis = (Serv)Enum.Parse(typeof(Serv), splitArray[1]);
                else if(placeChoice == splitArray[0] && numberChoice == 2)
                    Choices.Last().Proteinsource = (Protein)Enum.Parse(typeof(Protein), splitArray[1]);
                else if(placeChoice == splitArray[0] && numberChoice == 3)
                    Choices.Last().Menues = splitArray[1];



            }
        }

        private static bool EndOfProgram()
        {
            Console.WriteLine("Vill du välja mat för en dag till? Ja, eller Nej: ");
            string oneMoreDinner = Console.ReadLine();
            bool answerOneMoreDinner;
            if (oneMoreDinner.ToUpper() == "JA")
                answerOneMoreDinner = true;
            else
                answerOneMoreDinner = false;
            return answerOneMoreDinner;
                
        }

    }

}
