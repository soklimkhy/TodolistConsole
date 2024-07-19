using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;


namespace TodolistConsole
{
    //Facade pattern
    public class UI
    {
        private readonly TaskManager _taskManager;
        private readonly IExportFormatter _exportFormatter;
        static string filePath = "todo.txt";
        public UI(TaskManager taskManager, IExportFormatter exportFormatter)
        {
            _taskManager = taskManager;
            _exportFormatter = exportFormatter;
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("+--------------------+");
                Console.WriteLine("| Todolist Console |");
                Console.WriteLine("+--------------------+");

                Console.WriteLine("1. View Tasks");
                Console.WriteLine("2. Add task");
                Console.WriteLine("3. Remove task");
                Console.WriteLine("4. Edit Task");
                Console.WriteLine("5. Search Task");
                Console.WriteLine("6. Export File");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewTask();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        RemoveTask();
                        break;
                    case "4":
                        EditTask();
                        break;
                    case "5":
                        SearchTask();
                        break;
                    case "6":
                        ExportFiles();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                        break;
                }
            }
        }
        static void ViewTasks()
        {
            Console.WriteLine("| Tasks: ");

            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);

                for (int i = 0; i < tasks.Length; i++)
                {
                    Console.WriteLine($"| {i + 1}. {tasks[i]}");
                }
            }
            else
            {
                Console.WriteLine("No tasks available.");
            }

            Console.WriteLine();
        }

        static void AddTask()
        {
            Console.Write("\nEnter the task: ");
            string task = Console.ReadLine();

            // Add the current date and time to the task
            string timestampedTask = $"{task}\t| {DateTime.Now:yyyy-MM-dd HH:mm:ss} ";

            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(timestampedTask);
            }

            Console.WriteLine("Task added successfully.\n");
        }

        static void RemoveTask()
        {
            ViewTasks();

            Console.Write("Enter the task number to remove: ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber))
            {
                List<string> tasks = new List<string>(File.ReadAllLines(filePath));

                if (taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    string removedTask = tasks[taskNumber - 1];
                    tasks.RemoveAt(taskNumber - 1);

                    File.WriteAllLines(filePath, tasks.ToArray());

                    Console.WriteLine($"Task '{removedTask}' removed successfully.\n");
                }
                else
                {
                    Console.WriteLine("Invalid task number.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }
        }
        static void EditTask()
        {
            ViewTasks();

            Console.Write("Enter the task number to edit: ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber))
            {
                List<string> tasks = new List<string>(File.ReadAllLines(filePath));

                if (taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    Console.Write("Enter the updated task: ");
                    string updatedTask = Console.ReadLine();

                    // Add the current date and time to the updated task
                    string timestampedTask = $"{updatedTask}\t| {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                    tasks[taskNumber - 1] = timestampedTask;
                    File.WriteAllLines(filePath, tasks.ToArray());

                    Console.WriteLine($"Task updated successfully.\n");
                }
                else
                {
                    Console.WriteLine("Invalid task number.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }
        }
        static void SearchTask()
        {
            Console.WriteLine("Search tasks by:");
            Console.WriteLine("1. Task number");
            Console.WriteLine("2. Alphabetical order");

            Console.Write("Enter your choice (1-4): ");
            string searchChoice = Console.ReadLine();

            List<string> tasks = new List<string>(File.ReadAllLines(filePath));

            switch (searchChoice)
            {
                case "1":
                    Console.Write("Enter the task number: ");
                    if (int.TryParse(Console.ReadLine(), out int taskNumber))
                    {
                        if (taskNumber > 0 && taskNumber <= tasks.Count)
                        {
                            Console.WriteLine($"------------------------------------------");
                            Console.WriteLine($"| Task {taskNumber}: {tasks[taskNumber - 1]}");
                            Console.WriteLine($"------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Invalid task number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                    break;



                case "2":
                    Console.WriteLine("Tasks in alphabetical order:");
                    tasks.Sort();
                    foreach (var task in tasks)
                    {
                        Console.WriteLine(task);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 2.");
                    break;
            }
        }

        static void ViewTask()
        {
            Console.WriteLine("Choose a view for displaying tasks:");
            Console.WriteLine("1. Table view");
            Console.WriteLine("2. Card view");
            Console.WriteLine("3. List view");

            Console.Write("Enter your choice (1-3): ");
            string viewChoice = Console.ReadLine();

            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);

                switch (viewChoice)
                {
                    case "1":
                        Console.WriteLine("Table view:");
                        Console.WriteLine("+-------------------------------+---------------------+");
                        Console.WriteLine("| Task Number | Task\t\t|  \tDate\t      | ");
                        Console.WriteLine("+-------------------------------+---------------------+");
                        for (int i = 0; i < tasks.Length; i++)
                        {
                            Console.WriteLine($"| {i + 1,11} | {tasks[i],-20}| ");
                        }
                        Console.WriteLine("+-------------------------------+---------------------+");
                        break;

                    case "2":
                        Console.WriteLine("Card view:");
                        for (int i = 0; i < tasks.Length; i++)
                        {
                            Console.WriteLine("+---------------------------------------------+");
                            Console.WriteLine($"|                                             |");
                            Console.WriteLine($"| Task{i + 1,2}: {tasks[i],-15}|");
                            Console.WriteLine($"|                                             |");
                            Console.WriteLine("+---------------------------------------------+");
                        }
                        break;



                    case "3":
                        Console.WriteLine("List view:");
                        for (int i = 0; i < tasks.Length; i++)
                        {
                            Console.WriteLine($"| {i + 1}. {tasks[i]} |");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No tasks available.");
            }

            Console.WriteLine();
        }
        static void ExportFiles()
        {
            Console.WriteLine("Choose an export format:");
            Console.WriteLine("1. TXT");
            Console.WriteLine("2. DOCX");
            Console.WriteLine("3. PDF");
            Console.WriteLine("4. CSV");
            Console.WriteLine("5. XLSX");

            Console.Write("Enter your choice (1-5): ");
            string exportChoice = Console.ReadLine();

            switch (exportChoice)
            {
                case "1":
                    // Export as TXT
                    ExportToTxt();
                    break;

                case "2":
                    // Export as DOCX
                    ExportToDocx();
                    break;

                case "3":
                    // Export as PDF
                    ExportToPdf();
                    break;

                case "4":
                    // Export as CSV
                    ExportToCsv();
                    break;

                case "5":
                    // Export as XLSX
                    ExportToXlsx();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
        static void ExportToTxt()
        {
            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);
                string txtFilePath = "tasks_export.txt";

                using (StreamWriter writer = new StreamWriter(txtFilePath))
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine(task);
                    }
                }

                Console.WriteLine($"Tasks exported to {txtFilePath} successfully.");
            }
            else
            {
                Console.WriteLine("No tasks available to export.");
            }
        }
        static void ExportToDocx()
        {
            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);
                string docxFilePath = "tasks_export.docx";

                using (DocX document = DocX.Create(docxFilePath))
                {
                    // Add a title
                    document.InsertParagraph("Task List").FontSize(16).Bold().Alignment = Alignment.center;

                    // Add a table for tasks
                    Table table = document.AddTable(tasks.Length + 1, 2);
                    table.Alignment = Alignment.center;
                    table.Design = TableDesign.TableGrid;

                    // Add headers
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Task Number").Bold();
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Task").Bold();

                    // Populate the table with task data
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append((i + 1).ToString());
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(tasks[i]);
                    }

                    document.InsertTable(table);
                    document.Save();
                }

                Console.WriteLine($"Tasks exported to {docxFilePath} successfully.");
            }
            else
            {
                Console.WriteLine("No tasks available to export.");
            }
        }
        static void ExportToPdf()
        {
            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);
                string pdfFilePath = "tasks_export.pdf";

                using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create))
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20f, 20f, 20f, 20f);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.Open();

                    // Add a title
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Task List", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Add task data
                    foreach (var task in tasks)
                    {
                        iTextSharp.text.Paragraph taskParagraph = new iTextSharp.text.Paragraph(task);
                        document.Add(taskParagraph);
                    }

                    document.Close();
                }

                Console.WriteLine($"Tasks exported to {pdfFilePath} successfully.");
            }
            else
            {
                Console.WriteLine("No tasks available to export.");
            }
        }
        static void ExportToCsv()
        {
            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);
                string csvFilePath = "tasks_export.csv";

                using (StreamWriter writer = new StreamWriter(csvFilePath))
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine(task);
                    }
                }

                Console.WriteLine($"Tasks exported to {csvFilePath} successfully.");
            }
            else
            {
                Console.WriteLine("No tasks available to export.");
            }
        }
        static void ExportToXlsx()
        {
            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);
                string xlsxFilePath = "tasks_export.xlsx";

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Tasks");

                    for (int i = 0; i < tasks.Length; i++)
                    {
                        worksheet.Cells[i + 1, 1].Value = tasks[i];
                    }

                    package.SaveAs(new FileInfo(xlsxFilePath));
                }

                Console.WriteLine($"Tasks exported to {xlsxFilePath} successfully.");
            }
            else
            {
                Console.WriteLine("No tasks available to export.");
            }
        }
    }
}
