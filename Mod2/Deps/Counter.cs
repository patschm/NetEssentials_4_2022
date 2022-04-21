using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deps
{
    public interface ICounter
    {
        void Increment();
    }

    public class Counter : ICounter
    {
        private int _counter = 0;

        public void Increment()
        {
            System.Console.WriteLine(++_counter);
        }
    }
}