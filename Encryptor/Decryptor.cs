using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{
    public static class Decryptor
    {

        private static Encoding encoding = Encoding.GetEncoding("windows-1251");

        private static string rusAlph = "АаВЕеКМОоРрСсуХх";
        private static string engAlph = "AaBEeKMOoPpCcyXx";

        public static void DecryptorText(string encryptedFilePath)
        {
            StringBuilder sb = new StringBuilder();

            List<string> decryptedBits = new List<string>(); 

            try
            {
                using (StreamReader sr = new StreamReader(encryptedFilePath, encoding))
                {
                    string text = sr.ReadToEnd();

                    bool flag = true;

                    int count = 0;

                    foreach (var elem in text)
                    {
                        int indexRus = rusAlph.IndexOf(elem);
                        int indexEng = engAlph.IndexOf(elem);


                        if ((indexRus != -1 || indexEng != -1) && flag)
                        {
                            if (rusAlph.Contains(elem))
                            {
                                sb.Append('0');
                            }

                            if (engAlph.Contains(elem))
                            {
                                sb.Append('1');
                            }

                            if (sb.Length == 8)
                            {
                                if (sb.ToString().Equals("00000000"))
                                    count++;
                                if (count >= 2)
                                    flag = false;
                                decryptedBits.Add(sb.ToString());
                                sb.Clear();
                            }
                        }
                    }
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\nРасшифрованные биты:");

            foreach (var e in decryptedBits)
            {
                Console.WriteLine(e);
            }

            byte[] decryptedBytes = new byte[decryptedBits.Count-2];

            for (int i = 0; i < decryptedBits.Count-2; i++)
            {
                decryptedBytes[i] = Convert.ToByte(decryptedBits[i], 2);
            }

            string encryptedText = encoding.GetString(decryptedBytes);

            using (StreamWriter sw = new StreamWriter("decryptedFile.txt", false, encoding))
            {
                sw.Write(encryptedText);
            }

        }
    }
}
