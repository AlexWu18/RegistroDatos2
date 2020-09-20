using System;
using System.IO;
namespace Char_by_Char
{
    class Program
    {
       private static string Char1;
        static void Main(string[] args)
        {
            Char1 = args[0];

                 if (!File.Exists(Char1))
            {
                using (StreamWriter file = new StreamWriter(Char1))
                {
                    file.WriteLine("Nombre" + "," + "Apellido" + "," + "Edad" + "," + "Ahorros" + "," + "Password");
                }
            }

            int Edad;
            string Nombre, Apellido, PassWord, RepassWord;
            decimal Ahorros;

            StreamWriter Archivo = File.AppendText(Char1);
            Console.Write("Nombre : ");
            Nombre = Console.ReadLine(); 
            Console.Write("Apellido : ");
            Apellido = Console.ReadLine();
            Console.Write("Edad : ");
            Edad = Convert.ToInt32(Console.ReadLine()); 
            Console.Write("Ahorros : ");
            Ahorros =Convert.ToDecimal(Console.ReadLine());
            Console.Write("Password : ");
            PassWord = Console.ReadLine(); 
            Aqui:
            Console.Write("Confirmar Password : ");
            RepassWord = Console.ReadLine(); 

            if (RepassWord == PassWord)
            {
                String Codigo = PassWord1();
                Archivo.WriteLine(Nombre + "," + Apellido + "," + Edad + "," + Ahorros + "," + PassWord);
                Archivo.Close();
            }
            if (RepassWord != PassWord)
            {
                String Codigo = PassWord1();
                Archivo.Close();
                goto Aqui;
                 
            }

        }     
        public static string PassWord1()
    {
        ConsoleKeyInfo VAB; 
        string XDS = string.Empty;

        do
        {
            VAB = Console.ReadKey(true);
            if (VAB. Key != ConsoleKey.Backspace && VAB.Key != ConsoleKey.Enter)
            {
                XDS += VAB.KeyChar;
                Console.WriteLine("*");
            }
            else if (VAB.Key == ConsoleKey.Backspace && XDS.Length > 0)
            {
                XDS = XDS.Substring(0, (XDS.Length - 1));
                Console.WriteLine("\b \b");
            }
              
        }  while ( VAB.Key != ConsoleKey.Enter);

        return XDS;


        }
    }
}
