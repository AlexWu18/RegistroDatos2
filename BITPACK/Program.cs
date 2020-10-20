using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace BITPACK
{ class Program
    {
        private static string fileName;
        static void Main(string[] args)
        {
            fileName = args[0];
            var isNew = !(File.Exists(fileName));
            if (isNew)
            {
                File.Create(fileName).Close();
                File.WriteAllText(fileName,"Cedula,Nombre,Apellido,Ahorros,Contraseña,Datos");
                Console.WriteLine("Archivo Creado exitosamente");
            }
            int cont = 1;
            while (cont == 1)
            {
                Menu();
            }
        }
        public static string CaptureData(bool isNew=true, string oldId=null)
        {

            Console.WriteLine("Captura de Datos");
            var id = "";
            if (isNew)
            {
                Console.Write("Cedula: ");
                id = ValidateInt();
            }
            else
                id = oldId;
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            var apellido = Console.ReadLine();
            Console.Write("Ahorros: ");
            var ahorros = ValidateDouble();
            passwordInput:
            Console.Write("Contraseña: ");
            var password = MaskPassword();
            Console.Write("Confirmar Contraseña: ");
            var confirmPassword = MaskPassword();
            if((string.IsNullOrEmpty(password)) || !(password.Equals(confirmPassword)))
            {
                Console.WriteLine("Las contraseñas no coinciden");
                goto passwordInput;
            }
            var encryptedData = EncodeData();

            return $"{id},{nombre},{apellido},{ahorros},{password},{encryptedData}";
        }
        public static void CaptureMenu(string record)
        {
   
            Console.WriteLine("Guardar (G), Reiniciar (R), Salir (S)");
            var opcion = Console.ReadLine();
            switch (opcion.ToUpper())
            {
                case "G":
                    SaveData(record);
                    break;
                case "R":
                    CaptureMenu(CaptureData());
                    break;
                case "S":
                    break;
                default:
                    CaptureMenu(record);
                    break;
            }
        }
        public static void SaveData(string record) {
            record = Environment.NewLine + record;
            File.AppendAllText(fileName, record);
            Console.WriteLine("Registro insertado");
        }
        public static void Menu()
        {
            Console.WriteLine("Menu de Opciones");
            Console.WriteLine("1. Capturar");
            Console.WriteLine("2. Lista");
            Console.WriteLine("3. Buscar uno");
            Console.WriteLine("4. Modificar");
            Console.WriteLine("5. Eliminar");
            Console.WriteLine("6. Salir");
            var op = ValidateInt();
            switch (op)
            {
                case "1":
                    CaptureMenu(CaptureData());
                        break;
                case "2":
                   PrintData(GetAllData());
                    break;
                case "3":
                    Console.Write("Digite identificador: ");
                    var ced = ValidateInt();
                    PrintData(GetOneRecord(ced));
                    break;
                case "4":
                    ModifyData();
                    break;
                case "5":
                    DeleteData();
                    break;
                case "6":
                    Environment.Exit(1);
                    break;
                default:
                    Menu();
                    break;


            }

        }
        public static List<string> GetAllData()
        {
            string[] datos = File.ReadAllLines(fileName);
            List<string> lista = new List<string>();
            for(int i =0; i < datos.Length; i++)
            {
                if (i == 0)
                    datos[i] = datos[i].Replace("Datos", "Edad,Sexo,Estado Civil, Grado Academico");
                else
                {
                    var arr = datos[i].Split(",").ToList();
                    var encryptedValue = int.Parse(arr[arr.Count() - 1]);
                    arr.RemoveAt(arr.Count() - 1);
                    string values = string.Join(",", arr.ToArray());
                    var decryptedData = DecodeData(encryptedValue);
                    datos[i] = $"{values},{decryptedData}";
                }
                lista.Add(datos[i]);
            }
            return lista;
            
        }
        private static int SearchData(List<string> data, string identificador)
        {
            int indexValue = 0;
            foreach (var line in data)
            {
                if (line.Contains(identificador))
                {
                    var arr = line.Split(",");
                    if (arr[0].Trim() == identificador)
                    {
                        indexValue = data.IndexOf(line);
                        break;
                    }
                }
            }
            return indexValue;
        }
        public static List<string> GetOneRecord(string identificador)
        {
            List<string> data = GetAllData();
            List<string> record = new List<string>();
            record.Add(data[0].Replace("Datos", "Edad,Sexo,Estado Civil,Grado Academico"));
            record.Add("No Existe el record");
            int indexData = SearchData(data, identificador);
            if (indexData > 0)
            {
                record[1] = data[indexData];
            }
            
            return record;

        }
        public static void PrintData(List<string> data)
        {
            foreach(var line in data)
            {
                var arr = line.Split(",").ToList();
                foreach(var col in arr)
                {
                    if (col.Trim() == "Cedula")
                        Console.Write($"{col}\t\t");
                    else
                    {
                            Console.Write($"{col}\t\t");
                    }
                       
                }
                Console.WriteLine();
            }
        }
        public static void ModifyData()
        {
            Console.Write("Digite Identificador: ");
            var id = ValidateInt();
            var lista = GetOneRecord(id);
            if (lista[1].Split(',').Count() < 4)
            {
                Console.WriteLine(lista[1]);
                Console.WriteLine("No se encontro el registro, Desea continuar? S/N");
                var resp = Console.ReadLine();
                switch (resp.ToUpper())
                {
                    case "S":
                        ModifyData();
                        break;
                    case "N":
                        break;
                }
            }
            else
            {
                var data = File.ReadAllText(fileName);
                int indexVal = SearchData(GetAllData(), id);
                var oldData = GetAllData()[indexVal];
                var newData = CaptureData(false, id);
                ModifyMenu(data, oldData, newData);
            }
        }
        public static void ModifyMenu(string data, string oldData, string newData)
        {
            Console.WriteLine("Modificar (M), Reiniciar (R), Salir (S)");
            var resp = Console.ReadLine();
            switch (resp.ToUpper())
            {
                case "M":
                    File.WriteAllText(fileName, data.Replace(oldData, newData));
                    break;
                case "R":
                    ModifyData();
                    break;
                case "S":
                    break;
                default:
                    ModifyMenu(data, oldData, newData);
                    break;
            }
        }
        public static void DeleteMenu(string data, string record)
        {
            Console.WriteLine("Desea eliminar este registro? SI (S) / NO (N)");
            var resp = Console.ReadLine();
            switch (resp.ToUpper())
            {
                case "S":
                    File.WriteAllText(fileName, data.Replace(Environment.NewLine + record, "").Trim());
                    break;
                case "N":
                    break;
                default:
                    DeleteMenu(data, record);
                    break;
            }
        }
        public static void DeleteData()
        {
            Console.Write("Digite Identificador: ");
            var id = ValidateInt();
            var lista = GetOneRecord(id);
            if (lista[1].Split(',').Count() < 4)
            {
                Console.WriteLine(lista[1]);
                Console.WriteLine("No se encontro el registro. Desea continuar? S/N");
                var resp = Console.ReadLine();
                switch (resp.ToUpper())
                {
                    case "S":
                        DeleteData();
                        break;
                    case "N":
                        break;
                }
            }
            else
            {
                var data = File.ReadAllText(fileName);
                var indexValue = SearchData(GetAllData(), id);
                var record = GetAllData()[indexValue];
                DeleteMenu(data, record);
            }
        }
        public static string ValidateInt()
        {
            string intValue = "";
            readingKey:
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key != ConsoleKey.Backspace)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.NumPad0:
                        case ConsoleKey.D0:
                            Console.Write("0");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            Console.Write("1");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            Console.Write("2");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            Console.Write("3");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            Console.Write("4");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            Console.Write("5");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.D6:
                            Console.Write("6");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            Console.Write("7");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad8:
                        case ConsoleKey.D8:
                            Console.Write("8");
                            intValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            Console.Write("9");
                            intValue += key.KeyChar;
                            break;
                    }
                }
                else
                {
                    if (intValue.Length > 0)
                    {
                        intValue = intValue.Remove(intValue.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                key = Console.ReadKey(true);
            }
            if (key.Key == ConsoleKey.Enter && intValue.Length == 0)
                goto readingKey;
            Console.WriteLine();
            return intValue;
        }
        public static string ValidateDouble()
        {
            string doubleValue = "";
            int dotCount = 0;
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key != ConsoleKey.Backspace)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.NumPad0:
                        case ConsoleKey.D0:
                            Console.Write("0");

                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            Console.Write("1");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            Console.Write("2");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            Console.Write("3");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            Console.Write("4");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            Console.Write("5");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.D6:
                            Console.Write("6");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            Console.Write("7");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad8:
                        case ConsoleKey.D8:
                            Console.Write("8");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            Console.Write("9");
                            doubleValue += key.KeyChar;
                            break;
                
                        case ConsoleKey.OemPeriod:
                            if(dotCount == 0)
                            {
                                dotCount++;
                                Console.Write(".");
                                doubleValue += key.KeyChar;
                              
                            }
                            break;

                    }
                }
                else
                {
                    if (doubleValue.Length > 0)
                    {
                        char character = doubleValue.Last();
                        if (character == '.')
                            dotCount = 0;
                        doubleValue = doubleValue.Remove(doubleValue.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                key = Console.ReadKey(true);
            }
            Console.WriteLine();
            return doubleValue;
        }
    
        public static string MaskPassword()
        {
            string psw = "";
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    psw += key.KeyChar;
                }
                else
                {
                    if (psw.Length > 0)
                    {
                        psw = psw.Remove(psw.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                key = Console.ReadKey(true);
               
            }
            Console.WriteLine();
            return psw;
        }
        public static int EncodeData()
        {
            int encription = 0;
            Console.WriteLine("Edad: ");
            int age = int.Parse(ValidateInt());

            encription = encription | age;
            encription = encription << 4;
            genderInput:
            Console.WriteLine("Sexo: M|F ");
            char gender = Console.ReadLine().ToCharArray()[0];
            switch (gender.ToString().ToUpper())
            {
                case "F":
                    encription = encription | 8;
                    break;
                case "M":
                    break;
                default:
                    goto genderInput;
            }
            statusInput:
            Console.WriteLine("Estado Civil: S|C ");
            char status = Console.ReadLine().ToCharArray()[0];
            switch (status.ToString().ToUpper())
            {
                case "C":
                    encription = encription | 4;
                    break;
                case "S":
                    break;
                default:
                    goto statusInput;
            }
            gradeInput:
            Console.WriteLine("Grado Academico");
            Console.WriteLine("(I) Inicial");
            Console.WriteLine("(M) Media");
            Console.WriteLine("(G) Grado");
            Console.WriteLine("(P) PostGrado");
            Console.Write("Opcion: ");
            char grade = Console.ReadLine().ToCharArray()[0];
            switch (grade.ToString().ToUpper())
            {
                case "I":
                    break;
                case "M":
                    encription = encription | 1;
                    break; 
                case "G":
                    encription = encription | 2;
                    break;
                case "P":
                    encription = encription | 3;
                    break;
                default:
                    goto gradeInput;
            }
            return encription;
        }
        public static string DecodeData(int encription)
        {
            string grade;
            if (encription == (encription | 1))
                grade = "Media";
            else if (encription == (encription | 2))
                grade = "Grado";
            else if (encription == (encription | 3))
                grade = "PostGrado";
            else
                grade = "Basica";

            string status;
            if (encription == (encription | 4))
                status = "Casado";
            else
                status = "Soltero";

            string gender;
            if (encription == (encription | 8))
                gender = "Femenino";
            else
                gender = "Masculino";

            int age = encription >> 4;
            return $"{age},{gender},{status},{grade}";

        }
    }
}
