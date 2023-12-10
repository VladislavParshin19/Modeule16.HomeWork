using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Modeule16.HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1) Просмотр содержимого директории");
                Console.WriteLine("2) Создание файла/директории");
                Console.WriteLine("3) Удаление файла/директории");
                Console.WriteLine("4) Копирование файла/директории");
                Console.WriteLine("5) Перемещение файла/директории");
                Console.WriteLine("6) Чтение из файла");
                Console.WriteLine("7) Запись в файл");
                Console.WriteLine("8) Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewDirectoryContents();
                        break;

                    case "2":
                        CreateFileOrDirectory();
                        break;

                    case "3":
                        DeleteFileOrDirectory();
                        break;

                    case "4":
                        CopyFileOrDirectory();
                        break;

                    case "5":
                        MoveFileOrDirectory();
                        break;

                    case "6":
                        ReadFromFile();
                        break;

                    case "7":
                        WriteToFile();
                        break;

                    case "8":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void ViewDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Список файлов:");
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine("\nСписок директорий:");
                foreach (var directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void CreateFileOrDirectory()
        {
            Console.Write("1. Создать файл\n2. Создать директорию\nВыберите действие: ");
            string choice = Console.ReadLine();

            Console.Write("Введите путь и имя файла/директории: ");
            string path = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    File.Create(path).Close();
                    Console.WriteLine($"Файл {path} создан успешно.");
                }
                else if (choice == "2")
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine($"Директория {path} создана успешно.");
                }
                else
                {
                    Console.WriteLine("Неверный выбор типа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void DeleteFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для удаления: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine($"Файл {path} удален успешно.");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine($"Директория {path} удалена успешно.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void CopyFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для копирования: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь к месту назначения: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)), true);
                    Console.WriteLine($"Файл {sourcePath} скопирован успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Директория {sourcePath} скопирована успешно.");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            string[] files = Directory.GetFiles(sourceDir);
            foreach (string file in files)
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            string[] dirs = Directory.GetDirectories(sourceDir);
            foreach (string dir in dirs)
            {
                string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destDir);
            }
        }

        private static void MoveFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для перемещения: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь к месту назначения: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Файл {sourcePath} перемещен успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Директория {sourcePath} перемещена успешно.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void ReadFromFile()
        {
            Console.Write("Введите путь к файлу для чтения: ");
            string filePath = Console.ReadLine();

            try
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"Содержимое файла {filePath}:\n{content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void WriteToFile()
        {
            Console.Write("Введите путь к файлу для записи: ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Введите текст для записи в файл (Ctrl+Z для завершения):");
            string content = Console.In.ReadToEnd();

            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Файл {filePath} успешно записан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
