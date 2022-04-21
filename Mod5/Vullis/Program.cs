using System.Diagnostics;
using System.Text;

namespace Vullis;

class Program
{
    static Unmanaged res1 = new Unmanaged();
    static Unmanaged res2 = new Unmanaged();

    static void Main()
    {
        //string data = "";
        StringBuilder data = new StringBuilder();

        Stopwatch sw = new();
        sw.Start();
        for(int i = 0; i < 100000; i++)
        {
            data.Append(i.ToString());
        }
        sw.Stop();
        System.Console.WriteLine(sw.Elapsed);

        //res1 = new Unmanaged();
        try
        {
            res1.Open();
        }
        finally
        {
            res1.Dispose();
        }

        res1 = null;

    

        //res2 = new Unmanaged();
        using(res2)
        {
            res2.Open();
        }

        res2=null;


        using (Unmanaged res3 = new())
        {
            res3.Open();
        }
        
        GC.Collect();
        GC.WaitForPendingFinalizers();
      
    }
}

