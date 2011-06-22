using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Performance
{
    public interface IFixture
    {
        void Execute(Runner fixture);
    }
}
