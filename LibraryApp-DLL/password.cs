using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LibraryApp_DLL
{
    public class password
    {
        public void login()
        {
            while (true)
            {
                WriteLine("-*-*-*- ENTER YOUR PASSWORD -*-*-*- ");
                string password = ReadLine();

                if (password == "5355")
                {
                    WriteLine("**** You Have Successfully Logged In ****" +
                        "\n              Press any key! "
                        );
                    ReadKey(true);
                    break;

                }

                else
                {
                    WriteLine("**** WRONG PASSWORD! ****" +
                        "\n Press any key to try again. " +
                        "\n If you want to exit, press the cross in the upper right corner.");
                    Console.ReadKey(true);

                }
            }
        }
    }
}
