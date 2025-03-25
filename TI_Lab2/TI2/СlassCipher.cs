using System.Collections;

namespace TI_Lab2
{
    public class СlassCipher
    {
        public BitArray FindBitRegister(string parsingString)
        {
            BitArray bitRegister = new BitArray(parsingString.Length);
            for (int i = 0; i < parsingString.Length; i++)
                bitRegister[i] = parsingString[i] == '1';
            return bitRegister;
        }

        public BitArray Cipher(BitArray bitKey, BitArray plainText)
        {
            return bitKey.Xor(plainText);
        }

        public BitArray FindBitKey(BitArray bitRegister, int length)
        {
            BitArray bitKey = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                bitKey[i] = bitRegister[0];

                // P(x) = x^27 + x^8 + x^7 + x + 1
                int len = bitRegister.Length;
                bool nextValue = bitRegister[len - 1 - 26] ^ bitRegister[len - 1 - 7] ^ bitRegister[len - 1 - 6] ^ bitRegister[len - 1];
                for (int index = 0; index < bitRegister.Length - 1; index++)
                {
                    bitRegister[index] = bitRegister[index + 1];
                }
                bitRegister[bitRegister.Length - 1] = nextValue;
            }
            return bitKey;
        }
    }
}