using System.Data.SQLite;

namespace KChatServer
{
    public static class DbHelper
    {
        public static SQLiteConnection Conn { get; set; }

        public static void CreateTable()
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS KTasks (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Author varchar(50) NOT NULL, CreatedOnDate Date NOT NULL, Assignee varchar(50) NOT NULL, TaskState charchar(20) NOT NULL, TaskDescription varchar(300) NOT NULL, TaskPriority INTEGER NOT NULL);";
            cmd.ExecuteNonQuery();
        }

        public static void SetTask(KChatTask task)
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO KTasks(Author,CreatedOnDate,Assignee,TaskState,TaskDescription, TaskPriority) " +
                              $"VALUES('{task.Author}','{task.CreatedOnDate.ToString()}','{task.Assignee}','{task.TaskState.ToString()}','{task.TaskDesc}',{task.TaskPriority})";
            cmd.ExecuteNonQuery();
        }

        public static void UpdateTaskState(int taskId, TaskState newState)
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = $"UPDATE KTasks SET TaskState = '{newState.ToString()}' WHERE Id = '{taskId}'";
        }
    }
}
