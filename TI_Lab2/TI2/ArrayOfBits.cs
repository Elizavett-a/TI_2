using System.Text;
using System.Collections;

namespace TI_Lab2
{
    class ArrayOfBits
    {
        public static string BitArrayToStr(BitArray arrayB)
        {
            StringBuilder tempStr = new StringBuilder();
            foreach (bool bit in arrayB)
            {
                tempStr.Append(bit ? '1' : '0');
            }
            return tempStr.ToString();
        }

        public static BitArray StringToBitArray(string inputStr)
        {
            inputStr = checkData.CheckForBD(inputStr); ;

            BitArray arrayB = new BitArray(inputStr.Length);
            for (int i = 0; i < inputStr.Length; i++)
            {
                arrayB[i] = inputStr[i] == '1';
            }
            return arrayB;
        }
    }
}
