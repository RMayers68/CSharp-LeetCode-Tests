
namespace PasswordChecker
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Variable declaration
            bool validPassword = false;
            while (validPassword == false)
            {
                int minLength = 8, score = 0;
                string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string lowercase = "abcdefghijklmnopqrstuvwxyz";
                string digits = "0123456789";
                string specialChars = "!@#$%^&*()_-+={[]}|\":;<>,.?/";
                // Ask user password
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                if (password.Length > minLength)
                {
                    score++;
                }
                if (Tools.Contains(password, uppercase))
                {
                    score++;
                }
                if (Tools.Contains(password, lowercase))
                {
                    score++;
                }
                if (Tools.Contains(password, digits))
                {
                    score++;
                }
                if (Tools.Contains(password, specialChars))
                {
                    score++;
                }
                if (password == "password" || password == "1234")
                {
                    score = 0;
                }
                switch (score)
                {
                    case 4:
                    case 5:
                        Console.WriteLine("The password is extremely strong, nice job!");
                        validPassword = true;
                        break;
                    case 3:
                        Console.WriteLine("The password is strong.");
                        break;
                    case 2:
                        Console.WriteLine("The password is ok.");
                        break;
                    case 1:
                        Console.WriteLine("The password is weak.");
                        break;
                    default:
                        Console.WriteLine("The password sucks and meets no standards.");
                        break;
                }
            }
        }
    }
}
