// namespace CodeFolder.DataModels;

public class AccountModel
{
    private string Name { get; set; }
    private string Surname { get; set; }
    private string DateOfBirth { get; set; }
    private string Password { get; set; }
    
    public AccountModel (int id, string name, string surname, string dateOfBirth, string password)
    {
        // this.ID = id++;
        this.Name = name;
        this.Surname = surname;
        this.DateOfBirth = dateOfBirth;
        this.Password = password;
        //list of previous commutes by plane, can only be implemented after the tickets have been made :)
        // List<Ticket> tripHistory = new List<Ticket>();
    }

}