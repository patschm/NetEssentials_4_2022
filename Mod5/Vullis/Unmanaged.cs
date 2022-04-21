using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace Vullis
{
    public class Unmanaged : IDisposable
    {
        private static bool _isOpen = false;
        private FileStream _stream;

        public void Open()
        {
            if(!_isOpen)
            {
                System.Console.WriteLine("Resource wordt geopend");
                _isOpen = true;
                _stream = File.OpenRead(@"D:\Netessentials_4_2022\Tmp\data.txt");
            }
            else
            {
                System.Console.WriteLine("Helaas. Is al open....");
            }            
        }
        public void Close()
        {
            System.Console.WriteLine("Closing resource..");
            _isOpen = false;
        }

        protected virtual void Dispose(bool _fromDispose)
        {
              Close();
              if(_fromDispose)
              {
                   _stream.Dispose();
              }
           
        }
        public void Dispose()
        {
           Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Unmanaged()
        {
            Dispose(false);
        }
    }
}