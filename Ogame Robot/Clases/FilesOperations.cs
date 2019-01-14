using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public class FilesOperations
    {
        private static string pathInicialization = "Inicialization.txt";
        private static string pathErrorLog = "ErrorLog.txt";

        public static void InicializationFileLoad()
        {
            DataBase.InicializationFile inicializationFile = new DataBase.InicializationFile();
            using (FileStream fs = new FileStream(pathInicialization, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {

                    if (new FileInfo(pathInicialization).Length == 0)
                    {
                        /*
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("username=");
                            sw.WriteLine("password=");
                            sw.WriteLine("autologin=");

                        }
                        */
                    }
                    else
                    {
                        inicializationFile.username = sr.ReadLine().Split('=')[1];
                        inicializationFile.password = sr.ReadLine().Split('=')[1];
                        inicializationFile.autologin = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);


                        if (sr.ReadLine() == "#")
                        {
                            string line = sr.ReadLine();
                            while (line != "#")
                            {

                                inicializationFile.addLines.Add(line);
                                line = sr.ReadLine();
                            }
                        }
                        else
                        {
                            sr.Close();
                            fs.Close();
                            try
                            {
                                System.IO.File.Move(pathInicialization, "InicializationBuggedOld.txt");
                            }
                            catch (System.IO.IOException e)
                            {
                                int i = 0;
                            }
                            return;
                        }

                        DataBase.dataBase.inicializationFile = inicializationFile;

                    }
                }

            }

        }
        public static void InicializationFileSave()
        {
            using (FileStream fs = new FileStream(pathInicialization, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("username=" + DataBase.dataBase.inicializationFile.username);
                    sw.WriteLine("password=" + DataBase.dataBase.inicializationFile.password);
                    sw.WriteLine("autologin=" + Convert.ToString(DataBase.dataBase.inicializationFile.autologin));

                    //aditional data
                    sw.WriteLine("#");
                    for (int i = 0; i < DataBase.dataBase.inicializationFile.addLines.Count(); i++)
                    {
                        sw.WriteLine(DataBase.dataBase.inicializationFile.addLines[i]);
                    }
                    sw.WriteLine("#");



                }
            }
        }
        public static void InicializationFileLoadAdds()
        {
            DataBase.InicializationFile inicializationFile = new DataBase.InicializationFile();
            using (FileStream fs = new FileStream(pathInicialization, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {

                    if (new FileInfo(pathInicialization).Length == 0)
                    {
                        /*
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("username=");
                            sw.WriteLine("password=");
                            sw.WriteLine("autologin=");

                        }
                        */
                    }
                    else
                    {
                        string line = "";

                        while (line != "#")
                        {
                            line = sr.ReadLine();
                        }
                        line = sr.ReadLine();
                        while (line != "#")
                        {

                            inicializationFile.addLines.Add(line);
                            line = sr.ReadLine();
                        }

                        DataBase.dataBase.inicializationFile.addLines = inicializationFile.addLines;

                    }
                }

            }

        }     


        public static void ErrorLogFileAddError(Exception exception)
        {
            using (StreamWriter stream = File.AppendText(pathErrorLog))
            {
                stream.WriteLine(DateTime.Now.ToString());
                stream.WriteLine(exception.Message);
                stream.WriteLine(exception.StackTrace);
                stream.WriteLine();
            }

        }
        public static void ErrorLogFileAddLines(string[] lines )
        {
            using (StreamWriter stream = File.AppendText(pathErrorLog))
            {
                for (int i = 0; i < lines.Count(); i++)
                {
                    stream.WriteLine(lines[i]);
                }
            }

        }




    }
}
