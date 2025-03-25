using System.Text;

namespace TI_Lab2
{
    public class checkData
    {
        public static string CheckForBD(string str)
        {
            StringBuilder result = new StringBuilder();
            foreach (char symbol in str)
            {
                if (symbol == '0' || symbol == '1')
                {
                    result.Append(symbol);
                }
            }
            return result.ToString();
        }
    }
}