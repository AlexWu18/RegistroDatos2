using System;
using System.IO;
using System.Text;
namespace Version_3
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
        Console.WriteLine("[5] Editar");
        Console.WriteLine("[6] Eliminar");
        
        
        Console.WriteLine("SELECCIONE UN NUMERO DEL 1-6");
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

            case"5":
             string Cosa1;
                string Azul1 = "No Existe";

                Console.Write("Escriba la Cedula que va editar:");
                Cosa1 = Console.ReadLine();
                foreach(string line in File.ReadLines(Informacion))
                {
                    if( line.Contains(Cosa1))
                    {
                        var arr = line.Split(",");
                        if(arr[0] == Cosa1)
                        {
                            Azul1 = line;
                        }
                    }
                }
                Console.WriteLine(Azul1);
                Console.ReadLine();

                using(FileStream EDI = File.Create(Informacion))
                {
                    Byte[] NUEVOIF = new UTF8Encoding(true).GetBytes("Cedula,Nombre,Apellido,Edad");
                    EDI.Write(NUEVOIF, 0 , NUEVOIF.Length);
                }
                Console.ReadKey();
            break;

                case "6":
                string Cosa2;
                string Azul2 = "No Existe";

                Console.Write("Escriba la CeduCla que vayas a eliminar:");
                Cosa2 = Console.ReadLine();
                foreach(string line in File.ReadLines(Informacion))
                {
                    if( line.Contains(Cosa2))
                    {
                        var arr = line.Split(",");
                        if(arr[0] == Cosa2)
                        {
                            Azul2 = line;
                        }
                    }
                }
                Console.WriteLine(Azul2);
                Console.ReadLine();

                string SI, NO;
                Console.Write("Continuar: SI [S] y NO [N] : ");
                SI = Console.ReadLine();
                NO = SI.ToLower(); 
                
                if(NO == "s")
                {
                 string Buscar = Cosa2;

                 string[] values = File.ReadAllText(Informacion).Split(new char[] { '\n' });
                 StringBuilder Lectura = new StringBuilder();

                 for(int i = 0; i < values.Length; i++ )
                 {
                     if(values[i].Split(',')[0] == Buscar) continue;
                    Lectura.AppendLine(values[i].TrimEnd('\r'));
                 }
                 File.WriteAllText(Informacion, Lectura.ToString().TrimEnd(new char[]{ '\n','\r'}),Encoding.UTF8);

                }
                if(NO == "n")
                {
                    Console.WriteLine("NO SE HA BORRADO NADA, INTENTE OTRA VEZ....");
                }

                break;



            }
        }
    }
}
