using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tasks.Delete
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result<string>>
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _user;
        private readonly INotificationRepository _notifRepo;
        private readonly INotifier _notifier;
        private readonly IMemoryCacheService _cache;

        public DeleteTaskCommandHandler(
            ITaskRepository taskRepo,
            IUnitOfWork uow,
            ICurrentUserService user,
            INotificationRepository notifRepo,
            INotifier notifier,
            IMemoryCacheService cache)
        {
            _taskRepo = taskRepo;
            _uow = uow;
            _user = user;
            _notifRepo = notifRepo;
            _notifier = notifier;
            _cache = cache;
        }

        public async Task<Result<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetByIdAsync(request.TaskId);
            if (task == null || task.UserId != _user.UserId)
                return Result<string>.Failure("Task not found or unauthorized.");

            _taskRepo.Delete(task);
            await _uow.SaveChangesAsync();

            await _cache.RemoveAsync($"tasks:user:{_user.UserId}");

            var notif = new Notification
            {
                UserId = _user.UserId,
                Message = $"Task '{task.Title}' was deleted.",
                CreatedAt = DateTime.UtcNow
            };

            await _notifRepo.AddAsync(notif);
            await _uow.SaveChangesAsync();

            await _notifier.SendNotificationAsync(_user.UserId, notif.Message);

            return Result<string>.Success("Task deleted.");
        }
    }

}
