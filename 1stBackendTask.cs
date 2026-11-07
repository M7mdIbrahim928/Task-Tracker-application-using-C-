
using System;
using System.Collections.Generic;
using System.Linq;

enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

enum Priority
{
    Low,
    Medium,
    High
}

class TaskItem
{
    public int Id;
    public string Title;
    public string Description;
    public DateTime DueDate;
    public Priority Priority;
    public TaskStatus Status;

    public TaskItem(int id, string title, string description,
                    DateTime dueDate, Priority priority)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = TaskStatus.Pending;
    }
}

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();
    static int nextId = 1;

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== TASK TRACKER =====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Update Task Status");
            Console.WriteLine("3. View Tasks");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    UpdateTask();
                    break;

                case "3":
                    ViewTasks();
                    break;

                case "4":
                    DeleteTask();
                    break;

                case "5":
                    Console.WriteLine("Exiting Task Tracker...");
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    // Add a new task
    static void AddTask()
    {
        Console.Write("Enter Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Due Date (yyyy-mm-dd): ");
        DateTime dueDate;

        while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
        {
            Console.Write("Invalid date. Enter again: ");
        }

        Console.WriteLine("Priority: 1.Low  2.Medium  3.High");
        Console.Write("Choose Priority: ");

        int p;
        while (!int.TryParse(Console.ReadLine(), out p) || p < 1 || p > 3)
        {
            Console.Write("Enter 1, 2, or 3: ");
        }

        TaskItem task = new TaskItem(
            nextId++,
            title,
            description,
            dueDate,
            (Priority)(p - 1)
        );

        tasks.Add(task);

        Console.WriteLine("Task added successfully!");
    }

    // Update task status
    static void UpdateTask()
    {
        Console.Write("Enter Task ID: ");
        int id;

        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        TaskItem task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }

        Console.WriteLine("1. Pending");
        Console.WriteLine("2. In Progress");
        Console.WriteLine("3. Completed");
        Console.Write("Choose new status: ");

        int status;

        if (!int.TryParse(Console.ReadLine(), out status) ||
            status < 1 || status > 3)
        {
            Console.WriteLine("Invalid status!");
            return;
        }

        task.Status = (TaskStatus)(status - 1);

        Console.WriteLine("Task updated successfully!");
    }

    // View tasks by category
    static void ViewTasks()
    {
        Console.WriteLine("\n1. Active Tasks");
        Console.WriteLine("2. Overdue Tasks");
        Console.WriteLine("3. Completed Tasks");
        Console.Write("Choose category: ");

        string choice = Console.ReadLine();

        IEnumerable<TaskItem> result;

        if (choice == "1")
        {
            result = tasks.Where(t =>
                t.Status != TaskStatus.Completed &&
                t.DueDate.Date >= DateTime.Today);
        }
        else if (choice == "2")
        {
            result = tasks.Where(t =>
                t.Status != TaskStatus.Completed &&
                t.DueDate.Date < DateTime.Today);
        }
        else if (choice == "3")
        {
            result = tasks.Where(t =>
                t.Status == TaskStatus.Completed);
        }
        else
        {
            Console.WriteLine("Invalid category!");
            return;
        }

        if (!result.Any())
        {
            Console.WriteLine("No tasks found!");
            return;
        }

        foreach (TaskItem t in result)
        {
            Console.WriteLine("\n--------------------");
            Console.WriteLine("ID: " + t.Id);
            Console.WriteLine("Title: " + t.Title);
            Console.WriteLine("Description: " + t.Description);
            Console.WriteLine("Due Date: " + t.DueDate.ToShortDateString());
            Console.WriteLine("Priority: " + t.Priority);
            Console.WriteLine("Status: " + t.Status);
        }
    }

    // Delete a task
    static void DeleteTask()
    {
        Console.Write("Enter Task ID to delete: ");
        int id;

        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        TaskItem task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }

        tasks.Remove(task);

        Console.WriteLine("Task deleted successfully!");
    }
}
