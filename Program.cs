using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class BankAccount
{
    public required string FullName { get; set; }
    public required string AccountNumber { get; set; }
    public required string AccountType { get; set; }
    public decimal AmountBalance { get; set; }
    public required string Note { get; set; }
}

class User
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

class Bank
{
    private List<BankAccount> accounts;
    private List<User> users;
    public User? currentUser;

    public Bank()
    {
        accounts = new List<BankAccount>();
        users = new List<User>();
        currentUser = null;
    }

    public void Register(string username, string password)
    {
        User user = new User
        {
            Username = username,
            Password = password
        };

        users.Add(user);
        Console.WriteLine("User registration successful.");
        
    }

    public void Login(string username, string password)
    {
        User user = FindUser(username);
        if (user != null && user.Password == password)
        {
            currentUser = user;
            Console.WriteLine("Login successful.");
            
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
            
        }
    }

    public void Logout()
    {
        currentUser = null;
        Console.WriteLine("Logout successful.");
       
    }

    public void AddAccount(BankAccount account)
    {
        accounts.Add(account);
        Console.WriteLine("Account creation successful.");
        
    }

    public void Deposit(string accountNumber, decimal amount)
    {
        if (currentUser != null)
        {
            BankAccount account = FindAccount(accountNumber);
            if (account != null)
            {
                account.AmountBalance += amount;
                account.Note = "Deposit";
                Console.WriteLine("Deposit successful.");
                
            }
            else
            {
                Console.WriteLine("Account not found.");
                
            }
        }
        else
        {
            Console.WriteLine("Please log in to perform transactions.");
        }
    }

    public void Withdraw(string accountNumber, decimal amount)
    {
        if (currentUser != null)
        {
            BankAccount account = FindAccount(accountNumber);
            if (account != null)
            {
                if (account.AccountType == "Savings" && (account.AmountBalance - amount) < 1000)
                {
                    Console.WriteLine("Invalid transaction. Minimum balance must be maintained for savings account.");
                }
                else if (account.AccountType == "Current" || (account.AccountType == "Savings" && account.AmountBalance - amount >= 1000))
                {
                    account.AmountBalance -= amount;
                    account.Note = "Withdrawal";
                    Console.WriteLine("Withdrawal successful.");
                    
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
                
            }
        }
        else
        {
            Console.WriteLine("Please log in to perform transactions.");
        }
    }

    public void DisplayAccounts()
    {
        Console.WriteLine("|-------------------|-------------------------------|--------------------------|---------------------|----------|");
        Console.WriteLine("| FULL NAME         | ACCOUNT NUMBER                | ACCOUNT TYPE             | AMOUNT BAL          | NOTE     |");
        Console.WriteLine("|-------------------|-------------------------------|--------------------------|---------------------|----------|");
        foreach (BankAccount account in accounts)
        {
            Console.WriteLine("| {0,-17} | {1,-30} | {2,-24} | {3,-19} | {4,-8} |", account.FullName, account.AccountNumber,
                account.AccountType, account.AmountBalance, account.Note);
        }
        Console.WriteLine("|-------------------|-------------------------------|--------------------------|---------------------|----------|");
    }

    private BankAccount? FindAccount(string accountNumber)
    {
        foreach (BankAccount account in accounts)
        {
            if (account.AccountNumber == accountNumber)
            {
                return account;
            }
        }
        return null;
    }

    private User? FindUser(string username)
    {
        foreach (User user in users)
        {
            if (user.Username == username)
            {
                return user;
            }
        }
        return null;
    }
}

class Program
{
    static void Main()
    {
        Bank bank = new Bank();

        Console.WriteLine("Welcome to the Bank Application!");

        while (true)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("\nUser Registration");
                    Console.Write("Username: ");
                    string registerUsername = Console.ReadLine();
                    Console.Write("Password: ");
                    string registerPassword = Console.ReadLine();
                    bank.Register(registerUsername, registerPassword);
                    break;
                case "2":
                    Console.WriteLine("\nUser Login");
                    Console.Write("Username: ");
                    string loginUsername = Console.ReadLine();
                    Console.Write("Password: ");
                    string loginPassword = Console.ReadLine();
                    bank.Login(loginUsername, loginPassword);
                    break;
                case "3":
                    Console.WriteLine("\nThank you for using the Bank Application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            if (bank.currentUser != null)
            {
                // Create accounts
                BankAccount account1 = new BankAccount
                {
                    FullName = "John Doe",
                    AccountNumber = "0987654321",
                    AccountType = "Savings",
                    AmountBalance = 10000,
                    Note = "Gift"
                };

                BankAccount account2 = new BankAccount
                {
                    FullName = "John Doe",
                    AccountNumber = "0987654311",
                    AccountType = "Current",
                    AmountBalance = 100000,
                    Note = "Food"
                };

                // Add accounts to the bank
                bank.AddAccount(account1);
                bank.AddAccount(account2);

                while (true)
                {
                    Console.WriteLine("\nPlease select an option:");
                    Console.WriteLine("1. Deposit");
                    Console.WriteLine("2. Withdraw");
                    Console.WriteLine("3. Display Accounts");
                    Console.WriteLine("4. Logout");

                    string userOption = Console.ReadLine();

                    switch (userOption)
                    {
                        case "1":
                            Console.Write("\nEnter the account number: ");
                            string depositAccountNumber = Console.ReadLine();
                            Console.Write("Enter the amount to deposit: ");
                            decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                            bank.Deposit(depositAccountNumber, depositAmount);
                            break;
                        case "2":
                            Console.Write("\nEnter the account number: ");
                            string withdrawAccountNumber = Console.ReadLine();
                            Console.Write("Enter the amount to withdraw: ");
                            decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                            bank.Withdraw(withdrawAccountNumber, withdrawAmount);
                            break;
                        case "3":
                            Console.WriteLine("\nAccount Details:");
                            bank.DisplayAccounts();
                            break;
                        case "4":
                            bank.Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                    if (userOption == "4")
                    {
                        break;
                    }
                }
            }
        }
    }
}

