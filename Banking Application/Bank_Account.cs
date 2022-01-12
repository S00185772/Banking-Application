using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static Banking_Application.Program;


namespace Banking_Application
{
    public abstract class Bank_Account
    {

        public String accountNo;
        public byte[] name;
        public String address_line_1;
        public String address_line_2;
        public String address_line_3;
        public String town;
        public double balance;

        public Bank_Account()
        {

        }

        public Bank_Account(byte[] name, String address_line_1, String address_line_2, String address_line_3, String town, double balance)
        {
            this.accountNo = System.Guid.NewGuid().ToString();
            this.name = name;
            this.address_line_1 = address_line_1;
            this.address_line_2 = address_line_2;
            this.address_line_3 = address_line_3;
            this.town = town;
            this.balance = balance;
        }

        public void lodge(double amountIn)
        {

            balance += amountIn;

        }






        public abstract bool withdraw(double amountToWithdraw);

        public abstract double getAvailableFunds();

        public override String ToString()
        {
            string test1 = "";
            test1 = Decode(name);
            return "\nAccount No: " + accountNo + "\n" +
            // "Name: " + name + "\n" +
            ("Name Encoded: " + string.Join(", ", name)) + "\n" +
            "Address Line 1: " + address_line_1 + "\n" +
            "Address Line 2: " + address_line_2 + "\n" +
            "Address Line 3: " + address_line_3 + "\n" +
            "Town: " + town + "\n" +
            "Balance: " + balance + "\n"+
            "Decoded Name " + Test("tom",name) + "\n"; 

        }




        //public static void Unprotect(ref byte[] paddedDataToUnprotect)
        //{

        //    /*Step 1: Remove Data Protection*/

        //    System.Security.Cryptography.ProtectedMemory.Unprotect(paddedDataToUnprotect, MemoryProtectionScope.SameProcess);//Protect/Encrypt Data

        //    /*Step 2: Remove Data Padding (PKCS7)*/

        //    byte noOfPaddedBytes = paddedDataToUnprotect[paddedDataToUnprotect.Length - 1];//PKCS7 => The Value In The Last Byte Of The Padded Array Denotes The Number of Padded Bytes.
        //    byte[] unpaddedByteArray = new byte[paddedDataToUnprotect.Length - noOfPaddedBytes];//Byte Array To Hold The Original Data After Padding Has Been Removed.

        //    //Verify That Padding Is Value

        //    for (long i = (paddedDataToUnprotect.Length - noOfPaddedBytes); i < paddedDataToUnprotect.Length - 1; i++)//For Each Padded Byte (Except The Very Last Byte)
        //    {

        //        if (paddedDataToUnprotect[i] != noOfPaddedBytes)//Verify That Each Padded Byte Has Been Assigned The Same Value As The Last Element In The Padded Byte Array (PKCS7).
        //            throw new CryptographicException("Padding is invalid and cannot be removed.");//If Not Equal, Throw An Exception.
        //    }

        //    Buffer.BlockCopy(paddedDataToUnprotect, 0, unpaddedByteArray, 0, unpaddedByteArray.Length);//Copy The Original Data From The Padded Byte Array To The New, Unpadded Byte Array.

        //    paddedDataToUnprotect = unpaddedByteArray;//Replace The Padded Byte Array With The Unpadded Byte Array.

        //}




    }
}

   


