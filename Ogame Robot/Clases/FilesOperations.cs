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
        private static string pathSettings = "SettingsCommands.txt";

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

                                try//new setting implementation
                                {
                                    int id = Settings.getID(line.Split('=')[0]);
                                    if (id != -1)
                                    {
                                        inicializationFile.settings.properties.Add(id, line.Split('=')[1]);
                                    }
                                    else
                                    {
                                        ErrorLogFileAddError(new Exception("I cant find this command:" + line.Split('=')[0]));
                                    }
                                }
                                catch (Exception)
                                {
                                    //for example:when ini contains two same commands it will save the first
                                }
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
                    foreach (var item in DataBase.dataBase.inicializationFile.settings.properties)
                    {
                        sw.WriteLine(DataBase.dataBase.inicializationFile.settings.settings[item.Key]+"="+DataBase.dataBase.inicializationFile.settings.properties[item.Key]);
                    }
                    sw.WriteLine("#");



                }
            }
        }
        /*
        public static void InicializationFileLoadAdds()
        {
            DataBase.InicializationFile inicializationFile = new DataBase.InicializationFile();
            using (FileStream fs = new FileStream(pathInicialization, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {

                    if (new FileInfo(pathInicialization).Length == 0)
                    {
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
    */

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
        public static void ErrorLogFileAddLines(string[] lines)
        {
            using (StreamWriter stream = File.AppendText(pathErrorLog))
            {
                for (int i = 0; i < lines.Count(); i++)
                {
                    stream.WriteLine(lines[i]);
                }
            }

        }


        public static Dictionary<int, string> SettingsFileLoad()
        {
            Dictionary<int, string> settings = new Dictionary<int, string>();
            string[] lines;
            try
            {
                lines = File.ReadAllLines(pathSettings);
                foreach (string item in lines)
                {
                    settings.Add(settings.Keys.Count(), item);
                }
            }
            catch (Exception e)
            {
                ErrorLogFileAddError(e);
                ErrorLogFileAddLines(new string[] { "Probably I cant find Settings file:" + pathSettings });
            }

            return settings;
        }


    }
}
