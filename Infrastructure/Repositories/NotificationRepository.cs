using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.IRepository;
using Dapper;
using Domain.Model;
using Infrastructure.DapperContext;

namespace Infrastructure.Repositories
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly Dappercontext _context;

        public NotificationRepository(Dappercontext context)
        {
            _context = context;
        }

        public async Task<bool> SaveNotificationAsync(Notification notification)
        {
            var sql = @"
                INSERT INTO Notifications (SentBy, Message, TargetRole, SentAt, CreatedAt)
                VALUES (@SentBy, @Message, @TargetRole, @SentAt, @CreatedAt)";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new
                {
                    notification.SentBy,
                    notification.Message,
                    notification.TargetRole,
                    SentAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                });

                return result > 0;
            }
        }
    }
}
