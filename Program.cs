using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassDefinitions;


public class Program
{
    private const string TRAINERS_FILE = "trainers.txt";
    private const string LISTINGS_FILE = "listings.txt";
    private const string TRANSACTIONS_FILE = "transactions.txt";

    private static List<Trainer> trainers;
    private static List<Listing> listings;
    private static List<Transaction> transactions;

    static void Main(string[] args)
    {
        trainers = new List<Trainer>();
        listings = new List<Listing>();
        transactions = new List<Transaction>();

        LoadData();

        bool exit = false;
        while (!exit)
        {
           
            Console.WriteLine("Train Like A Champion - Personal Fitness");
            Console.WriteLine("1. Manage trainer data");
            Console.WriteLine("2. Manage listing data");
            Console.WriteLine("3. Manage customer booking data");
            Console.WriteLine("4. Run reports");
            Console.WriteLine("5. Exit the application");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ManageTrainerData();
                    break;
                case "2":
                    Console.Clear();
                    ManageListingData();
                    break;
                case "3":
                    Console.Clear();
                    ManageCustomerBookingData();
                    break;
                case "4":
                    Console.Clear();
                    RunReports();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }

            SaveData();
        }
    }

    private static void LoadData()
    {
        // Load trainers from file
        if (File.Exists(TRAINERS_FILE))
        {
            string[] lines = File.ReadAllLines(TRAINERS_FILE);
            foreach (string line in lines)
            {
                string[] fields = line.Split('#');
                Trainer trainer = new Trainer
                {
                    ID = int.Parse(fields[0]),
                    Name = fields[1],
                    Email = fields[2],
                    MailingAddress = fields[3]
                };
                trainers.Add(trainer);
            }
        }

        // Load listings from file
        if (File.Exists(LISTINGS_FILE))
        {
            string[] lines = File.ReadAllLines(LISTINGS_FILE);
            foreach (string line in lines)
            {
                string[] fields = line.Split('#');
                Listing listing = new Listing
                {
                    ID = int.Parse(fields[0]),
                    TrainerName = fields[1],
                    SessionDate = DateTime.Parse(fields[2]),
                    SessionTime = TimeSpan.Parse(fields[3]),
                    Cost = decimal.Parse(fields[4]),
                    Taken = bool.Parse(fields[5])
                };
                listings.Add(listing);
            }
        }

        // Load transactions from file
        if (File.Exists(TRANSACTIONS_FILE))
        {
            string[] lines = File.ReadAllLines(TRANSACTIONS_FILE);
            foreach (string line in lines)
            {
                string[] fields = line.Split('#');
                Transaction transaction = new Transaction
                {
                    ID = int.Parse(fields[0]),
                    CustomerName = fields[1],
                    CustomerEmail = fields[2],
                    TrainingDate = DateTime.Parse(fields[3]),
                    TrainerID = int.Parse(fields[4]),
                    TrainerName = fields[5],
                    Status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), fields[6])
                };
                transactions.Add(transaction);
            }
        }
    }

    private static void SaveData()
    {
        // Save trainers to file
        string[] trainerLines = trainers.Select(t => t.ToString()).ToArray();
        File.WriteAllLines(TRAINERS_FILE, trainerLines);

        // Save listings to file
        string[] listingLines = listings.Select(l => l.ToString()).ToArray();
    }

static void ManageTrainerData(){
    bool exit = false;

    do
    {
        Console.WriteLine("TRAINER MENU");
        Console.WriteLine("1. Add Trainer");
        Console.WriteLine("2. Edit Trainer");
        Console.WriteLine("3. Delete Trainer");
        Console.WriteLine("4. Return to Main Menu");
        Console.Write("Enter your selection: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddTrainer();
                Console.Clear();
                break;
            case "2":
                EditTrainer();
                Console.Clear();
                break;
            case "3":
                DeleteTrainer();
                Console.Clear();
                break;
            case "4":
                exit = true;
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
    } while (!exit);
}

static void AddTrainer()
{
    Console.WriteLine("ADD TRAINER");

    Console.Write("Enter Trainer ID: ");
    string id = Console.ReadLine();

    Console.Write("Enter Trainer Name: ");
    string name = Console.ReadLine();

    Console.Write("Enter Trainer Email Address: ");
    string email = Console.ReadLine();

    Console.Write("Enter Trainer Mailing Address: ");
    string mailingAddress = Console.ReadLine();

    Trainer trainer = new Trainer(id, name, email, mailingAddress);
    trainers.Add(trainer);

    Console.WriteLine("Trainer added successfully.");
}

static void EditTrainer()
{
    Console.WriteLine("EDIT TRAINER");

    Console.Write("Enter Trainer ID: ");
    string id = Console.ReadLine();

    Trainer trainer = trainers.FirstOrDefault(t => t.ID == id);

    if (trainer != null)
    {
        Console.Write("Enter Trainer Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Trainer Email Address: ");
        string email = Console.ReadLine();

        Console.Write("Enter Trainer Mailing Address: ");
        string mailingAddress = Console.ReadLine();

        trainer.Name = name;
        trainer.Email = email;
        trainer.MailingAddress = mailingAddress;

        Console.WriteLine("Trainer updated successfully.");
    }
    else
    {
        Console.WriteLine("Trainer not found.");
    }
}

static void DeleteTrainer()
{
    Console.WriteLine("DELETE TRAINER");

    Console.Write("Enter Trainer ID: ");
    string id = Console.ReadLine();

    Trainer trainer = trainers.FirstOrDefault(t => t.ID == id);

    if (trainer != null)
    {
        trainers.Remove(trainer);
        Console.WriteLine("Trainer deleted successfully.");
    }
    else
    {
        Console.WriteLine("Trainer not found.");
    }
}

static void LoadTrainersFromFile()
{
    try
    {
        string[] lines = File.ReadAllLines("trainers.txt");

        foreach (string line in lines)
        {
            string[] fields = line.Split('#');
            string id = fields[0];
            string name = fields[1];
            string email = fields[2];
            string mailingAddress = fields[3];

            Trainer trainer = new Trainer(id, name, email, mailingAddress);
            trainers.Add(trainer);
        }

        Console.WriteLine("Trainers loaded successfully.");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Trainers file not found.");
    }
}

private static void ManageListingData()
{
    Console.WriteLine("\n===== Manage Listing Data =====");
    Console.WriteLine("1. Add Listing");
    Console.WriteLine("2. Edit Listing");
    Console.WriteLine("3. Delete Listing");
    Console.WriteLine("4. View All Listings");
    Console.WriteLine("5. Return to Main Menu");
    Console.Write("Enter your choice: ");
    int choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            AddListing();
            Console.Clear();
            break;
        case 2:
            EditListing();
            Console.Clear();
            break;
        case 3:
            DeleteListing();
            Console.Clear();
            break;
        case 4:
            ViewAllListings();
            Console.Clear();
            break;
        case 5:
            return;
        default:
            Console.WriteLine("Invalid choice. Please enter a valid choice.");
            ManageListingData();
            break;
    }
}

private static void AddListing()
{
    Console.WriteLine("\n===== Add Listing =====");
    Console.Write("Enter Listing ID: ");
    string listingId = Console.ReadLine();
    Console.Write("Enter Trainer Name: ");
    string trainerName = Console.ReadLine();
    Console.Write("Enter Date of the Session: ");
    DateTime sessionDate = DateTime.Parse(Console.ReadLine());
    Console.Write("Enter Time of the Session: ");
    TimeSpan sessionTime = TimeSpan.Parse(Console.ReadLine());
    Console.Write("Enter Cost of the Session: ");
    double sessionCost = double.Parse(Console.ReadLine());

    // Check if listing already exists
    bool listingExists = false;
    foreach (Listing listing in listings)
    {
        if (listing.ListingId == listingId)
        {
            listingExists = true;
            break;
        }
    }
    if (listingExists)
    {
        Console.WriteLine("Listing with the same ID already exists. Please try again.");
        AddListing();
        return;
    }

    // Get trainer ID
    string trainerId = null;
    foreach (Trainer trainer in trainers)
    {
        if (trainer.Name == trainerName)
        {
            trainerId = trainer.Id;
            break;
        }
    }
    if (trainerId == null)
    {
        Console.WriteLine("Trainer not found. Please try again.");
        AddListing();
        return;
    }

    // Add listing to listings list
    listings.Add(new Listing(listingId, trainerId, trainerName, sessionDate, sessionTime, sessionCost, false));
    Console.WriteLine("Listing added successfully.");

    // Save listings to file
    string listingsData = "";
    foreach (Listing listing in listings)
    {
        listingsData += $"{listing.ListingId}#{listing.TrainerName}#{listing.SessionDate.ToShortDateString()}#{listing.SessionTime.ToString()}#{listing.SessionCost}#{listing.IsTaken}\n";
    }
    File.WriteAllText("listings.txt", listingsData);
}

private void EditListing()
{
    Console.WriteLine("\nEnter the ID of the listing to edit:");
    int id = int.Parse(Console.ReadLine());

    for (int i = 0; i < listings.Count; i++)
    {
        if (listings[i].Id == id)
        {
            Console.WriteLine("\nEnter the new trainer name:");
            string trainerName = Console.ReadLine();
            Console.WriteLine("Enter the new date of the session (mm/dd/yyyy):");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new time of the session (hh:mm AM/PM):");
            string time = Console.ReadLine();
            Console.WriteLine("Enter the new cost of the session:");
            decimal cost = decimal.Parse(Console.ReadLine());

            listings[i].TrainerName = trainerName;
            listings[i].Date = date;
            listings[i].Time = time;
            listings[i].Cost = cost;
            Console.WriteLine("\nListing updated successfully.");
            SaveListings();
            return;
        }
    }

    Console.WriteLine("\nListing not found.");
}

private void DeleteListing()
{
    Console.WriteLine("\nEnter the ID of the listing to delete:");
    int id = int.Parse(Console.ReadLine());

    for (int i = 0; i < listings.Count; i++)
    {
        if (listings[i].Id == id)
        {
            listings.RemoveAt(i);
            Console.WriteLine("\nListing deleted successfully.");
            SaveListings();
            return;
        }
    }

    Console.WriteLine("\nListing not found.");
}

private void ViewAllListings()
{
    Console.WriteLine("\nAll Listings:");

    foreach (Listing listing in listings)
    {
        Console.WriteLine(listing.ToString());
    }
}

private void ManageCustomerBookingData()
{
    while (true)
    {
        Console.WriteLine("\nMANAGE CUSTOMER BOOKING DATA");
        Console.WriteLine("1. View available training sessions");
        Console.WriteLine("2. Book a session");
        Console.WriteLine("3. View booked sessions");
        Console.WriteLine("4. Cancel a booked session");
        Console.WriteLine("5. Exit");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Clear();
                ViewAvailableSessions();
                break;
            case 2:
                Console.Clear();
                BookSession();
                break;
            case 3:
                Console.Clear();
                ViewBookedSessions();
                break;
            case 4:
                Console.Clear();
                CancelSession();
                break;
            case 5:
                Console.Clear();
                return;
            default:
                Console.WriteLine("\nInvalid choice. Please try again.");
                break;
        }
    }
}

private void ViewAvailableSessions()
{
    Console.WriteLine("\nAVAILABLE TRAINING SESSIONS:");
    Console.WriteLine("ID | Trainer | Date | Time | Cost");

    foreach (Listing listing in listings)
    {
        if (!listing.IsTaken)
        {
            Console.WriteLine(listing.Id + " | " + listing.TrainerName + " | " + listing.Date.ToShortDateString() + " | " + listing.Time + " | " + listing.Cost.ToString("C"));
        }
    }
}

private void BookSession()
{
    Console.WriteLine("\nBOOK A SESSION:");
    Console.WriteLine("Enter your name:");
    string customerName = Console.ReadLine();
    Console.WriteLine("Enter your email:");
    string customerEmail = Console.ReadLine();
    Console.WriteLine("Enter the ID of the session you want to book:");
    int id = int.Parse(Console.ReadLine());

    Listing listingToBook = null;
    foreach (Listing listing in listings)
    {
        if (listing.Id == id)
        {
            if (!listing.IsTaken)
            {
                listing.IsTaken = true;
                listingToBook = listing;
                Console.WriteLine("\nSession booked successfully.");
                SaveListings();
                break;
            }
            else
            {
                Console.WriteLine("\nSorry, that session is already taken.");
                return;
            }
        }
    }

    if (listingToBook == null)
    {
        Console.WriteLine("\nSession not found.");
        return;
    }

    int transactionId = transactions.Count == 0 ? 1 : transactions.Max(t => t.Id) + 1;
    Transaction transaction = new Transaction(transactionId, customerName, customerEmail, listingToBook.Date, listingToBook.TrainerId, listingToBook.TrainerName, "Booked");
    transactions.Add(transaction);
    SaveTransactions();
}

private void ViewBookedSessions()
{
    Console.WriteLine("\nBOOKED TRAINING SESSIONS:");
    Console.WriteLine("ID | Customer | Email | Trainer | Date | Time | Cost");

    foreach (Transaction transaction in transactions)
    {
        if (transaction.Status == "Booked")
        {
            Listing listing = listings.Find(l => l.Id == transaction.ListingId);
            Console.WriteLine(transaction.Id + " | " + transaction.CustomerName + " | " + transaction.CustomerEmail + " | " + listing.TrainerName + " | " + listing.Date.ToShortDateString() + " | " + listing.Time + " | " + listing.Cost.ToString("C"));
        }
    }
}

private static void CancelSession()
{
    Console.WriteLine("Enter the session ID to cancel:");
    int sessionId = int.Parse(Console.ReadLine());

    Transaction transactionToCancel = transactions.FirstOrDefault(t => t.SessionId == sessionId);

    if (transactionToCancel == null)
    {
        Console.WriteLine("Session ID not found.");
        return;
    }

    if (transactionToCancel.Status == "Completed")
    {
        Console.WriteLine("Session has already been completed.");
        return;
    }

    transactionToCancel.Status = "Cancelled";
    Console.WriteLine("Session has been cancelled.");
}

private static void RunReports()
{
    Console.WriteLine("Choose a report to generate:");
    Console.WriteLine("1. Individual Customer Sessions");
    Console.WriteLine("2. Historical Customer Sessions");
    Console.WriteLine("3. Historical Revenue Report");

    int reportChoice = int.Parse(Console.ReadLine());

    switch (reportChoice)
    {
        case 1:
            Console.Clear();
            GenerateIndividualCustomerSessionsReport();
            break;
        case 2:
            Console.Clear();
            GenerateHistoricalCustomerSessionsReport();
            break;
        case 3:
            Console.Clear();
            GenerateHistoricalRevenueReport();
            break;
        default:
            Console.WriteLine("Invalid report choice.");
            break;
    }
}

private static void GenerateIndividualCustomerSessionsReport()
{
    Console.WriteLine("Enter the customer email address:");
    string customerEmail = Console.ReadLine();

    var customerSessions = transactions.Where(t => t.CustomerEmail == customerEmail);

    Console.WriteLine($"Sessions for customer {customerEmail}:");
    foreach (var session in customerSessions)
    {
        Console.WriteLine($"{session.SessionId}#{session.CustomerName}#{session.CustomerEmail}#{session.TrainingDate}#{session.TrainerId}#{session.TrainerName}#{session.Status}");
    }
}

private static void GenerateHistoricalCustomerSessionsReport()
{
    var sortedTransactions = transactions.OrderBy(t => t.CustomerEmail).ThenBy(t => t.TrainingDate);

    string currentCustomerEmail = null;
    int totalSessions = 0;

    Console.WriteLine("Historical Customer Sessions:");

    foreach (var session in sortedTransactions)
    {
        if (session.CustomerEmail != currentCustomerEmail)
        {
            if (currentCustomerEmail != null)
            {
                Console.WriteLine($"Total sessions for {currentCustomerEmail}: {totalSessions}");
                Console.WriteLine();
            }

            currentCustomerEmail = session.CustomerEmail;
            totalSessions = 0;
            Console.WriteLine($"Sessions for customer {currentCustomerEmail}:");
        }

        Console.WriteLine($"{session.SessionId}#{session.CustomerName}#{session.CustomerEmail}#{session.TrainingDate}#{session.TrainerId}#{session.TrainerName}#{session.Status}");
        totalSessions++;
    }

    if (currentCustomerEmail != null)
    {
        Console.WriteLine($"Total sessions for {currentCustomerEmail}: {totalSessions}");
    }
}

private static void GenerateHistoricalRevenueReport()
{
    var sortedTransactions = transactions.OrderBy(t => t.TrainingDate);

    int currentYear = 0;
    int currentMonth = 0;
    decimal totalRevenue = 0;

    Console.WriteLine("Historical Revenue Report:");

    foreach (var session in sortedTransactions)
    {
        int year = session.TrainingDate.Year;
        int month = session.TrainingDate.Month;

        if (year != currentYear || month != currentMonth)
        {
            if (currentYear != 0 || currentMonth != 0)
            {
                Console.WriteLine($"Total revenue for {currentYear}-{currentMonth}: ${totalRevenue}");
                Console.WriteLine();
            }

            currentYear = year;
            currentMonth = month;
            totalRevenue = 0;

            Console.WriteLine($"Revenue for {currentYear}-{currentMonth}:");
        }

        if (session.Status = "Completed")
        {
            totalRevenue += session.Cost;
        }
    }

    if (currentYear != 0 || currentMonth != 0)
    {
        Console.WriteLine($"Total revenue for {currentYear}-{currentMonth}: ${totalRevenue}");
    }
}



}


