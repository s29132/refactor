using System;
using System.Collections.Generic;
using LegacyApp;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {   //BL walidacja
            var manager = new UserDataManager(firstName, lastName, dateOfBirth, email);
            bool correct = ConfirmUserData(manager);
            if (!correct)
                return false;
            
            
            var client = GetClient(new ClientRepository(), firstName, lastName, dateOfBirth, email, clientId);
        
            //uzyc inteface zrobic liste objektow je implementujacych OPEN CLOSE principle 
            
            client.SetUpClient(new UserCreditService());
            
            
            bool isAddUser = manager.ClientCreditValidation(client);
            if (!isAddUser)
                return false;
            //IO
            UserDataAccess.AddUser(client);
            return true;
        }

        private User GetClient(IRepository repository, string firstName, string lastName, DateTime dateOfBirth, 
            string email, int clientId)
        {
            var client = repository.GetById(clientId);
            client.DateOfBirth = dateOfBirth; 
            client.EmailAddress = email;
            client.FirstName = firstName;
            client.LastName = lastName;
            return client;
        }

        private bool ConfirmUserData(IManager manager)
        {
            return manager.ValidateUserData();
        }
    }
}

internal class UserDataManager : IManager
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public UserDataManager(string firstName, string lastName, DateTime dateOfBirth, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.DateOfBirth = dateOfBirth;
    }
    public bool ValidateUserData()
    {
        if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName))
        {
            return false;
        }

        if (!this.Email.Contains("@") && !this.Email.Contains("."))
        {
            return false;
        }
            
        var now = DateTime.Now;
        int age = now.Year - this.DateOfBirth.Year;
        if (now.Month < this.DateOfBirth.Month || (now.Month == this.DateOfBirth.Month && now.Day < this.DateOfBirth.Day)) age--;

        if (age < 21)
        {
            return false;
        }

        return true;
    }

    public bool ClientCreditValidation(User client)
    {
        if (client.HasCreditLimit && client.CreditLimit < 500)
        {
            return false;
        }

        return true;
    }
}
