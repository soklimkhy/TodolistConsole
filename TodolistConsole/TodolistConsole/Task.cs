using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//OOP principles for creating reusable and maintainable code 
public class Task
{
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public Task(string description)
    {
        Description = description;
        CreatedDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Description}\t| {CreatedDate:yyyy-MM-dd HH:mm:ss}";
    }
}


