using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{
    public static class Encryptor
    {
        private static Encoding encoding = Encoding.GetEncoding("windows-1251");

        private static string rusAlph = "АаВЕеКМОоРрСсуХх";
        private static string engAlph = "AaBEeKMOoPpCcyXx";

        public static void EncryptorText(string containerFilePath, string encrtypedText)
        {

            byte[] encodedBytes = encoding.GetBytes(encrtypedText); // переводим текст в байты

            StringBuilder sb = new StringBuilder();

            StringBuilder encodedBits = new StringBuilder();


            for (int i = 0; i < encodedBytes.Length; i++)
            {
                encodedBits.Append(Convert.ToString(encodedBytes[i], 2).PadLeft(8, '0'));
            }

            string newencodedBits = encodedBits.ToString();

            Console.WriteLine("Зашифрованные биты:");

            foreach (var elem in newencodedBits)
            {
                Console.Write(elem);
            }

            try
            {
                using (StreamReader sr = new StreamReader(containerFilePath, encoding))
                {
                    string text = sr.ReadToEnd();

                    int i = 0;

                    bool flag = true;

                    foreach (var elem in text)
                    {
                        int index = rusAlph.IndexOf(elem);

                        if (index != -1 && flag)
                        {
                            if (newencodedBits[i].Equals('1')) 
                            {
                                sb.Append(engAlph[index]);
                            }
                            else if (newencodedBits[i].Equals('0'))
                            {
                                sb.Append(rusAlph[index]);
                            }
                            if (i < newencodedBits.Length) 
                            {
                                i++;
                            }
                            if (i == newencodedBits.Length)
                                flag = false;
                        }

                        else
                        {
                            sb.Append(elem);
                        } 

                    }
                }

                using (StreamWriter sw = new StreamWriter("EncryptedText.txt", false, encoding))
                {
                    sw.Write(sb);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


        }


    }
}
