using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using Xceed.Document.NET;
using Xceed.Words.NET;
using TodolistConsole;
// Strategy Pattern
public class Program
{
    static void Main(string[] args)
    {
        string filePath = "todo.txt";
        var taskManager = new TaskManager(filePath);

        // Use a default text formatter initially
        IExportFormatter exportFormatter = new TextExportFormatter();

        UI ui = new UI(taskManager, exportFormatter);
        ui.MainMenu();
    }
}

