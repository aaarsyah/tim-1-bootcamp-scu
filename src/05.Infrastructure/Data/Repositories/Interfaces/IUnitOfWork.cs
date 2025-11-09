using Microsoft.EntityFrameworkCore.Storage;

namespace MyApp.Infrastructure.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ICourseRepository Courses { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IScheduleRepository Schedules { get; }
        IMyClassRepository MyClasses { get; }
        IUserManagerRepository UserManager { get; }
        IInvoiceManagerRepository InvoiceManager { get; }
        ICartItemRepository CartItems { get; }
        Task<int> SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
        void CommitTransaction(IDbContextTransaction transaction);
        void RollbackTransaction(IDbContextTransaction transaction);

    }

}