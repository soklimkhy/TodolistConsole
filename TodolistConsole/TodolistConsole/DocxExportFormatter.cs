using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace TodolistConsole
{
    //Strategy Pattern
    public class DocxExportFormatter : IExportFormatter
    {
        public void ExportTasks(List<Task> tasks, string filePath)
        {
            using (DocX document = DocX.Create(filePath))
            {
                // Add a title
                document.InsertParagraph("Task List").FontSize(16).Bold().Alignment = Alignment.center;

                // Add a table for tasks
                Table table = document.AddTable(tasks.Count + 1, 2);
                table.Alignment = Alignment.center;
                table.Design = TableDesign.TableGrid;

                // Add headers
                table.Rows[0].Cells[0].Paragraphs[0].Append("Task Number").Bold();
                table.Rows[0].Cells[1].Paragraphs[0].Append("Task").Bold();

                // Populate the table with task data
                for (int i = 0; i < tasks.Count; i++)
                {
                    table.Rows[i + 1].Cells[0].Paragraphs[0].Append((i + 1).ToString());
                    table.Rows[i + 1].Cells[1].Paragraphs[0].Append(tasks[i].Description);
                }

                document.InsertTable(table);
                document.Save();
            }
        }
    }

}
