namespace EatvardAPI;
using Models;
using Data;
using Microsoft.EntityFrameworkCore;

public class PopulateDatabase
{
    EatvardContext _context;

    public PopulateDatabase(EatvardContext dbContext) 
    {
        _context = dbContext;
    }

    private void AddUser(string firstName, string lastName, string Email)
    {
        UserAccount newAccount = new UserAccount()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = Email
        };
        _context.Users.Add(newAccount);
    }

    public void AddRestaurant(string name, string address, string city)
    {
        Restaurant restaurant = new Restaurant()
        {
            Name = name,
            Address = address,
            City = city
        };
        _context.Restaurants.Add(restaurant);
    }

    //public void AddPost(string Author)

    public void SaveChanges()
    {
        int written = _context.SaveChanges();
        Console.WriteLine(written);
    }
}
