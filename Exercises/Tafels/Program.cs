using System.IO.Compression;

//SchrijfTafels();
LeestTafels();

void LeestTafels()
{
    FileStream fs = File.OpenRead(@"D:\Netessentials_4_2022\Tmp\tafels.zip");
    DeflateStream zip = new DeflateStream(fs, CompressionMode.Decompress);
    StreamReader reader = new StreamReader(zip);
    string? line = null;
    while((line = reader.ReadLine()) != null)
    {
        System.Console.WriteLine(line);
    }
}

void SchrijfTafels()
{
    FileStream fs = File.OpenWrite(@"D:\Netessentials_4_2022\Tmp\tafels.zip");
    DeflateStream zip = new DeflateStream(fs, CompressionMode.Compress);
    StreamWriter writer = new StreamWriter(zip);
    
    Console.SetOut(writer);
    for(int tafel = 1; tafel <=10; tafel++)
    {
        Console.WriteLine($"De tafel van {tafel}");
        for(int teller = 1; teller <= 10; teller++)
        {
            Console.WriteLine($"{teller} x {tafel} = {teller * tafel}");
        }
        Console.WriteLine();
    }
    writer.Flush();
    writer.Close();
}