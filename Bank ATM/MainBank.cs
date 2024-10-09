using System;
using System.IO;

namespace Bank_ATM
{
    internal class MainBank
    {
        public struct BankAccount
        {
            public string AccountNumber;
            public string PIN;
            public double Balance;
        }

        private BankAccount[] accounts;
        private int numAccounts;
        private string filePath = "cDetails.txt";

        public void Start()
        {
            int choice = 0;

            ReadFileToArray();

            while (choice != 4)
            {
                Console.Clear();
                Console.WriteLine("\t\tBANK ATM\n\t\t-------");
                Console.WriteLine("\t1 - Display Account Balance");
                Console.WriteLine("\t2 - Deposit");
                Console.WriteLine("\t3 - Withdraw");
                Console.WriteLine("\t4 - Quit the program");

                do
                {
                    Console.Write("Enter your choice (1-4): ");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice < 1 || choice > 4);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        DisplayAccountBalance();
                        Pause();
                        break;
                    case 2:
                        Console.Clear();
                        Deposit();
                        Pause();
                        break;
                    case 3:
                        Console.Clear();
                        Withdraw();
                        Pause();
                        break;
                }
            }
            WriteArrayToFile();
            // the quit choice
            Console.WriteLine("End of Program.\nThank You");
        }

        private void WriteArrayToFile()
        {
            StreamWriter myfile = new StreamWriter(filePath);
            for (int i = 0; i < numAccounts; i++)
            {
                myfile.WriteLine(accounts[i].AccountNumber);
                myfile.WriteLine(accounts[i].PIN);
                if (i != (numAccounts - 1))
                {
                    myfile.WriteLine(accounts[i].Balance);
                }
                else { myfile.Write(accounts[i].Balance); }
            }
            myfile.Close();
        }

        private void DisplayAccountBalance()
        {
            Console.WriteLine("\t\tBANK ATM\n\t\t-------");
            Console.WriteLine("\tDisplay Account Balance");
            Console.WriteLine("\t-----------------------");
            Console.Write("Enter your account number: ");
            string accountNumberInput = Console.ReadLine();

            int accountIndex = FindAccountIndexByAccountNumber(accountNumberInput);

            if (accountIndex == -1)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                Console.WriteLine($"Account Balance for Account Number:" + accounts[accountIndex].AccountNumber);
                Console.WriteLine($"Balance: " + accounts[accountIndex].Balance.ToString("c"));
            }
        }

        private void Deposit()
        {
            Console.WriteLine("\t\tBANK ATM\n\t\t-------");
            Console.WriteLine("\tDeposit");
            Console.WriteLine("\t-------");
            Console.Write("Enter your account number: ");
            string accountNumberInput = Console.ReadLine();

            int accountIndex = FindAccountIndexByAccountNumber(accountNumberInput);

            if (accountIndex == -1)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                Console.Write("Enter the amount to deposit $: ");
                double depositAmount = double.Parse(Console.ReadLine());
                accounts[accountIndex].Balance += depositAmount;
                Console.WriteLine("Deposit successful.");
                Console.WriteLine($"New Balance: " + accounts[accountIndex].Balance.ToString("c"));
            }
        }

        private void Withdraw()
        {
            Console.WriteLine("\t\tBANK ATM\n\t\t-------");
            Console.WriteLine("\tWithdraw");
            Console.WriteLine("\t--------");
            Console.Write("Enter your account number: ");
            string accountNumberInput = Console.ReadLine();

            int accountIndex = FindAccountIndexByAccountNumber(accountNumberInput);

            if (accountIndex == -1)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                Console.Write("Enter the amount to withdraw $: ");
                double withdrawAmount = double.Parse(Console.ReadLine());

                if (withdrawAmount <= accounts[accountIndex].Balance)
                {
                    accounts[accountIndex].Balance -= withdrawAmount;
                    Console.WriteLine("Withdrawal successful.");
                    Console.WriteLine($"New Balance: " + accounts[accountIndex].Balance.ToString("c"));
                }
                else
                {
                    Console.WriteLine("Insufficient balance for withdrawal.");
                }
            }
        }

        private int FindAccountIndexByAccountNumber(string accountNumberInput)
        {
            for (int i = 0; i < numAccounts; i++)
            {
                if (accounts[i].AccountNumber == accountNumberInput)
                {
                    return i;
                }
            }

            return -1; // Return -1 if not found
        }

        private void Pause()
        {
            Console.Write("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        private void ReadFileToArray()
        {
            int nbLines = 0;
            StreamReader myfile = new StreamReader(filePath);
            while (myfile.EndOfStream == false)
            {
                string tmp = myfile.ReadLine();
                nbLines++;
            }
            myfile.Close();

            int size = (nbLines / 3) + 10;
            accounts = new BankAccount[size];

            myfile = new StreamReader(filePath);
            int i = 0;
            while (myfile.EndOfStream == false)
            {
                accounts[i].AccountNumber = myfile.ReadLine();
                accounts[i].PIN = myfile.ReadLine();
                accounts[i].Balance = Convert.ToDouble(myfile.ReadLine());
                i++;
            }
            myfile.Close();
            numAccounts = i;
        }
    }

}
