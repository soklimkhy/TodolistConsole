using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodolistConsole
{
    //Strategy Pattern
    public class TextExportFormatter : IExportFormatter
    {
        public void ExportTasks(List<Task> tasks, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine(task.ToString());
                }
            }
        }
    }
}
