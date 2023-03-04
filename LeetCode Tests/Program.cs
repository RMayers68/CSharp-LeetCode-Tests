﻿using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using System.Reflection;
using System.Xml;

public class Solution
{
    public static void Main() //Made to test other functions.
    {
        while (true)
        {
            // Insert any test case and function into this block to test
            Console.WriteLine(MyAtoi("+-12"));
            Console.WriteLine(MyAtoi("     -42"));
            Console.WriteLine(MyAtoi("4931 with words"));
            Console.WriteLine(MyAtoi("    +9999999999999"));
            Console.WriteLine(MyAtoi("words and 987"));
            Console.ReadKey();
        }
    }



    // LeetCode Algorithms   

    public static int MyAtoi(string s) // 95% speed (50ms) but I feel like it can be compressed. 70% on memory
    {
        if (String.IsNullOrWhiteSpace(s))
            return 0;
        else if (s.Length == 1 && !Char.IsDigit(s[0]))
            return 0;
        int startIndex = 0, endIndex = s.Length, result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            switch (s[i])
            {
                default:
                    if (Char.IsDigit(s[i]))
                        break;
                    else return 0;
                case ' ':
                    continue;
                case '+' or '-':
                    if (!Char.IsDigit(s[i + 1]))
                        return 0;
                    else
                        break;
            }
            startIndex = i;
            break;
        }
        s = s.Substring(startIndex);
        for (int i = 1; i < s.Length; i++)
        {

            if (!Char.IsDigit(s[i]) )
            {
                endIndex = i;
                break;
            }
        }
        if (endIndex < s.Length)
            s = s.Remove(endIndex);
        if(!Int32.TryParse(s, out result))
        {
            if (s[0] == '-')
                return Int32.MinValue;
            else return Int32.MaxValue;
        }
        return result;
    }

    public static int ReverseInteger(int x) // 45% speed, low in memory ( decrease usage by modifying string in place rather than char array)
    {
        string xString = x.ToString();
        char[] xCharArray = xString.ToCharArray();
        Array.Reverse(xCharArray);
        string xConvert = new(xCharArray);
        if (xConvert[xConvert.Length-1] == '-')
        {
            xConvert = '-' + xConvert;
            xConvert = xConvert.Remove(xConvert.Length-1);
        }
        try
        {
            x = Convert.ToInt32(xConvert);
        }
        catch (OverflowException)
        {
            return 0;
        }        
        return x;
    }

    public static string ZigZagConvert(string s, int numRows) // Average in terms of runtime O(3s), poor on memory
    {
        if (s.Length - 1 < numRows)
            return s;
        string zigZag = "";
        for (int i = numRows; i > 0; i--)
        {
            string tmp = "";
            int index = i-1;
            tmp += s[index];
            s = s.Remove(index, 1);
            while (index <= s.Length-1)
            {
                if (i < numRows)
                {
                    tmp += s[index];
                    s = s.Remove(index, 1);
                }
                if (i > 1)
                {
                    index += i + i - 3;
                }
                if (index <= s.Length-1)
                {
                    tmp += s[index];
                    s = s.Remove(index, 1);
                }               
            }
            zigZag = tmp + zigZag;
        }
        return zigZag;
    }

    public static string LongestPalindrome(string s) //Original solution very slow (n^3), adapted this solution from one I saw in solutions
    {
        int start = 0, end = 0;
        for (int i = 0; i < s.Length; i++)
        {
            int evenLength = ExpandPalindrome(s, i, i + 1);
            int length = ExpandPalindrome(s, i, i);
            length = Math.Max(evenLength, length);
            if (length > end - start)
            {
                start = i - (length - 1) / 2;
                end = i + length / 2;
            }
        }
        return s.Substring(start, end - start + 1);
    }
    private static int ExpandPalindrome(string s, int left, int right)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }
        return right - left - 1;
    }

    public static int LengthOfLongestSubstring(string s) // 74.5% in speed, poor in memory performance however
    {
        string subString = "", longestSubstring = "";
        foreach (char c in s)
        {
            if (!subString.Contains(c))
                subString += c;
            else
            {
                subString = subString.Substring(subString.LastIndexOf(c)+1);
                subString += c;
            }
            if (longestSubstring.Length < subString.Length)
                longestSubstring = subString;
        }
        Console.WriteLine(longestSubstring);
        return longestSubstring.Length;
    }

    public static double FindMedianSortedArrays(int[] nums1, int[] nums2) //Confused, leetcode time scaled from 5% (225ms) to 92% (90 ms), so I think it's good?
    {
        int twoPointer = nums2.Length-1, onePointer = nums1.Length-1;
        int[] nums3 = new int[onePointer+twoPointer+2];
        // merge 2 arrays into 3rd array
        for (int i = nums3.Length - 1; i >= 0; i--)
        {
            if (twoPointer < 0)
            {
                for (int j = i; j >= 0; j--)
                {
                    nums3[j] = nums1[onePointer];
                    onePointer--;
                }
                break;
            }
            else if (onePointer < 0)
            {
                for (int j = i; j >= 0; j--)
                {
                    nums3[j] = nums2[twoPointer];
                    twoPointer--;
                }
                break;
            }
            switch (nums2[twoPointer] >= nums1[onePointer])
            {
                case true:
                    nums3[i] = nums2[twoPointer];
                    twoPointer--;
                    break;
                case false:
                    nums3[i] = nums1[onePointer];
                    onePointer--;
                    break;
            }
        }
        if (nums3.Length == 1)
            return Convert.ToDouble(nums3[0]);
        else if (!(nums3.Length % 2 == 0))
            return Convert.ToDouble(nums3[nums3.Length/2]);
        else
            return (Convert.ToDouble(nums3[(nums3.Length/2) - 1]) + Convert.ToDouble(nums3[(nums3.Length / 2)])) / 2.0;
    }

    public static void Merge(int[] nums1, int m, int[] nums2, int n) //Time of O(m+n), beats 66.52% (or 97.44%, leetcode times make no sense) and standard in Memory (51.96%)
    {
        int twoPointer = n - 1, onePointer = m - 1;
        for (int i = nums1.Length - 1; i >= 0; i--)
        {
            if (twoPointer < 0)
            {
                return;
            }
            else if (onePointer < 0)
            {
                for (int j = i; j >= 0; j--)
                {
                    nums1[j] = nums2[twoPointer];
                    twoPointer--;
                }                   
                return;
            }
            switch(nums2[twoPointer] >= nums1[onePointer])
            {
                case true:
                    nums1[i] = nums2[twoPointer];
                    twoPointer--;
                    break;
                case false:
                    nums1[i] = nums1[onePointer];
                    onePointer--;
                    break;
            }
        }
    }

    public static int Divide(int dividend, int divisor) // Divide without using /,*,or % operators
    {
        long result = 0;
        int negativeChecker = dividend;        
        long longDividend = Math.Abs(Convert.ToInt64(dividend));
        long longDivisor = Math.Abs(Convert.ToInt64(divisor));
        long resultIncrement = 1, subtractValue = longDivisor;
        while (longDividend >= longDivisor)
        {
            if (longDividend >= subtractValue)
            {
                longDividend -= subtractValue;
                result += resultIncrement;
                subtractValue <<= 1;
                resultIncrement <<= 1;
            }
            else
            {
                subtractValue >>= 1;
                resultIncrement >>= 1;
            }
        }
        if (negativeChecker < 0 && divisor > 0 || divisor < 0 && negativeChecker > 0)
        {
            long negativeResult = result;
            for (int i = 0; i < 2; i++)
                result -= negativeResult;
        }
        if (result > Int32.MaxValue)
            return Int32.MaxValue;
        return Convert.ToInt32(result);
    }

    public static string IntToRoman(int num) 
    // NOTES: Tried to not use a dictionary, recursion would have made less readable code but faster and less memory than about 84%!
    {
        string result = "";
        while(num > 0)
        {
            result += num switch
            {
                int i when i >= 1000 => "M",
                int i when i < 1000 && i >= 900 => "CM",
                int i when i < 900 && i >= 500 => "D",
                int i when i < 500 && i >= 400 => "CD",
                int i when i < 400 && i >= 100 => "C",
                int i when i < 100 && i >= 90 => "XC",
                int i when i < 90 && i >= 50 => "L",
                int i when i < 50 && i >= 40 => "XL",
                int i when i < 40 && i >= 10 => "X",
                int i when i < 10 && i >= 9 => "IX",
                int i when i < 9 && i >= 5 => "V",
                int i when i < 5 && i >= 4 => "IV",
                int i when i < 4 && i >= 1 => "I",
            };
            num -= num switch
            {
                int i when i >= 1000 => 1000,
                int i when i < 1000 && i >= 900 => 900,
                int i when i < 900 && i >= 500 => 500,
                int i when i < 500 && i >= 400 => 400,
                int i when i < 400 && i >= 100 => 100,
                int i when i < 100 && i >= 90 => 90,
                int i when i < 90 && i >= 50 => 50,
                int i when i < 50 && i >= 40 => 40,
                int i when i < 40 && i >= 10 => 10,
                int i when i < 10 && i >= 9 => 9,
                int i when i < 9 && i >= 5 => 5,
                int i when i < 5 && i >= 4 => 4,
                int i when i < 4 && i >= 1 => 1,
            };
        }
        return result;
    }

    public static ListNode AddTwoNumbers(ListNode? l1, ListNode? l2, int carry)
    {
        int value = 0;
        ListNode result;
        if (l1 != null && l2 != null)
        {
            value = l1.val + l2.val + carry;
            result = new(value % 10, AddTwoNumbers(l1.next, l2.next, value/10));
        }          
        else if (l1 == null && l2 == null)
        {
            if (carry == 1)
                return new ListNode(1, null);
            else
                return null;
        }     
        else if (l2 == null)
        {
            value = l1.val + carry;
            result = new(value % 10, AddTwoNumbers(l1.next, null, value/10));
        }           
        else
        {
            value = l2.val + carry;
            result = new(value % 10, AddTwoNumbers(null, l2.next, value/10));
        }                        
        return result;
    }
    public static int[] PlusOne(int[] digits)
    {
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            digits[i] = (digits[i] + 1) % 10;
            // If no leftover, then we will just copy the rest of the array over and delete our first number
            if (digits[i] != 0)
            {
                for (int j = i; j >= 0; j--)
                    digits[i] = digits[i];
                return digits;
            }             
        }
        //In the case of 99 creating 100 and similar cases...
        int[] leadingOne = new int[digits.Length+1];
        leadingOne[0] = 1;
        return leadingOne;
    }
    public static int LengthofLastString(string s)                                  // faster than 66.58% and less memory than 94.79%!
    {
        int length = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == ' ' && length > 0)
                return length;
            else if (s[i] != ' ')
                length++;
        }
        return length;
    }

    public static int SearchInsert(int[] nums, int target)                  // My first Binary Search
    {
        bool IndexFound = false;
        int mid = ((nums.Length - 1) / 2), bottom = 0, top = nums.Length - 1;
        while (!IndexFound)
        {
            if (mid == top)                 // Condition where we have reached end of sorted binary search
            {
                if (target > nums[mid])
                {
                    mid++;
                }
                IndexFound = true;
                break;
            }            
            if (target > nums[mid])    // Target higher than mid value
                bottom = mid+1;
            else if (target < nums[mid])    //Target lower than mid value
                top = mid-1;
            else       // Target equal to mid value
                IndexFound = true;
            mid = (top + bottom) / 2;
            if (top < bottom)
            {
                top = mid;
                bottom = mid;
            }
        }
        return mid;
    }

    public static int[] BuildArray(int[] nums)
    {
        int[] ans = new int[nums.Length];                                //Solution with using 0(1) extra memory
        for (int i = 0; i < nums.Length; i++)
        {
            ans[i] = nums[nums[i]];
        }
        return ans;
    }
    public static int SearchInsertBetter(int[] nums, int target)            //Better Binary Search
    {

        int min = 0;
        int max = nums.Length - 1;
        int mid = (max + min) / 2;

        while (min <= max)
        {

            mid = min + (max - min) / 2;

            if (nums[mid] == target)
            {

                return mid;
            }
            else if (nums[mid] < target)
            {

                min = mid + 1;
            }
            else if (nums[mid] > target)
            {

                max = mid - 1;
            }
        }

        return min;
    }
    private static int removeDuplicates(int[] nums)
    {
        int res = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[i - 1])
            {
                nums[res] = nums[i];
                res++;
            }
        }
        return res;
    }

    public static ListNode MergeTwoLists(ListNode list1, ListNode list2)       // First time ever programming Linked Lists, much better ways to do this.
    {
        if (list1 == null)
            return list2;
        else if(list2 == null)
             return list1;
        else
        {
            LinkedList<ListNode> mergedLists = new();
            for (ListNode node = list1; node != null; node = node.next)
            {
                while (list2 != null && list2.val <= node.val)
                {
                    if (mergedLists.Last != null)
                        mergedLists.Last.Value.next = list2;
                    mergedLists.AddLast(list2);
                    list2 = list2.next;
                }
                if (mergedLists.Last != null)
                    mergedLists.Last.Value.next = node;
                ListNode tmp = node;
                mergedLists.AddLast(tmp);
            }
            if (list2 != null)
            {
                for(ListNode node = list2; node != null; node = node.next)
                {
                    if (mergedLists.Last != null)
                        mergedLists.Last.Value.next = node;
                    ListNode tmp = node;
                    mergedLists.AddLast(tmp);
                }                                   
            }
            return mergedLists.First.Value;
        }
    }

    public static ListNode BetterMergeTwoLists(ListNode list1, ListNode list2) // Much better algorithm using recursion to speed up and save memory (although times and memory usage on LeetCode are wildly variable, needs research)
    {
        if (list1 == null) return list2;
        if (list2 == null) return list1;
        if (list1.val <= list2.val)
        {
            list1.next = MergeTwoLists(list1.next, list2);
            return list1;
        }
        else
        {
            list2.next = MergeTwoLists(list1, list2.next);
            return list2;
        }
    }
    public static int RemoveElement(int[] nums, int val) // Again great at memory storage, but slow
    {
        int res = nums.Length - 1;
        int k = 0;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            int tmp = -1;
            if (nums[i] == val)
            {
                tmp = nums[res];
                nums[res] = nums[i];
                nums[i] = tmp;
                res--;
            }
            else k++;
        }
        return k;
    }
    public static bool IsValid(string s)        //Slow but effective on saving memory
    {
        if (s.Length < 2)
            return false;
        Stack<char> stack = new();
        for (int i = 0; i < s.Length; i++)
            if (s[i] == '(' || s[i] == '[' || s[i] == '{')
                stack.Push(s[i]);
            else if (s[i] == ')' || s[i] == ']' || s[i] == '}')
            {
                switch (s[i])
                {
                    case ')':
                        if (stack.TryPeek(out char c) && c == '(')
                            stack.Pop();
                        else return false;
                        break;
                    case ']':
                        if (stack.TryPeek(out char d) && d == '[')
                            stack.Pop();
                        else return false;
                        break;
                    case '}':
                        if (stack.TryPeek(out char e) && e == '{')
                            stack.Pop();
                        else return false;
                        break;
                }
            }
        if (stack.Any()) return false;
        else return true;
    }
    public static int[] TwoSum(int[] nums, int target)  //97.7% faster WITHOUT using a Dictionary
    {
        for (int i = nums.Length; i > 0; i--)
        {
            for (int j = 0; j < i - 1; j++)
                if (nums[j] + nums[i - 1] == target)
                {
                    int[] answer = { j, i - 1 };
                    return answer;
                }
        }
        return nums;
    }

    public static bool IsPalindrome(int x)     //Didn't turn x into a String!
    {
        if (x < 0)
            return false;
        else
        {
            List<int> list = new();
            while (x > 0)
            {
                list.Add(x % 10);
                x = x / 10;
            }
            list.Reverse();
            int j = 0;
            for (int i = list.Count; i > 0; i--)
            {
                if (list[j] != list[i - 1])
                    return false;
                else if (j == i - 1)
                    return true;
                j++;
            }
        }
        return true;
    }

    public static string LongestCommonPrefix(string[] strs)  // Very low performing - go back and find faster way
    {
        string res = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        if (strs.Length == 0)
            return "";
        else if (strs.Length == 1)
            return strs[0];
        else for (int i = 0; i < strs.Length - 1; i++)
            {
                string tmp = "";
                bool KeepChecking = true;
                for (int j = 0; j < strs[i].Length; j++)
                {
                    if (j > strs[strs.Length - 1].Length - 1) ;
                    else if (strs[i][j] != strs[strs.Length - 1][j])
                        KeepChecking = false;
                    else if (strs[i][j] == strs[strs.Length - 1][j] && KeepChecking)
                        tmp += strs[i][j];
                }
                if (tmp.Length < res.Length)
                    res = tmp;
            }
        return res;
    }

    // Misc Tutorials - Ignore if looking for my proof of knowledge (where is that I swore I left it here...)


    //Money Calculator
    private static void MoneyMaker()
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


    // Password Checker
    private static void PasswordChecker()
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
            /*if (Tools.Contains(password, uppercase)) // add regex
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
            }*/
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



    // CHOOSE ADVENTURE
    static void ChooseAdventure(string[] args)
    {
        /* THE MYSTERIOUS NOISE */

        // Start by asking for the user's name:
        Console.Write("What is your name?: ");
        string name = Console.ReadLine();
        Console.WriteLine($"Hello, {name}! Welcome to our story.");
        Console.WriteLine("It begins on a cold rainy night. You're sitting in your room and hear a noise coming from down the hall. Do you go investigate? Type YES or NO:");
        string noiseChoice = Console.ReadLine().ToUpper();
        if (noiseChoice == "NO")
        {
            Console.WriteLine("Not much of an adventure if we don't leave our room!\nTHE END.");
        }
        else if (noiseChoice == "YES")
        {
            Console.WriteLine("You walk into the hallway and see a light coming from under a door down the hall.\nYou walk towards it. Do you open it or knock? Type OPEN or KNOCK:");
        }
        string doorChoice = Console.ReadLine().ToUpper();
        if (doorChoice == "KNOCK")
        {
            Console.WriteLine("A voice behind the door speaks. It says, \"Answer this riddle: ");
            Console.WriteLine("\"Poor people have it. Rich people need it. If you eat it you die. What is it?\"");
            Console.Write("Type your answer:");
            string riddleAnswer = Console.ReadLine().ToUpper();
            if (riddleAnswer == "NOTHING")
            {
                Console.WriteLine("The door opens and NOTHING is there.\nYou turn off the light and run back to your room and lock the door.\nTHE END.");
            }
            else
            {
                Console.WriteLine("You answered incorrectly. The door doesn't open.\nTHE END.");
            }
        }
        else if (doorChoice == "OPEN")
        {
            Console.WriteLine("The door is locked! See if one of your three keys will open it.\nEnter a number (1-3):");
            int keyChoice = Convert.ToInt32(Console.ReadLine());
            switch (keyChoice)
            {
                case 1:
                    Console.WriteLine("You choose the first key. Lucky choice!\nThe door opens and NOTHING is there. Strange...\nTHE END.");
                    break;
                case 2:
                    Console.WriteLine("You choose the second key. The door doesn't open.\nTHE END.");
                    break;
                case 3:
                    Console.WriteLine("You choose the third key. The door doesn't open.\nTHE END.");
                    break;
            }
        }
    }


    // Calculate Area Cost
    public static void CalcAreaCost(string[] args) //all values are from Codecademy
    {
        Console.WriteLine("Please enter a number correlating to a monument's plan:");
        Console.WriteLine("1: Teotihuacan\n2: Taj Mahal\n3: Great Mosque of Mecca");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                CalculateTeotihuacanCost();
                break;
            case 2:
                CalculateTajMahalCost();
                break;
            case 3:
                CalculateMeccaMosqueCost();
                break;
        }
    }

    static void CalculateTeotihuacanCost()
    {
        double rectangleArea = Rectangle(1500, 2500);
        double halfCircleArea = Circle(375) / 2;
        double triangleArea = Triangle(750, 500);
        double totalArea = rectangleArea + halfCircleArea + triangleArea;
        double totalAreaCost = Math.Round(totalArea * 180, 2);
        Console.WriteLine($"Teotihuacan's cost for {Math.Round(totalArea, 2)} meters is {totalAreaCost} pesos!");
    }

    static void CalculateTajMahalCost()
    {
        double rectangleArea = Rectangle(90.5, 90.5);
        double triangleArea = Triangle(24, 20.78);
        double totalArea = rectangleArea - (triangleArea * 4);
        double totalAreaCost = Math.Round(totalArea * 180, 2);
        Console.WriteLine($"Taj Mahal's cost for {Math.Round(totalArea, 2)} meters is {totalAreaCost} pesos!");
    }

    static void CalculateMeccaMosqueCost()
    {
        double rectangleArea = Rectangle(264, 390);
        double triangleCutOut = Triangle(84, 264);
        double rectangleCutOut = Rectangle(84, 106);
        double totalArea = rectangleArea - triangleCutOut - rectangleCutOut;
        double totalAreaCost = Math.Round(totalArea * 180, 2);
        Console.WriteLine($"The Mecca's cost for {Math.Round(totalArea, 2)} meters is {totalAreaCost} pesos!");
    }

    static double Rectangle(double length, double width)
    {
        return length * width;
    }

    static double Circle(double radius)
    {
        return Math.PI * Math.Pow(radius, 2);
    }

    static double Triangle(double bottom, double height)
    {
        return 0.5 * bottom * height;
    }







    //   MONSTER MAKER
    // Extend the BuildACreature() method so that all of its parameters are optional. It should assign a random body part if a parameter is not specified.

    static void SuperMonsterMaker(string[] args)
    {
        Console.WriteLine("Welcome to Super Monster Maker!");
        Console.WriteLine("1:Pick the pieces of your monster\n2:Have a random monster created for you");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.WriteLine("Please enter a head type: Monster, Ghost, or Bug");
                string head = Console.ReadLine().ToLower();
                Console.WriteLine("Please enter a body type: Monster, Ghost, or Bug");
                string body = Console.ReadLine().ToLower();
                Console.WriteLine("Please enter a foot type: Monster, Ghost, or Bug");
                string feet = Console.ReadLine().ToLower();
                BuildACreature(head, body, feet);
                break;
            case 2:
                RandomMode();
                break;
        }
    }

    static void BuildACreature(string head, string body, string feet)
    {
        //this returns a creature with the three selected variants of head, body and feet
        int headInt = TranslateToNumber(head);
        int bodyInt = TranslateToNumber(body);
        int feetInt = TranslateToNumber(feet);
        SwitchCase(headInt, bodyInt, feetInt);
    }

    static void RandomMode()
    {
        Random randomNumber = new Random();
        int head = randomNumber.Next(1, 4);
        int body = randomNumber.Next(1, 4);
        int feet = randomNumber.Next(1, 4);
        SwitchCase(head, body, feet);
    }

    static void SwitchCase(int head, int body, int feet)
    {
        switch (head)
        {
            case 1:
                MonsterHead();
                break;
            case 2:
                BugHead();
                break;
            case 3:
                GhostHead();
                break;
        }

        switch (body)
        {
            case 1:
                MonsterBody();
                break;
            case 2:
                BugBody();
                break;
            case 3:
                GhostBody();
                break;
        }

        switch (feet)
        {
            case 1:
                MonsterFeet();
                break;
            case 2:
                BugFeet();
                break;
            case 3:
                GhostFeet();
                break;
        }
    }

    static int TranslateToNumber(string creature)
    {
        switch (creature)
        {
            case "monster":
                return 1;
            case "bug":
                return 2;
            case "ghost":
                return 3;
            default:
                return 1;
        }
    }

    static void GhostHead()
    {
        Console.WriteLine("     ..-..");
        Console.WriteLine("    ( o o )");
        Console.WriteLine("    |  O  |");
    }

    static void GhostBody()
    {
        Console.WriteLine("    |     |");
        Console.WriteLine("    |     |");
        Console.WriteLine("    |     |");
    }

    static void GhostFeet()
    {
        Console.WriteLine("    |     |");
        Console.WriteLine("    |     |");
        Console.WriteLine("    '~~~~~'");
    }

    static void BugHead()
    {
        Console.WriteLine("     /   \\");
        Console.WriteLine("     \\. ./");
        Console.WriteLine("    (o + o)");
    }

    static void BugBody()
    {
        Console.WriteLine("  --|  |  |--");
        Console.WriteLine("  --|  |  |--");
        Console.WriteLine("  --|  |  |--");
    }

    static void BugFeet()
    {
        Console.WriteLine("     v   v");
        Console.WriteLine("     *****");
    }

    static void MonsterHead()
    {
        Console.WriteLine("     _____");
        Console.WriteLine(" .-,;='';_),-.");
        Console.WriteLine("  \\_\\(),()/_/");
        Console.WriteLine("　  (,___,)");
    }

    static void MonsterBody()
    {
        Console.WriteLine("   ,-/`~`\\-,___");
        Console.WriteLine("  / /).:.('--._)");
        Console.WriteLine(" {_[ (_,_)");
    }

    static void MonsterFeet()
    {
        Console.WriteLine("    |  Y  |");
        Console.WriteLine("   /   |   \\");
        Console.WriteLine("   \"\"\"\" \"\"\"\"");
    }

    //Definition for singly - linked list.
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}




    