using System;

public class Class1
{
	public Class1()
	{

namespace MoneyMaker
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //variable definition
            double platinumCoin = 25, goldCoin = 10, silverCoin = 5;
            //user input for amount
            Console.WriteLine("Welcome to Money Maker!\nPlease enter a amount of money (in cents):");
            while (true)
            {
                double userAmount = Math.Floor(Convert.ToDouble(Console.ReadLine()));
                Console.WriteLine($"{userAmount} cents is equal to...");
                //Calculations
                double platinumCoins = Math.Floor(userAmount / platinumCoin);
                double leftoverAmount = userAmount % platinumCoin;
                double goldCoins = Math.Floor(leftoverAmount / goldCoin);
                leftoverAmount = leftoverAmount % goldCoin;
                double silverCoins = Math.Floor(leftoverAmount / silverCoin);
                leftoverAmount = leftoverAmount % silverCoin;
                //Showing user final product
                Console.WriteLine($"Quarters: {platinumCoins}\nDimes: {goldCoins}\nNickels: {silverCoins}\nPennies: {leftoverAmount}");
            }



        }
    }
}

	}
}
