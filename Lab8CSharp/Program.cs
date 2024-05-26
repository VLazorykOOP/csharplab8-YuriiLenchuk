
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        while (true)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Оберіть завдання:");
            Console.WriteLine("1. Пошук com посилань");
            Console.WriteLine("2. Видалення українських слів, що починаються на голосну літеру");
            Console.WriteLine("3. Видалення всіх входжень першої букви слова");
            Console.WriteLine("4. Виведення додатних чисел послідовності");
            Console.WriteLine("5. Робота з папками");
            Console.WriteLine("6. Вихід");
            Console.Write("Введіть номер завдання: ");
            string choice = Console.ReadLine() ?? "7";

            switch (choice)
            {
                case "1":
                    Task1();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "2":
                    Task2();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "3":
                    Task3();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "4":
                    Task4();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "5":
                    task5();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "6":
                    return;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
            }
        }
    }

    static void Task1()
    {
        string inputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\input.txt";
        string outputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\output.txt";

        string content = File.ReadAllText(inputFilePath);
        string pattern = @"\bhttps?://[a-zA-Z0-9-]+\.com\b";

        // знаходження edu.ua URLs в тексті
        MatchCollection matches = Regex.Matches(content, pattern);

        using (StreamWriter writer = new StreamWriter(outputFilePath, false))
        {
            writer.WriteLine($"Знайдено {matches.Count} адрес .com:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
                writer.WriteLine(match.Value);
            }
        }

        Console.WriteLine("\nВведіть посилання, які ви хочете вилучити (розділіть їх комами):");
        string[] linksToRemove = Console.ReadLine().Split(',');

        Console.WriteLine("\nВведіть посилання, на які ви хочете замінити (розділіть їх комами, в тому ж порядку):");
        string[] linksToReplace = Console.ReadLine().Split(',');


        // Заміна URL
        for (int i = 0; i < linksToRemove.Length; i++)
        {
            content = content.Replace(linksToRemove[i].Trim(), linksToReplace[i].Trim());
        }

        File.WriteAllText(outputFilePath, content);

        Console.WriteLine("\nГотово! Результати збережено в output.txt");
    }

    static void Task2()
    {
        string inputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\input.txt";
        string outputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\output.txt";

        // Зчитування тексту з файлу
        string text = File.ReadAllText(inputFilePath);

        // Регулярний вираз для знаходження українських слів, які починаються на голосну літеру
        string pattern = @"\b[аеиоуюяєїіАЕИОУЮЯЄЇІ][\w']*";

        // Видалення знайдених слів
        string result = Regex.Replace(text, pattern, "");

        File.WriteAllText(outputFilePath, result);

        Console.WriteLine("Готово!");

    }

    static void Task3()
    {
        string inputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\input.txt";
        string outputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\output.txt";

        string[] lines = File.ReadAllLines(inputFilePath);
        string[] results = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            results[i] = RemoveSubsequentOccurrences(lines[i]);
        }

        // Запис результату у новий файл
        File.WriteAllLines(outputFilePath, results);

        Console.WriteLine("Результат збережено у файлі output.txt");
    }

    static string RemoveSubsequentOccurrences(string text)
    {
        // Розділення рядка на слова та розділові знаки за допомогою регулярних виразів
        string[] parts = Regex.Split(text, @"(\W+)");


        string result = "";

        foreach (string part in parts)
        {
            // Якщо частина не порожня
            if (!string.IsNullOrEmpty(part))
            {
                // Якщо частина є словом
                if (Regex.IsMatch(part, @"\w+"))
                {
                    // Вилучення наступних входжень першої літери у слові
                    char firstChar = part[0];
                    string newWord = firstChar + Regex.Replace(part.Substring(1), $"{firstChar}", "");
                    result += newWord;
                }
                else
                {
                    // Додавання розділового знаку до результату
                    result += part;
                }
            }
        }

        return result.TrimEnd();
    }


    static void Task4()
    {

        string outputFilePath = "D:\\c#\\csharplab8-YuriiLenchuk\\Lab8CSharp\\output.txt";

        // Приклад вхідних даних
        int n = 10;
        int[] numbers = { -1, 5, 10, -15, 20, 25, -30, 35, 40, -45 };

        // Створення і заповнення списку додатніх чисел
        List<int> positiveNumbers = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (numbers[i] > 0)
            {
                positiveNumbers.Add(numbers[i]);
            }
        }

        // Запис вмісту списку додатніх чисел в файл
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            foreach (int number in positiveNumbers)
            {
                writer.WriteLine(number);
            }
        }

        // Виведення вмісту файлу на екран
        Console.WriteLine("Вміст файлу 'output.txt':");
        string[] lines = File.ReadAllLines(outputFilePath);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
    }


    static void task5()
    {
        Console.WriteLine("Task 5\n");

        string studentName = "Lenchuk";
        string folder1Path = $"D:\\temp\\{studentName}1";
        string folder2Path = $"D:\\temp\\{studentName}2";
        string allFolderPath = $"D:\\temp\\ALL";

        // Task1
        Directory.CreateDirectory(folder1Path);
        Directory.CreateDirectory(folder2Path);

        // Task2
        string t1FilePath = Path.Combine(folder1Path, "t1.txt");
        string t2FilePath = Path.Combine(folder1Path, "t2.txt");

        string t1Text = "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми";
        string t2Text = "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ";

        File.WriteAllText(t1FilePath, t1Text);
        File.WriteAllText(t2FilePath, t2Text);

        // Task3
        string t3FilePath = Path.Combine(folder2Path, "t3.txt");
        File.WriteAllText(t3FilePath, File.ReadAllText(t1FilePath) + "\n" + File.ReadAllText(t2FilePath));

        // Task4
        PrintFileInfo(t1FilePath);
        PrintFileInfo(t2FilePath);
        PrintFileInfo(t3FilePath);

        // Task5
        string moveT2FilePath = Path.Combine(folder2Path, "t2.txt");
        if (File.Exists(moveT2FilePath))
        {
            File.Delete(moveT2FilePath);
        }
        File.Move(t2FilePath, moveT2FilePath);

        // Task6
        string copyT1FilePath = Path.Combine(folder2Path, "t1.txt");
        File.Copy(t1FilePath, copyT1FilePath);

        // Task7
        if (Directory.Exists(allFolderPath))
        {
            Directory.Delete(allFolderPath, true);
        }
        Directory.Move(folder1Path, allFolderPath);


        // Task8
        Console.WriteLine("\nFiles in All directory:");
        string[] filesInAll = Directory.GetFiles(allFolderPath);
        foreach (string file in filesInAll)
        {
            PrintFileInfo(file);
        }
    }
    static void PrintFileInfo(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        Console.WriteLine($"File Name: {fileInfo.Name}");
        Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
        Console.WriteLine($"Size (bytes): {fileInfo.Length}");
        Console.WriteLine($"Created: {fileInfo.CreationTime}");
        Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
        Console.WriteLine();
    }

}