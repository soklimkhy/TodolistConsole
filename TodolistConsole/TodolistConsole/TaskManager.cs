using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodolistConsole
{
    
    // Facade Class
    
    public class TaskManager
    {
        private readonly string _filePath;

        public TaskManager(string filePath)
        {
            _filePath = filePath;
        }

        public List<Task> GetTasks()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Task>();
            }

            return File.ReadAllLines(_filePath)
                .Select(line => new Task(line.Split('\t')[0]))
                .ToList();
        }

        public void AddTask(string description)
        {
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                writer.WriteLine(new Task(description));
            }
        }

        public void RemoveTask(int taskNumber)
        {
            List<Task> tasks = GetTasks();

            if (taskNumber > 0 && taskNumber <= tasks.Count)
            {
                tasks.RemoveAt(taskNumber - 1);
                File.WriteAllLines(_filePath, tasks.Select(t => t.ToString()).ToArray());
            }
        }

        public void EditTask(int taskNumber, string updatedDescription)
        {
            List<Task> tasks = GetTasks();

            if (taskNumber > 0 && taskNumber <= tasks.Count)
            {
                tasks[taskNumber - 1] = new Task(updatedDescription);
                File.WriteAllLines(_filePath, tasks.Select(t => t.ToString()).ToArray());
            }
        }

        public List<Task> SearchTasks(string searchTerm)
        {
            List<Task> tasks = GetTasks();
            return tasks.Where(t => t.Description.Contains(searchTerm)).ToList();
        }
    }

}
