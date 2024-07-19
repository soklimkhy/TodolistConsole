using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodolistConsole
{
    //Strategy Pattern
    public interface IExportFormatter
    {
        void ExportTasks(List<Task> tasks, string filePath);
    }
}
