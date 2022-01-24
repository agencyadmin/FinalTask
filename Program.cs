namespace FinalTask
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
    public class Student // Описываем  класс  и помечаем его атрибутом для последующей сериализации или десериализации
    {
        public string Name { get; set; }
        // после замены типов на float, int, bool выдало ошибку десериализации : System.Runtime.Serialization.SerializationException:
        // 'Unable to find assembly 'FinalTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.'
        // вернул все в стринг типы и дата тайм
        public string Group { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;

        }
    }


    class Program
    {
        public static void Main(string[] args)

        {
            Student[] newStudent = null;

            newStudent = ReadDeSerValues(@"C:\Users\wmtra\Desktop\TestDir\Students.dat");
            //ReadValues(@"C:\Users\wmtra\Desktop\TestDir\Students.dat");
            //ReadTXTValues(@"C:\Users\wmtra\Desktop\TestDir\Students.dat");
            WriteValues(@"C:\Users\wmtra\Desktop\TestDir\Students", newStudent);
        }

        public static void ReadValues(string urlDataFile)
        {
            string StringValue;
            float FloatValue;
            int IntValue;
            bool BooleanValue;


            if (File.Exists(urlDataFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(urlDataFile, FileMode.Open)))
                {
                    StringValue = reader.ReadString();
                    FloatValue = reader.ReadSingle();
                    IntValue = reader.ReadInt32();
                    BooleanValue = reader.ReadBoolean();

                }
                Console.WriteLine("Строка: " + StringValue);
                Console.WriteLine("Дробь: " + FloatValue);
                Console.WriteLine("Целое: " + IntValue);
                Console.WriteLine("Булево значение " + BooleanValue);
            }
        }

        public static void ReadTXTValues(string urlDataFile)
        {
            if (File.Exists(urlDataFile)) // Проверим, существует ли файл по данному пути
                                          //{
                                          //    
                                          //    using (StreamWriter sw = File.CreateText(filePath)) 
                                          //        sw.WriteLine("Олег");
                                          //        sw.WriteLine("Дмитрий");
                                          //        sw.WriteLine("Иван");
                                          //    }
                                          //}
                                          // Откроем файл и прочитаем его содержимое
                using (StreamReader sr = File.OpenText(urlDataFile))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной и выводим в консоль
                    {
                        Console.WriteLine(str);
                        Console.WriteLine("____________________________________________________________________________________________");
                    }
                }
        }

        public static Student[] ReadDeSerValues(string urlDataFile)
        {
            //эта идея с прямой десериализацией файла не прошла , выдает ошибку System.Runtime.Serialization.SerializationException: 'The input stream is not a valid binary format. The starting contents (in bytes) are: 
            BinaryFormatter formatter = new BinaryFormatter();
            // Student Student1 = new ( "Ivan", "3", DateTime.Now );

            // using (var fs = new FileStream(@"C:\Users\wmtra\Desktop\TestDir\Group1.dat", FileMode.OpenOrCreate)) // получаем поток, куда будем записывать сериализованный объект
            // {
            //     formatter.Serialize(fs, Student1 );
            //    Console.WriteLine("Объект сериализован");
            //}
            // десериализация
            using (var fs = new FileStream(@"C:\Users\wmtra\Desktop\TestDir\Students.dat", FileMode.Open, FileAccess.Read))
            {
                Student[] newStudent = null;

                newStudent = (Student[])formatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован:");
                foreach (Student student in newStudent)
                    Console.WriteLine($"Имя: {student.Name} --- Группа: {student.Group} +++++++++++ Дата рождения: {student.DateOfBirth} --- ");
                return newStudent;
            }
        }
        public static void WriteValues(string urlDir, Student[] newStudent)
        {
            if (!Directory.Exists(urlDir)) Directory.CreateDirectory(urlDir);

            foreach (Student a in newStudent)
            {
                if (!File.Exists(@"C:\Users\wmtra\Desktop\TestDir\Students\" + a.Group + ".txt"))
                {


                    using (StreamWriter sw = File.CreateText(@"C:\Users\wmtra\Desktop\TestDir\Students\" + a.Group + ".txt"))
                    {
                        sw.WriteLine($"Имя студента: {a.Name}, Дата рождения студента: {a.Name}");
                    }
                }

                else
                {
                    using (StreamWriter sw = File.AppendText(@"C:\Users\wmtra\Desktop\TestDir\Students\" + a.Group + ".txt"))
                    {
                        sw.WriteLine($"Имя студента: {a.Name}, Дата рождения студента: {a.DateOfBirth}");

                    }
                }

            }
        }
    }
}