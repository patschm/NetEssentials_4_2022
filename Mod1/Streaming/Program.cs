using System.IO.Compression;
using System.Text;

namespace Streaming;
class Program 
{
    static void Main()
    {
        //BasicWrite();
        //BasicRead();
        //AdvancedWrite();
        //AdvancedRead();
        AdvancedWriteCompressed();
        System.Console.ReadLine();
    }

    private static void AdvancedWriteCompressed()
    {
        FileInfo fi = new FileInfo(@"D:\NETEssential\Tmp\data.zip");
        FileStream fs = fi.Create();
        GZipStream zip = new GZipStream(fs, CompressionMode.Compress);
        StreamWriter writer = new StreamWriter(zip);
        for(int i = 0; i < 1000; i++)
        {         
            writer.WriteLine($"Hello World {i}");
        }
        writer.Flush();
        writer.Close();
        fs.Close();
    }
    private static void AdvancedRead()
    {
        FileInfo fi = new FileInfo(@"D:\NETEssential\Tmp\data2.txt");
        FileStream fs = fi.OpenRead();
        StreamReader rdr = new StreamReader(fs);
        string? line;
        while((line = rdr.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
        }
    }

    private static void AdvancedWrite()
    {
        FileInfo fi = new FileInfo(@"D:\NETEssential\Tmp\data2.txt");
        FileStream fs = fi.Create();
        StreamWriter writer = new StreamWriter(fs);
        for(int i = 0; i < 1000; i++)
        {         
            writer.WriteLine($"Hello World {i}");
        }
        writer.Flush();
        writer.Close();
        fs.Close();
    }

    private static void BasicRead()
    {
        FileInfo fi = new FileInfo(@"D:\NETEssential\Tmp\data.txt");

        FileStream fs = fi.OpenRead();

        byte[] buffer = new byte[4];
        while (fs.Read(buffer, 0, buffer.Length) > 0)
        {          
            string line = Encoding.UTF8.GetString(buffer);
            System.Console.Write(line);
            Array.Clear(buffer);
        }     
    }

    private static void BasicWrite()
    {
        // File, Directory, Path
        // FileInfo, DirectoryInfo, DriveInfo
        FileInfo fi = new FileInfo(@"D:\NETEssential\Tmp\data.txt");
        FileStream? fs = null;
        string txt = "Hello World";

        if (!fi.Exists)
        {
            fs = fi.Create();
            for(int i = 0; i < 1000; i++)
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"{txt} {i}\n\r");
                fs.Write(buffer, 0, buffer.Length);
            }
        }
       fs?.Close();    
    }
}