using System;
using System.Linq;
using System.Text;

namespace Encryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string encryptedText = "сегодня в 5 утра атакуем";

            string containerFile = "voyna-i-mir-tom-1.txt";

            Encryptor.EncryptorText(containerFile, encryptedText);

            Decryptor.DecryptorText("EncryptedText.txt");
        }
    }
}
