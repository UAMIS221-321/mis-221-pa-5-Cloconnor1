namespace ClassDefinitions
{


class Trainer
{
   

    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string MailingAddress { get; set; }

    public static List<Trainer> LoadTrainers()
    {
        List<Trainer> trainers = new List<Trainer>();

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

        return trainers;
    }
}

public class Transaction
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime TrainingDate { get; set; }
    public int TrainerID { get; set; }
    public string TrainerName { get; set; }
    public TransactionStatus Status { get; set; }
}

public enum TransactionStatus
{
    Pending,
    Completed,
    Cancelled
}

public class Listing
{
    public int ID { get; set; }
    public string TrainerName { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan SessionTime { get; set; }
    public double SessionCost { get; set; }

    
   }
}

