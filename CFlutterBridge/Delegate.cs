using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CFlutter_Bridge
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StringDelegate(string str);
}
