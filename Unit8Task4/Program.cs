using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask

{
    class Program
    {
        static void Main(string[] args)
        {
            string LsFile, LsNewDir, lsNameFile;

            do
            {
                Console.WriteLine("Укажите файл с данными:");
                LsFile = Console.ReadLine();
            }
            while (LsFile.Length < 8);

            LsNewDir = LsNewDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ Students/";

            if (Directory.Exists(LsNewDir))
            {
                DirectoryInfo Di = new DirectoryInfo(LsNewDir);
                Di.Delete(true);
            }

            Directory.CreateDirectory(LsNewDir);

            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream(LsFile, FileMode.Open))
            {
                Student[] students = (Student[])formatter.Deserialize(fs);
              
                foreach (Student student in students)
                { 
                    

                    lsNameFile = LsNewDir + student.Group + ".txt";
                    var FileInfo = new FileInfo(lsNameFile);
                    if (!File.Exists(lsNameFile))
                    {
                        using (StreamWriter sw = FileInfo.CreateText())
                        {
                            sw.WriteLine($"Имя: {student.Name} ---- Дата Рождения: {student.DateOfBirth}");
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = FileInfo.AppendText())
                        {
                            sw.WriteLine($"Имя: {student.Name} ---- Дата Рождения: {student.DateOfBirth}");
                        }
                    }
                }
            }
  
                Console.ReadLine();
        }
       
    }

   
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

       
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
}
