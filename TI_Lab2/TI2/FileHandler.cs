using System.Collections;
using System.IO;
using System.Text;

namespace TI_Lab2
{
    public class FileHandler
    {
        public BitArray ReadFileToBitArray(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var bytes = File.ReadAllBytes(filePath);
            for (int i = 0; i < bytes.Length; i++)
            {
                BitArray help = new BitArray(new[] { bytes[i] });
                foreach (bool bit in help)
                {
                    stringBuilder.Append(bit ? '1' : '0');
                }
            }

            BitArray bitArray = new BitArray(stringBuilder.Length);
            for (int i = 0; i < bitArray.Length; i++)
            {
                bitArray[i] = stringBuilder[i] == '1';
            }

            return bitArray;
        }

        public void SaveBitArrayToFile(string filePath, BitArray bitArray)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                byte[] result = new byte[bitArray.Count / 8];
                bitArray.CopyTo(result, 0);
                fileStream.Write(result, 0, result.Length);
            }
        }
    }
}