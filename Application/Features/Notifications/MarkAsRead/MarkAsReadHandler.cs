using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.MarkAsRead
{
    using Application.Abstractions;
    using MediatR;
    public class MarkAsReadHandler : IRequestHandler<MarkAsReadCommand, bool>
    {
        private readonly INotificationRepository _repo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsReadHandler(INotificationRepository repo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _repo.GetByIdAsync(request.NotificationId);
            if (notification == null || notification.UserId != _currentUser.UserId)
                return false;

            notification.IsRead = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
