using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryApp_Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            {
                LibraryApp_DLL.password password = new LibraryApp_DLL.password();
                password.login();
            }

            {
                LibraryApp_DLL.Class1 first = new LibraryApp_DLL.Class1();
                first.Start();
            }
        }
    }
}
