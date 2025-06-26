using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tasks.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<string>>
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _user;
        private readonly INotificationRepository _notifRepo;
        private readonly INotifier _notifier;
        private readonly IMemoryCacheService _cache;

        public UpdateTaskCommandHandler(
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

        public async Task<Result<string>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetByIdAsync(request.Id);
            if (task == null || task.UserId != _user.UserId)
                return Result<string>.Failure("Task not found or unauthorized.");

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.Status = Domain.Enums.TaskStatus.InProgress;

            _taskRepo.Update(task);
            await _uow.SaveChangesAsync();

            await _cache.RemoveAsync($"tasks:user:{_user.UserId}");

            var notif = new Notification
            {
                UserId = _user.UserId,
                Message = $"Task '{task.Title}' was updated.",
                CreatedAt = DateTime.UtcNow
            };

            await _notifRepo.AddAsync(notif);
            await _uow.SaveChangesAsync();

            await _notifier.SendNotificationAsync(_user.UserId, notif.Message);

            return Result<string>.Success("Task updated.");
        }
    }

}
