using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Collections;

namespace csharp_files_intro
{


    /// esempi programmi gestione file...in modalità testo

    public class FileBasics
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Scrittura con cancellazione del file");
            scrivi(@"./tmp.txt", "testo da scrivere");

            Console.WriteLine("Lettura completa, contenuto del file:");
            leggi(@"./tmp.txt");

            Console.WriteLine("Scrittura con append");
            scriviAppend(@"./tmp.txt", "testo da scrivere");

            Console.WriteLine("Lettura completa, contenuto del file:");
            leggi(@"./tmp.txt");

            Console.WriteLine("Lettura completa, contenuto del file:");
            leggi(@"./tmp.txt");

            Console.WriteLine("Scrivo oggetto persona in JSON...");
            Persona p = new Persona();
            p.Surname = "Rossi";
            p.Name = "Mario";
            scriviOggettoPersona(@"./myObj.json",p);
            Console.WriteLine("e lo rileggo");
            p = leggiOggettoPersona(@"./myObj.json");

            //esempio lettura in accesso diretto con file con righe dimensioni fissa
            Console.WriteLine("Accesso diretto..accedo alla riga 3:");
            string riga=accessoDirettoRiga(@"./FILE-RECORD-FISSI.txt", 2, 31);
            Console.WriteLine("...ho letto:"+riga);
        }

        //scrittura
        public static void scrivi(string filename, string content)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(content);
            sw.Close();
        }

        //lettura
        public static void leggi(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            // Read and display lines from the file until the end of
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

            sr.Close();
        }

        public static void leggi2(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            // Read and display lines from the file until the end of
            // the file is reached.


            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                Console.WriteLine(line);
            }

            sr.Close();
        }

        //scrittura in append
        public static void scriviAppend(string filename, string content)
        {
            var oStream = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Read);
            StreamWriter sw = new StreamWriter(oStream);
            sw.WriteLine(content);
            sw.Close();
        }


        //leggere-scrviere oggetti in json
        // per altri esempi https://stackoverflow.com/questions/6115721/how-to-save-restore-serializable-object-to-from-file
        public static void scriviOggettoPersona(string filename, Persona p) { 

            string json = JsonConvert.SerializeObject(p);
            File.WriteAllText(filename, json);
        }

        public static Persona leggiOggettoPersona(string filename)
        {
            string json = File.ReadAllText(filename);
            Persona p = JsonConvert.DeserializeObject<Persona>(json);
            return p;
        }


        //gestione di file binari fixed lenght e accesso diretto
        public static string accessoDirettoRiga(string filename, int i, int bytesPerLine)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            stream.Seek(bytesPerLine * (i - 1), SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream);
            string line = reader.ReadLine();
            //BinaryReader rbin = new BinaryReader(stream);
            //string line = System.Text.Encoding.UTF8.GetString(rbin.ReadBytes(bytesPerLine));

            return line;
        }

    }

    public class Persona
    {
        private string surname;
        private string name;

        public string Surname { get => surname; set => surname = value; }
        public string Name { get => name; set => name = value; }
    }


}


