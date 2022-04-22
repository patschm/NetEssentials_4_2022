using System.Security.Cryptography;
using System.Text;

//SimpleHash();
//SymmHMAC();
//AsymmHash();

// Confidentiality
//AsymmConfident();
SymmConfident();

void SymmConfident()
{
    string txt = "Hello World";
    Aes alg = Aes.Create();
    //alg.Mode = CipherMode.ECB;
    byte[] sleutel = alg.Key;
    byte[] iv = alg.IV;
    
    byte[] cipher;
    using (MemoryStream mem = new MemoryStream())
    {
        using(CryptoStream crypt = new CryptoStream(mem, alg.CreateEncryptor(), CryptoStreamMode.Write))
        using(StreamWriter writer = new StreamWriter(crypt))
        {
            writer.WriteLine(txt);
        }
        cipher = mem.ToArray();
    }
    System.Console.WriteLine(Encoding.UTF8.GetString(cipher));

    // Ontvanger
    Aes alg2 = Aes.Create();
    //alg2.Mode = CipherMode.ECB;
    alg2.Key = sleutel;
    alg2.IV = iv;

    using(MemoryStream mem = new MemoryStream(cipher))
    using(CryptoStream crypt = new CryptoStream(mem, alg2.CreateDecryptor(), CryptoStreamMode.Read))
    using (StreamReader rdr = new StreamReader(crypt))
    {
        System.Console.WriteLine(rdr.ReadToEnd());
    }

}

void AsymmConfident()
{
    // Ontvanger doet dit
    RSA rsaOntvanger = RSA.Create();
    string pubKey = rsaOntvanger.ToXmlString(false);

    // Naar de sender
    string txt = "Hello World";
    RSA rsaSender = RSA.Create();
    rsaSender.FromXmlString(pubKey);
    byte[] cipher = rsaSender.Encrypt(Encoding.UTF8.GetBytes(txt), RSAEncryptionPadding.Pkcs1); 
    System.Console.WriteLine(Encoding.UTF8.GetString(cipher));


    // Ontvanger
    byte[] data = rsaOntvanger.Decrypt(cipher, RSAEncryptionPadding.Pkcs1);
    System.Console.WriteLine(Encoding.UTF8.GetString(data));
}

void AsymmHash()
{
   string txt = "Hello World";
    SHA1 alg = SHA1.Create();
    byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(txt));

    DSA dsa = DSA.Create(); // new DSACryptoServiceProvider()
    string publkey = dsa.ToXmlString(false);
    System.Console.WriteLine(publkey);
    byte[] signature = dsa.SignData(hash, HashAlgorithmName.SHA1);


    txt += ".";
    // Ontvanger
     SHA1 alg2 = SHA1.Create();
    byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(txt));

    DSA dsa2 = DSA.Create(); // new DSACryptoServiceProvider()
    dsa2.FromXmlString(publkey);
    bool isOk = dsa2.VerifyData(hash2, signature, HashAlgorithmName.SHA1);
    System.Console.WriteLine(isOk?"Hij's goed":"Awww");
}

void SymmHMAC()
{
     string txt = "Hello World";
     HMACSHA512 alg = new HMACSHA512();
     byte[] key = alg.Key;
     byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(txt));
    System.Console.WriteLine(Convert.ToBase64String(hash));

    HMACSHA512 alg2 = new HMACSHA512();
    alg2.Key = key;
     byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(txt));
    System.Console.WriteLine(Convert.ToBase64String(hash2));
}

void SimpleHash()
{
    string txt = "Hello World";
    SHA1 alg = SHA1.Create();
    byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(txt));
    System.Console.WriteLine(Convert.ToBase64String(hash));

    txt+=".";
    SHA1 alg2 = SHA1.Create();
    byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(txt));
    System.Console.WriteLine(Convert.ToBase64String(hash2));
}
