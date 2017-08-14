using System;

namespace KChatServer
{
    public enum TaskState
    {
        NewTask,
        InProgress,
        Finished,
        Closed
    }

    public class KChatTask
    {
        public int TaskId { get; private set; }
        public string Author { get; private set; }
        public DateTime CreatedOnDate { get; private set; }
        public string Assignee { get; private set; }
        public TaskState TaskState { get; private set; }
        public string TaskDesc { get; private set; }
        public string TaskPriority { get; private set; }

        public KChatTask(string author, string assignee, string taskD, string taskP)
        {
            Author = author;
            CreatedOnDate = DateTime.Now;
            Assignee = assignee;
            TaskState = TaskState.NewTask;
            TaskDesc = taskD;
            TaskPriority = taskP;
        }

        public KChatTask(string author, string assignee, string taskD, string taskP, string taskState, string createDate)
        {
            Author = author;
            TaskDesc = taskD;
            Assignee = assignee;
            TaskPriority = taskP;

            DateTime.TryParse(createDate, out DateTime tmp);
            CreatedOnDate = tmp;

            Enum.TryParse<TaskState>(taskState, out TaskState tmp2);
            TaskState = tmp2;
        }

    }
}
