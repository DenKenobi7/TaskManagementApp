namespace TaskManagementApp.Application.Constants;

public static class ServiceBusConstants
{
    public static class QueueNames
    {
        public const string PushTaskStatusUpdateQueue = "push-task-status-update";
        public const string TaskActionCompletedQueue = "task-action-completed";
    }
}