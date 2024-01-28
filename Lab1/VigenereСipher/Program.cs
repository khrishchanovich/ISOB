using System;

class VigenereCipher
{
    static char EncryptChar(char plainChar, char keyChar)
    {
        if (!char.IsLetter(plainChar))
            return plainChar;

        // Используем алфавит A-Z (65)
        char baseChar = 'A';
        return (char)(((plainChar + keyChar) % 26) + baseChar);
    }

    static char DecryptChar(char encryptedChar, char keyChar)
    {
        if (!char.IsLetter(encryptedChar))
            return encryptedChar;

        char baseChar = 'A';
        int result = ((encryptedChar - keyChar + 26) % 26) + baseChar;
        return (char)result;
    }

    public static string Encrypt(string plaintext, string key)
    {
        // Проводим все в заглавных буквах - обязательно переводим весь текст в заглавные буквы
        plaintext = plaintext.ToUpper();
        key = key.ToUpper(); 

        string encryptedText = "";
        int keyIndex = 0;

        foreach (char current in plaintext)
        {
            if (char.IsWhiteSpace(current))
            {
                encryptedText += current;
                continue;
            }

            char keyChar = key[keyIndex];
            encryptedText += EncryptChar(current, keyChar);
            keyIndex = (keyIndex + 1) % key.Length;
        }

        return encryptedText;
    }

    public static string Decrypt(string encryptedText, string key)
    {
        encryptedText = encryptedText.ToUpper(); 
        key = key.ToUpper();

        string decryptedText = "";
        int keyIndex = 0;

        foreach (char current in encryptedText)
        {
            if (char.IsWhiteSpace(current))
            {
                decryptedText += current;
                continue;
            }

            char keyChar = key[keyIndex];
            decryptedText += DecryptChar(current, keyChar);
            keyIndex = (keyIndex + 1) % key.Length;
        }

        return decryptedText;
    }


    static void Main()
    {
        string plainText;
        try
        {
            plainText = File.ReadAllText(@"D:\6 semester\IN-SB\Lab1\VigenereСipher\encryptedtext.txt");
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
        var secretKey = Console.ReadLine();

        string encryptedText = Encrypt(plainText, secretKey);
        Console.WriteLine("Encrypted: " + encryptedText);
        Console.WriteLine("Decrypted: " + Decrypt(encryptedText, secretKey));
    }
}
