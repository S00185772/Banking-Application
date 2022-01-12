using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

[assembly: System.Runtime.CompilerServices.DisablePrivateReflection]


namespace Banking_Application
{
    public class Program
    {
        public static string testtext = "";
        public static void Main(string[] args)
        {
            
              


            Data_Access_Layer dal = Data_Access_Layer.getInstance();
            dal.loadBankAccounts();
            bool running = true;

            do
            {
                
                Console.WriteLine("");
                Console.WriteLine("***Banking Application Menu***");
                Console.WriteLine("1. Add Bank Account");
                Console.WriteLine("2. Close Bank Account");
                Console.WriteLine("3. View Account Information");
                Console.WriteLine("4. Make Lodgement");
                Console.WriteLine("5. Make Withdrawal");
                Console.WriteLine("6. Exit");
                Console.WriteLine("CHOOSE OPTION:");
                String option = Console.ReadLine();
                
                switch(option)
                {
                    case "1":
                        String accountType = "";
                        int loopCount = 0;
                        
                        do
                        {

                           if(loopCount > 0)
                                Console.WriteLine("INVALID OPTION CHOSEN - PLEASE TRY AGAIN");

                            Console.WriteLine("");
                            Console.WriteLine("***Account Types***:");
                            Console.WriteLine("1. Current Account.");
                            Console.WriteLine("2. Savings Account.");
                            Console.WriteLine("CHOOSE OPTION:");
                            accountType = Console.ReadLine();

                            loopCount++;

                        } while (!(accountType.Equals("1") || accountType.Equals("2")));

                        String tempname = "";
                        byte[] name;
                        loopCount = 0;

                        do
                        {

                            if (loopCount > 0)
                                Console.WriteLine("INVALID NAME ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Name: ");
                            tempname = Console.ReadLine();

                            loopCount++;

                        } while (tempname.Equals(""));

                         name = Encode(tempname);

                        String addressLine1 = "";
                        loopCount = 0;

                        do
                        {

                            if (loopCount > 0)
                                Console.WriteLine("INVALID ÀDDRESS LINE 1 ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Address Line 1: ");
                            addressLine1 = Console.ReadLine();

                            loopCount++;

                        } while (addressLine1.Equals(""));

                        Console.WriteLine("Enter Address Line 2: ");
                        String addressLine2 = Console.ReadLine();
                        
                        Console.WriteLine("Enter Address Line 3: ");
                        String addressLine3 = Console.ReadLine();

                        String town = "";
                        loopCount = 0;

                        do
                        {

                            if (loopCount > 0)
                                Console.WriteLine("INVALID TOWN ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Town: ");
                            town = Console.ReadLine();

                            loopCount++;

                        } while (town.Equals(""));

                        double balance = -1;
                        loopCount = 0;

                        do
                        {

                            if (loopCount > 0)
                                Console.WriteLine("INVALID OPENING BALANCE ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Opening Balance: ");
                            String balanceString = Console.ReadLine();

                            try
                            {
                                balance = Convert.ToDouble(balanceString);
                            }

                            catch 
                            {
                                loopCount++;
                            }

                        } while (balance < 0);

                        Bank_Account ba;

                        if (Convert.ToInt32(accountType) == Account_Type.Current_Account)
                        {
                            double overdraftAmount = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID OVERDRAFT AMOUNT ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Overdraft Amount: ");
                                String overdraftAmountString = Console.ReadLine();

                                try
                                {
                                    overdraftAmount = Convert.ToDouble(overdraftAmountString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (overdraftAmount < 0);

                            ba = new Current_Account(name, addressLine1, addressLine2, addressLine3, town, balance, overdraftAmount);
                        }

                        else
                        {

                            double interestRate = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID INTEREST RATE ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Interest Rate: ");
                                String interestRateString = Console.ReadLine();

                                try
                                {
                                    interestRate = Convert.ToDouble(interestRateString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (interestRate < 0);

                            ba = new Savings_Account(name, addressLine1, addressLine2, addressLine3, town, balance, interestRate);
                        }

                        String accNo = dal.addBankAccount(ba);

                        Console.WriteLine("New Account Number Is: " + accNo);

                        break;
                    case "2":
                        Console.WriteLine("Enter Account Number: ");
                        accNo = Console.ReadLine();

                        ba = dal.findBankAccountByAccNo(accNo);

                        if (ba is null)
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            Console.WriteLine(ba.ToString());

                            String ans = "";

                            do
                            {

                                Console.WriteLine("Proceed With Delection (Y/N)?"); 
                                ans = Console.ReadLine();

                                switch (ans)
                                {
                                    case "Y":
                                    case "y": dal.closeBankAccount(accNo);
                                        break;
                                    case "N":
                                    case "n":
                                        break;
                                    default:
                                        Console.WriteLine("INVALID OPTION CHOSEN - PLEASE TRY AGAIN");
                                        break;
                                }
                            } while (!(ans.Equals("Y") || ans.Equals("y") || ans.Equals("N") || ans.Equals("n")));
                        }

                        break;
                    case "3":
                        Console.WriteLine("Enter Account Number: ");
                        accNo = Console.ReadLine();

                        ba = dal.findBankAccountByAccNo(accNo);

                        if(ba is null) 
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            Console.WriteLine(ba.ToString());
                        }

                        break;
                    case "4": //Lodge
                        Console.WriteLine("Enter Account Number: ");
                        accNo = Console.ReadLine();

                        ba = dal.findBankAccountByAccNo(accNo);

                        if (ba is null)
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            double amountToLodge = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID AMOUNT ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Amount To Lodge: ");
                                String amountToLodgeString = Console.ReadLine();

                                try
                                {
                                    amountToLodge = Convert.ToDouble(amountToLodgeString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (amountToLodge < 0);

                            dal.lodge(accNo, amountToLodge);
                        }
                        break;
                    case "5": //Withdraw
                        Console.WriteLine("Enter Account Number: ");
                        accNo = Console.ReadLine();

                        ba = dal.findBankAccountByAccNo(accNo);

                        if (ba is null)
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            double amountToWithdraw = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID AMOUNT ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Amount To Withdraw (€" + ba.getAvailableFunds() + " Available): ");
                                String amountToWithdrawString = Console.ReadLine();

                                try
                                {
                                    amountToWithdraw = Convert.ToDouble(amountToWithdrawString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (amountToWithdraw < 0);

                            bool withdrawalOK = dal.withdraw(accNo, amountToWithdraw);

                            if(withdrawalOK == false)
                            {

                                Console.WriteLine("Insufficient Funds Available.");
                            }
                        }
                        break;
                    case "6":
                        running = false;
                        break;
                    default:    
                        Console.WriteLine("INVALID OPTION CHOSEN - PLEASE TRY AGAIN");
                        break;
                }
                
                
            } while (running != false);

        }

        public static string Test(string text1 ,byte[] text2 )
        {

            string text = text1;//16 Bytes
            text = Encoding.ASCII.GetString(text2);
            byte[] protected_byte_array = Encoding.ASCII.GetBytes(text);
            Console.WriteLine("Plaintext (ASCII Encoded Text): " + text2);
            Console.WriteLine("Plaintext (ASCII Encoded Byte Array): [{0}]", string.Join(", ", text2));
            Console.WriteLine("");

            //Protect Data

            Protect(ref text2);
            //ProtectedMemory.Protect(protected_byte_array, MemoryProtectionScope.SameProcess);//Protect/Encrypt Data
            Console.WriteLine("Protected/Encrypted Data (Byte Array): [{0}]", string.Join(", ", text2));
            Console.WriteLine("Protected/Encrypted Data (Base64 Encoding): " + Convert.ToBase64String(text2));
            Console.WriteLine("");

            //Unprotect Data

            Unprotect(ref text2);
            //ProtectedMemory.Unprotect(protected_byte_array, MemoryProtectionScope.SameProcess);//Unprotect/Decrypt Data
            Console.WriteLine("Unprotected/Decrypted (Byte Array): [{0}]", string.Join(", ", text2));
            text = Encoding.ASCII.GetString(text2);
            Console.WriteLine("Re-Encoded ASCII String From Protected Byte Array: " + text);
            Console.WriteLine("");
           
            Console.ReadLine();
            return text;

            //Pause Application To Show Output On Screen





        }

        public static string Encoder(string text1, byte[] text2)
        {

            string text = text1;//16 Bytes
            text = Encoding.ASCII.GetString(text2);
            byte[] protected_byte_array = Encoding.ASCII.GetBytes(text);
            Console.WriteLine("Plaintext (ASCII Encoded Text): " + text);
            Console.WriteLine("Plaintext (ASCII Encoded Byte Array): [{0}]", string.Join(", ", text));
            Console.WriteLine("");

            //Protect Data

            Protect(ref text2);
            //ProtectedMemory.Protect(protected_byte_array, MemoryProtectionScope.SameProcess);//Protect/Encrypt Data
            Console.WriteLine("Protected/Encrypted Data (Byte Array): [{0}]", string.Join(", ", text));
            Console.WriteLine("Protected/Encrypted Data (Base64 Encoding): " + Convert.ToBase64String(text2));
            Console.WriteLine("");

            //Unprotect Data

            Unprotect(ref text2);           
            //ProtectedMemory.Unprotect(protected_byte_array, MemoryProtectionScope.SameProcess);//Unprotect/Decrypt Data
            Console.WriteLine("Unprotected/Decrypted (Byte Array): [{0}]", string.Join(", ", text));
            text = Encoding.ASCII.GetString(text2);
            Console.WriteLine("Re-Encoded ASCII String From Protected Byte Array: " + text);
            Console.WriteLine("");

            Console.ReadLine();
            return text;

            //Pause Application To Show Output On Screen





        }

        public static byte[] Encode(string stringtoEncode)
        {
            testtext = stringtoEncode;//16 Bytes
            byte[] protected_byte_array = Encoding.ASCII.GetBytes(testtext);

         // Protect(ref protected_byte_array);

            return protected_byte_array;
        }

        public static string Decode(byte[] bytetoDecode)
        {

        //    Unprotect(ref bytetoDecode);
            
           string testtext = Encoding.ASCII.GetString(bytetoDecode);

           

            return testtext;
        }



        public static void Protect(ref byte[] dataToProtect)
        {

            /*Step 1: Pad Data (PKCS7)*/

            long lengthOfOriginalDataInBytes = dataToProtect.Length;
            byte paddedBytesRequired = (byte)(16 - (lengthOfOriginalDataInBytes % 16));//No Of Bytes Required To Pad Byte Length Of Data Up To A Multiple Of 16 (Required To Use The ProtectedMemory.Protect() Method). 

            if (paddedBytesRequired == 0)//0=> Original Data Is Already A Multiple Of 16 Bytes Bytes => Full Padded Block Is Required (16 Bytes)
            {
                paddedBytesRequired = 16;
                lengthOfOriginalDataInBytes += 16;//16 Extra Padded Bytes Are Added As The Original Data Was A Multiple Of 16.
            }

            byte[] paddedDataToProtect = new byte[lengthOfOriginalDataInBytes + paddedBytesRequired];//Byte Array To Hold The Original Data Along With The Padded Data.

            Buffer.BlockCopy(dataToProtect, 0, paddedDataToProtect, 0, dataToProtect.Length);//Copy The Original Data To The New Byte Array That Has Room For The Padding.

            //Add In The Padded Data

            for (long i = dataToProtect.Length; i < paddedDataToProtect.Length; i++)//Assign The Same Value To Each Padded Byte (PKCS7)
                paddedDataToProtect[i] = paddedBytesRequired;

            dataToProtect = paddedDataToProtect;//Replace The Unpadded Byte Array With The Padded Byte Array

            /*Step 2: Protect Data*/

            ProtectedMemory.Protect(dataToProtect, MemoryProtectionScope.SameProcess);//Protect/Encrypt The Padded Byte Array

        }

        public static void Unprotect(ref byte[] paddedDataToUnprotect)
        {

            /*Step 1: Remove Data Protection*/

            ProtectedMemory.Unprotect(paddedDataToUnprotect, MemoryProtectionScope.SameProcess);//Protect/Encrypt Data

            /*Step 2: Remove Data Padding (PKCS7)*/

            byte noOfPaddedBytes = paddedDataToUnprotect[paddedDataToUnprotect.Length - 1];//PKCS7 => The Value In The Last Byte Of The Padded Array Denotes The Number of Padded Bytes.
            byte[] unpaddedByteArray = new byte[paddedDataToUnprotect.Length - noOfPaddedBytes];//Byte Array To Hold The Original Data After Padding Has Been Removed.

            //Verify That Padding Is Value

            for (long i = (paddedDataToUnprotect.Length - noOfPaddedBytes); i < paddedDataToUnprotect.Length - 1; i++)//For Each Padded Byte (Except The Very Last Byte)
            {

                if (paddedDataToUnprotect[i] != noOfPaddedBytes)//Verify That Each Padded Byte Has Been Assigned The Same Value As The Last Element In The Padded Byte Array (PKCS7).
                    throw new CryptographicException("Padding is invalid and cannot be removed.");//If Not Equal, Throw An Exception.
            }

            Buffer.BlockCopy(paddedDataToUnprotect, 0, unpaddedByteArray, 0, unpaddedByteArray.Length);//Copy The Original Data From The Padded Byte Array To The New, Unpadded Byte Array.

            paddedDataToUnprotect = unpaddedByteArray;//Replace The Padded Byte Array With The Unpadded Byte Array.

        }
    }

}


