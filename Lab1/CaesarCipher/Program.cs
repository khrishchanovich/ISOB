using System;
using System.IO;

public class CaesarCipher
{

    // Объявляем метод, который будет принимать текст и ключ
    private string CodeEncode(string text, int k)
    {
        // Создаем переменную, которая содержит в себе строчные и заглавные буквы алфавита (для обработки символов разных регистров)
        const string fullAlfabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        var letterQty = fullAlfabet.Length;
        var result = "";

        for (int i = 0; i < text.Length; i++)
        {
            var current = text[i];
            var isUpper = Char.IsUpper(current); 
            var index = fullAlfabet.IndexOf(Char.ToUpper(current));

            if (index < 0)
            {
                // Если символ не найден, то он же и вернется
                result += current.ToString();
            }
            else
            {
                // Обеспечиваем уикличность сдвига
                var codeIndex = (letterQty + index + k) % letterQty;
                result += isUpper ? Char.ToUpper(fullAlfabet[codeIndex]) : Char.ToLower(fullAlfabet[codeIndex]);
            }
        }

        return result;
    }


    public string Encrypt(string plainText, int key)
        => CodeEncode(plainText, key);

    public string Decrypt(string encryptedText, int key)
        => CodeEncode(encryptedText, -key);
}

class Program
{
    static void Main(string[] args)
    {
        var cipher = new CaesarCipher();

        // Чтение текста из файла
        string plainText;
        try
        {
            plainText = File.ReadAllText(@"D:\6 semester\IN-SB\Lab1\CaesarCipher\encryptedtext.txt");
        }
        catch (IOException e)
        {
            Console.WriteLine("Ошибка при чтении файла: " + e.Message);
            return;
        }

        Console.WriteLine("Текст из файла:");
        Console.WriteLine(plainText);
        Console.WriteLine();

        Console.Write("Введите значение ключа: ");
        var secretKey = Convert.ToInt32(Console.ReadLine());

        var encryptedText = cipher.Encrypt(plainText, secretKey);

        Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText);
        Console.WriteLine("Расшифрованное сообщение: {0}", cipher.Decrypt(encryptedText, secretKey));

    }
}

