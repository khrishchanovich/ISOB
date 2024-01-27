using System;
using System.IO;

public class CaesarCipher
{
    const string alfabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    private string CodeEncode(string text, int k)
    {
        var fullAlfabet = alfabet + alfabet.ToLower();
        var letterQty = fullAlfabet.Length;
        var retVal = "";
        for (int i = 0; i < text.Length; i++)
        {
            var c = text[i];
            var isUpper = Char.IsUpper(c); 
            var index = fullAlfabet.IndexOf(Char.ToUpper(c));
            if (index < 0)
            {
                retVal += c.ToString();
            }
            else
            {
                var codeIndex = (letterQty + index + k) % letterQty;
                retVal += isUpper ? Char.ToUpper(fullAlfabet[codeIndex]) : Char.ToLower(fullAlfabet[codeIndex]);
            }
        }

        return retVal;
    }


    public string Encrypt(string plainMessage, int key)
        => CodeEncode(plainMessage, key);

    public string Decrypt(string encryptedMessage, int key)
        => CodeEncode(encryptedMessage, -key);
}

class Program
{
    static void Main(string[] args)
    {
        var cipher = new CaesarCipher();

        // Чтение текста из файла
        string encryptedText;
        try
        {
            encryptedText = File.ReadAllText(@"D:\6 semester\IN-SB\Lab1\CaesarCipher\encryptedtext.txt");
        }
        catch (IOException e)
        {
            Console.WriteLine("Ошибка при чтении файла: " + e.Message);
            return;
        }

        Console.WriteLine("Текст из файла:");
        Console.WriteLine(encryptedText);
        Console.WriteLine();

        Console.Write("Введите значение ключа: ");
        var secretKey = Convert.ToInt32(Console.ReadLine());

        var encryptedMessage = cipher.Encrypt(encryptedText, secretKey);

        Console.WriteLine("Зашифрованное сообщение: {0}", encryptedMessage);
        Console.WriteLine("Расшифрованное сообщение: {0}", cipher.Decrypt(encryptedMessage, secretKey));
        Console.ReadLine();

    }
}

