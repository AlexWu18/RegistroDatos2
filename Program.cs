using System;
using System.IO;
namespace RegistroDato
{

    class Program
    {
        private static string Informacion;
        static void Main(string[] args)
        {
            Informacion = args[0];

            if (!File.Exists(Informacion))
            {
                using (StreamWriter file = new StreamWriter(Informacion))
                {
                    file.WriteLine("Cedula" + "," + "Nombre"+ "," + "Apellido" + "," + "Edad");
                }
            }

        string Num;
        Console.WriteLine("----Registro-------");
        Console.WriteLine("[1] Agregar usuario");
        Console.WriteLine("[2] Ver Lista");
        Console.WriteLine("[3] Buscar Cedula");
        Console.WriteLine("[4] Salida");
        
        Console.WriteLine("SELECCIONE UN NUMERO DEL 1-4");
        Num = Console.ReadLine();
        Console.Clear();
        
        switch(Num)
        {
            case "1":

            programa:
            string Nom, Ape, Cedu, A1, A2;
            int Edad;

            StreamWriter archivo = File.AppendText(Informacion);
            Console.Write("Cedula:");
            Cedu = Console.ReadLine();
            Console.Write("Nombre:");
            Nom = Console.ReadLine();
            Console.Write("Apellido:");
            Ape = Console.ReadLine();
            Console.Write("Edad:");
            Edad = Convert.ToInt32( Console.ReadLine());

            Console.WriteLine("Que quieres hacer?");
            Console.WriteLine("Salvar[S]  ||   Rehacer[R]   || Cerrrar[C] ");
            A1 = Console.ReadLine();
            A2 = A1.ToLower();

            if(A2 == "s")
            {
                archivo.WriteLine( Cedu +"," + Nom + "," + Ape + "," + Edad);
                archivo.Close();
            }
            if(A2 == "r")
            {
                archivo.Close();
                goto programa;
            }
            if(A2 == "c")
            {
                archivo.Close();
                goto Final;
            }
            
            Final:
            Console.WriteLine("Finalizado el programa.........");

            break;

            case "2" :
            {
                TextReader Leer;
                Leer = new StreamReader(Informacion);
                Console.WriteLine(Leer.ReadToEnd());
                Console.ReadKey();
                Leer.Close();

            break;
            }
        
            case "3":

                string Cosa;
                string Azul = "No Existe";

                Console.Write("Escriba la Cedula:");
                Cosa = Console.ReadLine();
                foreach(string line in File.ReadLines(Informacion))
                {
                    if( line.Contains(Cosa))
                    {
                        var arr = line.Split(",");
                        if(arr[0] == Cosa)
                        {
                            Azul = line;
                        }
                    }
                }
                Console.WriteLine(Azul);
                Console.ReadLine();
             break;

            case "4":
                Console.WriteLine("EL PROGRAMA HA FINALIZADO.....");
            break;


             }
        }
    }
}
