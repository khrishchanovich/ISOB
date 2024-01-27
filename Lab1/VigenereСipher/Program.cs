using System;

class VigenereCipher
{
    static char EncryptChar(char plainChar, char keyChar)
    {
        if (!char.IsLetter(plainChar))
            return plainChar;

        char baseChar = 'A';
        return (char)(((plainChar + keyChar - 2 * baseChar) % 26) + baseChar);
    }

    static char DecryptChar(char cipherChar, char keyChar)
    {
        if (!char.IsLetter(cipherChar))
            return cipherChar;

        char baseChar = 'A';
        int result = ((cipherChar - keyChar + 26) % 26) + baseChar;
        return (char)result;
    }

    public static string Encrypt(string plaintext, string key)
    {
        plaintext = plaintext.ToUpper();
        key = key.ToUpper(); 

        string encryptedText = "";
        int keyIndex = 0;

        foreach (char c in plaintext)
        {
            if (char.IsWhiteSpace(c))
            {
                encryptedText += c;
                continue;
            }

            char keyChar = key[keyIndex];
            encryptedText += EncryptChar(c, keyChar);
            keyIndex = (keyIndex + 1) % key.Length;
        }

        return encryptedText;
    }

    public static string Decrypt(string ciphertext, string key)
    {
        ciphertext = ciphertext.ToUpper(); 
        key = key.ToUpper();

        string decryptedText = "";
        int keyIndex = 0;

        foreach (char c in ciphertext)
        {
            if (char.IsWhiteSpace(c))
            {
                decryptedText += c;
                continue;
            }

            char keyChar = key[keyIndex];
            decryptedText += DecryptChar(c, keyChar);
            keyIndex = (keyIndex + 1) % key.Length;
        }

        return decryptedText;
    }


    static void Main()
    {
        string encryptedText;
        try
        {
            encryptedText = File.ReadAllText(@"D:\6 semester\IN-SB\Lab1\VigenereСipher\encryptedtext.txt");
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
        var secretKey = Console.ReadLine();

        string ciphertext = Encrypt(encryptedText, secretKey);
        Console.WriteLine("Encrypted: " + ciphertext);

        string decryptedText = Decrypt(ciphertext, secretKey);
        Console.WriteLine("Decrypted: " + decryptedText);
    }
}
