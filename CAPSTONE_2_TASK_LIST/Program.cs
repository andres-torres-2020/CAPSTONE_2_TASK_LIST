/*
 * A.TORRES
 * 
 * C#.NET CAPSTONE: TASK LIST
 * 
 * Manage tasks through a menu system
 * 
 * What will the application do?
 *  Present a menu to the user and ask them to choose:
 *  1. List tasks
 *  2. Add task
 *  3. Delete task
 *  4. Mark task complete
 *  5. Quit
 *  
 *  If the user chooses list tasks:
 *      Display all tasks.
 *      Format the task so output is tabbed--it may be easiest to show the description last.
 *      Show a task number, but start the task numbering with 1 not 0.
 *      
 *  If the user chooses to add task:
 *      Prompt the user to input each piece of data (team member’s name,
 *      task description, due date. (Tasks will always start incomplete-
 *      -that is, completion status is false.)
 *      Instantiate a new Task with this info, then add it at the end of your List.
 *      
 *  If the user chooses to delete task:
 *      Ask the user which task number. Remember, they’ll be using 1 through the size of the list,
 *      not 0 through size - 1, so shift their input accordingly.
 *      Validate the number entered--make sure it’s in range. Prompt them until
 *      they enter a number in range.
 *      Display the task the user chose. Ask if they’re sure they want to delete.
 *      If they answer Y, remove that item from your list and return to the main
 *          menu. If they answer N, return to the main menu.
 *          
 *  If the user chooses mark task complete:
 *      Ask the user which task number. Remember, they’ll be using 1 through the
 *      size of the list, not 0 through size - 1, so shift their input accordingly.
 *      Validate the number entered--make sure it’s in range. Prompt them until
 *      they enter a number in range.
 *      Display the task the user chose. Ask if they’re sure they want to mark the task as complete.
 *      If they answer Y, change the completion status within that item to true and
 *      return to the main menu. If they answer N, return to the main menu.
 *      Display the main menu options every time they return to the main menu.
 *      
 *  Allow the user to display tasks for only one team member.
 *  
 *  Allow the user to display tasks with a due date before a date they choose.
 *  
 *  Allow the user to edit a task that has already been entered.
 */
using System;
using System.Collections.Generic;

namespace CAPSTONE_2_TASK_LIST
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTaskListApp();
        }
        public static void RunTaskListApp()
        {
            List<Task> tasks = LoadPrevTasks();
            bool keepTasking = true;

            Console.WriteLine("Welcome to the Task Manager!\n");
            while (keepTasking)
            {
                ShowMenu();
                int choice = InputUtil.ReadInteger("What would you like to do? ", 1, 8 /*max # menu choices*/);
                switch (choice)
                {
                    case 1: ListTasks(tasks); break;
                    case 2: ListTeamMemberTasks(tasks); break;
                    case 3: AddTask(tasks); break;
                    case 4: DeleteTask(tasks); break;
                    case 5: MarkTaskComplete(tasks); break;
                    case 6: ListTasksPriorToDueDate(tasks); break;
                    case 7: UpdateTask(tasks); break;
                    case 8: keepTasking = false; break;
                }
            }
            Console.WriteLine("Have a great day!");
        }
        public static void ShowMenu()
        {
            Console.WriteLine("\t\t 1. List tasks");
            Console.WriteLine("\t\t 2. List team member tasks");
            Console.WriteLine("\t\t 3. Add task");
            Console.WriteLine("\t\t 4. Delete task");
            Console.WriteLine("\t\t 5. Mark task complete");
            Console.WriteLine("\t\t 6. Show all tasks before a date");
            Console.WriteLine("\t\t 7. Update task");
            Console.WriteLine("\t\t 8. Quit!\n");
        }
        public static List<Task> LoadPrevTasks()
        {
            return new List<Task>
                {
                    new Task("Andy", "Capstone # 2", DateTime.Parse("4-29-2020"), true),
                    new Task("Michele", "Quality time!", DateTime.Now, false),
                    new Task("Andy", "OOP lesson", DateTime.Parse("4-29-2020"), false),
                    new Task("Bugs", "OOP lesson", DateTime.Parse("5-1-2020"), false)
                };
        }
        public static string TaskStatus(bool completed)
        {
            return completed ? "DONE" : "INC";
        }
        public static void DisplayTask(Task t, int taskNo)
        {
            Console.WriteLine($"{taskNo,-3}\t{TaskStatus(t.CompletedStatus)}\t{t.DueDate:d}\t{t.Name,-10}\t{t.Description}");
        }
        public static void DisplayTeamMemberTask(Task t, int taskNo)
        {
            Console.WriteLine($"{taskNo,-3}\t{TaskStatus(t.CompletedStatus)}\t{t.DueDate:d}\t{t.Description}");
        }
        public static void DisplayAllTasks(List<Task> tasks)
        {
            Console.WriteLine("#\tStatus\tDue Date\t{0,-15}\tDescription", "Person");
            int itemNo = 1;
            foreach (Task t in tasks)
            {
                DisplayTask(t, itemNo);
                //Console.WriteLine($"{itemNo,-3}\t{TaskStatus(t.CompletedStatus)}\t{t.DueDate:d}\t{t.Name,-10}\t{t.Description}");
                itemNo++;
            }
            Console.WriteLine("");
        }
        public static void ListTasks(List<Task> tasks)
        {
            /*
             *  If the user chooses list tasks:
             *      Display all tasks.
             *      Format the task so output is tabbed--it may be easiest to show the description last.
             *      Show a task number, but start the task numbering with 1 not 0.
             */
            Console.WriteLine("LIST TASKS:\n");
            if (tasks.Count > 0)
            {
                DisplayAllTasks(tasks);
            }
            else
            {
                Console.WriteLine("No task created.\n");
            }
        }
        public static void ListTeamMemberTasks(List<Task> tasks)
        {
            /*
             *  If the user chooses list tasks:
             *      Display all SELECTED MEMBER'S tasks.
             *      Format the task so output is tabbed--it may be easiest to show the description last.
             *      Show a task number, but start the task numbering with 1 not 0.
             */
            Console.WriteLine($"SHOW MEMBER TASKS:\n");
            string memberName = InputUtil.GetInputString("Which team member? ");
            List<string> members = GetTeamMembers(tasks);
            try
            {
                if (members.Count <= 0)
                {
                    throw new Exception("There are no people with tasks.\n");
                }
                List<Task> teamMemberTasks = GetMemberTasks(memberName, tasks);
                if (teamMemberTasks.Count > 0)
                {
                    Console.WriteLine($"TASKS FOR {memberName} :\n");
                    Console.WriteLine("#\tStatus\tDue Date\tDescription", "Person");
                    int itemNo = 1;
                    foreach (Task t in teamMemberTasks)
                    {
                        DisplayTeamMemberTask(t, itemNo);
                        itemNo++;
                    }
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("This person has no tasks.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void ListTasksPriorToDueDate(List<Task> tasks)
        {
            // display tasks with a due date before a date they choose.
            Console.WriteLine($"SHOW ALL TASKS BEFORE DATE:\n");
            DateTime userEndDate = InputUtil.GetInputDate("Show Tasks before what end date? (dd-mm-yy) ");

            Dictionary<int, Task> tasksFound = new Dictionary<int, Task>();
            int taskCounter = 1;
            foreach (Task t in tasks)
            {
                if (t.DueDate < userEndDate)
                {
                    tasksFound.Add(taskCounter, t);
                }
                taskCounter++;
            }
            if (tasksFound.Count == 0)
            {
                Console.WriteLine("No task exists prior to date\n");
            }
            else
            {
                foreach (int taskNumber in tasksFound.Keys)
                {
                    DisplayTask(tasksFound[taskNumber], taskNumber);
                }
            }
        }
        public static void AddTask(List<Task> tasks)
        {
            /*
             *  If the user chooses to add task:
             *      Prompt the user to input each piece of data (team member’s name,
             *      task description, due date. (Tasks will always start incomplete-
             *      -that is, completion status is false.)
             *      Instantiate a new Task with this info, then add it at the end of your List.
             */
            Console.WriteLine("ADD TASK:\n");
            Task newTask = new Task();
            newTask.Name = InputUtil.GetInputString("Team Member Name:");
            newTask.Description = InputUtil.GetInputString("Description:");
            newTask.DueDate = InputUtil.GetInputDate("Due date (dd/mm/yyyy) :");
            tasks.Add(newTask);
        }
        public static void DeleteTask(List<Task> tasks)
        {
            /*
             * If the user chooses to delete task:
             *      Ask the user which task number. Remember, they’ll be using 1 through the size of the list,
             *      not 0 through size - 1, so shift their input accordingly.
             *      Validate the number entered--make sure it’s in range. Prompt them until
             *      they enter a number in range.
             *      Display the task the user chose. Ask if they’re sure they want to delete.
             *      If they answer Y, remove that item from your list and return to the main
             *          menu. If they answer N, return to the main menu.
             */
            Console.WriteLine("DELETE TASK:\n");
            if (tasks.Count <= 0)
            {
                Console.WriteLine("No tasks to delete!\n");
                return;
            }

            int count = tasks.Count;
            int taskIndex = InputUtil.ReadInteger($"Which task # do you want to delete? (1 - {count}) : ", 1, count /*max # tasks*/);

            DisplayTask(tasks[taskIndex - 1], taskIndex);

            string confirm;
            do
            {
                confirm = InputUtil.ReadString("Are you sure you want to delete this task? (y/n)").ToLower();
            } while (confirm != "y" && confirm != "n");

            if (confirm == "y")
            {
                DisplayTask(tasks[taskIndex - 1], taskIndex);
                tasks.RemoveAt(taskIndex - 1);
                Console.WriteLine("Task deleted!\n");
            }
        }
        public static void EditTask(Task taskToEdit)
        {
            // edit team member name
            taskToEdit.Name = InputUtil.GetInputString(
                $"Current team member name: {taskToEdit.Name}\nNew team member name: ");

            // edit description
            taskToEdit.Description = InputUtil.GetInputString(
                $"Current description: {taskToEdit.Description}\nNew description: ");

            // edit due date
            taskToEdit.DueDate = InputUtil.GetInputDate(
                $"Current due date: {taskToEdit.DueDate:d}\nNew due date: ");

            // edit completion status
            string[] acceptableTrueValues = { "d", "done", "complete", "c", "com" };
            string[] acceptableFalseValues = { "incomplete", "i", "inc" };
            taskToEdit.CompletedStatus = InputUtil.GetInputBool(
                $"Current completed status: {TaskStatus(taskToEdit.CompletedStatus)}\nNew CompletedStatus: "
                , acceptableTrueValues, acceptableFalseValues);
        }
        public static void UpdateTask(List<Task> tasks)
        {
            // Allow the user to edit a task that has already been entered.
            Console.WriteLine("UPDATE TASK:\n");
            if (tasks.Count <= 0)
            {
                Console.WriteLine("There is no task to edit!\n");
                return;
            }

            int count = tasks.Count;

            DisplayAllTasks(tasks);

            int taskIndex = InputUtil.ReadInteger($"Which task # do you want to edit? (1 - {count}) : ", 1, count /*max # tasks*/);

            EditTask(tasks[taskIndex - 1]);

            DisplayTask(tasks[taskIndex - 1], taskIndex);

            Console.WriteLine("Task updated!\n");
        }
        public static void MarkTaskComplete(List<Task> tasks)
        {
            /*
             *  If the user chooses mark task complete:
             *  
             *      Ask the user which task number. Remember, they’ll be using 1 through the
             *      size of the list, not 0 through size - 1, so shift their input accordingly.
             *      
             *      Validate the number entered--make sure it’s in range. Prompt them until
             *      they enter a number in range.
             *      
             *      Display the task the user chose.
             *      
             *      Ask if they’re sure they want to mark the task as complete.
             *      
             *      If they answer Y, change the completion status within that item to true and
             *      return to the main menu. If they answer N, return to the main menu.
             *      
             *      Display the main menu options every time they return to the main menu.
             */
            Console.WriteLine("MARK TASK COMPLETE:\n");
            if (tasks.Count <= 0)
            {
                Console.WriteLine("No tasks to change!\n");
                return;
            }

            int count = tasks.Count;
            int taskIndex = InputUtil.ReadInteger($"Which task # do you want to mark complete? (1 - {count}) : ", 1, count /*max # tasks*/);

            DisplayTask(tasks[taskIndex - 1], taskIndex - 1);

            if (tasks[taskIndex - 1].CompletedStatus)
            {
                Console.WriteLine("This task is already marked Completed\n");
                return;
            }

            string confirm;
            do
            {
                confirm = InputUtil.ReadString("Are you sure you want to mark the task as complete? (y/n)").ToLower();
            } while (confirm != "y" && confirm != "n");

            if (confirm == "y")
            {
                tasks[taskIndex - 1].CompletedStatus = true;
                DisplayTask(tasks[taskIndex - 1], taskIndex - 1);
            }
        }
        public static List<string> GetTeamMembers(List<Task> tasks)
        {
            List<string> members = new List<string>();
            foreach (Task t in tasks)
            {
                if (!members.Exists(t.Name.ToLower().Equals))
                {
                    members.Add(t.Name);
                }
            }
            return members;
        }
        public static List<Task> GetMemberTasks(string memberName, List<Task> tasks)
        {
            List<Task> memberTasks = new List<Task>();
            memberName = memberName.ToLower();
            foreach (Task t in tasks)
            {
                if (t.Name.ToLower() == memberName)
                {
                    memberTasks.Add(t);
                }
            }
            return memberTasks;
        }
    }
    #region InputUtil class
    class InputUtil
    {
        public static string ReadString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine().Trim();
        }
        public static bool GetInputBool(string prompt, string[] trueValues, string[] falseValues)
        {
            List<string> acceptableTrueValues = new List<string>(trueValues);
            acceptableTrueValues.Add("t");
            acceptableTrueValues.Add("true");
            List<string> acceptableFalseValues = new List<string>(falseValues);
            acceptableFalseValues.Add("f");
            acceptableFalseValues.Add("false");
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine().Trim().ToLower();
                if (acceptableTrueValues.Exists(input.Equals))
                {
                    return true;
                }
                else if (acceptableFalseValues.Exists(input.Equals))
                {
                    return false;
                }
                Console.WriteLine("t=true/f=false expected!");
            }
        }
        public static DateTime GetInputDate(string message)
        {
            while (true)
            {
                try
                {
                    string input = ReadString(message);
                    if (input.Length > 0)
                    {
                        return DateTime.Parse(input);
                    }
                    throw new Exception("");
                }
                catch
                {
                    Console.WriteLine("Try again!");
                }
            }
        }
        public static string GetInputString(string message)
        {
            while (true)
            {
                try
                {
                    string input = ReadString(message);
                    if (input.Length > 0)
                    {
                        return input;
                    }
                    throw new Exception("");
                }
                catch
                {
                    Console.WriteLine("Try again!");
                }
            }
        }
        public static int ReadInteger(string message, int minValue, int maxValue)
        {
            while (true)
            {
                try
                {
                    int number = -1;
                    string input = ReadString(message);
                    number = int.Parse(input);
                    if (number >= minValue && number <= maxValue)
                    {
                        return number;
                    }
                    throw new Exception("");
                }
                catch
                {
                    Console.WriteLine("Try again");
                }
            }
        }
    }
    #endregion
}
