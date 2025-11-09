using Microsoft.EntityFrameworkCore.Storage;

namespace MyApp.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppleMusicDbContext _context;

    public ICategoryRepository Categories { get; }
    public ICourseRepository Courses { get; }
    public IPaymentMethodRepository PaymentMethods { get; }
    public IScheduleRepository Schedules { get; }
    public IMyClassRepository MyClasses { get; }
    public IUserManagerRepository UserManager { get; }
    public IInvoiceManagerRepository InvoiceManager { get; }
    public ICartItemRepository CartItems { get; }
    public UnitOfWork(
        AppleMusicDbContext context,
        ICategoryRepository categoryRepository,
        ICourseRepository courseRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IScheduleRepository scheduleRepository,
        IMyClassRepository myClassRepository,
        IUserManagerRepository userManagerRepository,
        IInvoiceManagerRepository invoiceManagerRepository,
        ICartItemRepository cartItemRepository
    )
    {
        _context = context;
        Categories = categoryRepository;
        Courses = courseRepository;
        PaymentMethods = paymentMethodRepository;
        Schedules = scheduleRepository;
        MyClasses = myClassRepository;
        UserManager = userManagerRepository;
        InvoiceManager = invoiceManagerRepository;
        CartItems = cartItemRepository;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }
    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }
    public void CommitTransaction(IDbContextTransaction transaction)
    {
        try
        {
            _context.SaveChanges();
            if (transaction != null)
            {
                transaction.Commit();
            }
        }
        catch
        {
            RollbackTransaction(transaction);
            throw;
        }
        finally
        {
            if (transaction != null)
            {
                transaction.Dispose();
            }
        }
    }

    public void RollbackTransaction(IDbContextTransaction transaction)
    {
        if (transaction != null)
        {
            transaction.Rollback();
            transaction.Dispose();
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
