// See https://aka.ms/new-console-template for more information
using hw_12;

AppDBContext context = new AppDBContext();

context.Database.EnsureCreated();

var u = new List<User>()
{

    new User(){Name = "ali" , LastName = "Rezaee" , Age = 25},
    new User(){Name = "Reza" , LastName = "Heydari" , Age = 20},
    new User(){Name = "Parsa" , LastName = "Mousavi" , Age = 25},
    new User(){Name = "Akbar" , LastName = "BabaKhan" , Age = 25},
    new User(){Name = "Reza" , LastName = "Eshtiyagh" , Age = 25}

};
context.Users.AddRange(u);
context.SaveChanges();

Console.WriteLine("ok");


while (true)
{
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. Show Tasks");
    Console.WriteLine("3. Edit Task");
    Console.WriteLine("4. Delete Task");
    Console.WriteLine("5. Change Task Status");
    Console.WriteLine("6. Search Tasks");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");
    var option = Console.ReadLine();


    switch (option)
    {
        case "1":
            AddTask();
            break;
        case "2":
            ShowTasks();
            break;
        case "3":
            EditTask();
            break;
        case "4":
            DeleteTask();
            break;
        case "5":
            ChangeTaskStatus();
            break;
        case "6":
            SearchTasks();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}
       



static void AddTask()
{
    using (var context = new AppDBContext())
    {
        var task = new Work();

        Console.Write("Enter title: ");
        task.Name = Console.ReadLine();
        Console.Write("Enter description: ");
        task.Description = Console.ReadLine();
        Console.Write("Enter priority (1: High, 2: Medium, 3: Low): ");
        task.priority = Console.ReadLine();
        context.works.Add(task);
        context.SaveChanges();
    }
}
static void ShowTasks()
{
    using (var context = new AppDBContext())
    {
        var tasks = context.works.ToList();
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Name},  Priority: {task.priority}");
        }
    }
}
static void EditTask()
{
    using (var context = new AppDBContext())
    {
        Console.Write("Enter task ID to edit: ");
        var id = int.Parse(Console.ReadLine());
        var task = context.works.Find(id);

        if (task != null)
        {
            Console.Write("Enter new title (leave blank to keep current): ");
            var title = Console.ReadLine();
            if (!string.IsNullOrEmpty(title)) task.Name = title;

            Console.Write("Enter new description (leave blank to keep current): ");
            var description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description)) task.Description = description;



            Console.Write("Enter new priority (leave blank to keep current): ");
            var priorityInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(priorityInput)) task.priority = priorityInput;

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }
}
static void DeleteTask()
{
    using (var context = new AppDBContext())
    {
        Console.Write("Enter task ID to delete: ");
        var id = int.Parse(Console.ReadLine());
        var task = context.works.Find(id);

        if (task != null)
        {
            context.works.Remove(task);
            context.SaveChanges();
            Console.WriteLine("Task deleted.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }
}
static void ChangeTaskStatus()
{
    using (var context = new AppDBContext())
    {
        Console.Write("Enter task ID to change status: ");
        var id = int.Parse(Console.ReadLine());
        var task = context.works.Find(id);

        if (task != null)
        {
            Console.Write("Enter new status (In Progress, Completed, Cancelled): ");

            context.SaveChanges();
            Console.WriteLine("Task status updated.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }
}

static void SearchTasks()
{
    using (var context = new AppDBContext())
    {
        Console.Write("Enter title to search: ");
        var title = Console.ReadLine();
        var tasks = context.works.Where(t => t.Name.Contains(title)).ToList();

        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Name}, Priority: {task.priority}");
        }
    }
}



