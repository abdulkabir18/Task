namespace Application.Abstractions
{
    public interface INotifier
    {
        Task SendNotificationAsync(Guid userId, string message);
    }
}
