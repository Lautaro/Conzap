using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Module
{
    public interface IConzapModule
    {
        string Title { get; }
        void Execute();
    }
}
