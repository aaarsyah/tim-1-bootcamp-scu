using MyApp.Base;
using MyApp.Domain.Models;

namespace MyApp.Infrastructure.Data.Repositories
{
    public interface ICrudRepository<T> where T : BaseEntity // untuk biasa
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByRefIdAsync(Guid refId);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid refId);
    }
    public interface IImmutableRepository<T> where T : BaseEntity // untuk schedule, cartitem, dan myclass
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByRefIdAsync(Guid refId);
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(Guid refId);
    }
    public interface IReadOnlyRepository<T> where T : BaseEntity // untuk invoice dan invoicedetail
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByRefIdAsync(Guid refId);
    }
    public interface ICategoryRepository : ICrudRepository<Category>
    {
        //Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        //Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    }
    public interface ICourseRepository : ICrudRepository<Course>
    {
        Task<IQueryable<Course>> GetAllAsQueryableAsync();
    }
    public interface IPaymentMethodRepository : ICrudRepository<PaymentMethod>
    {
    }
    public interface IScheduleRepository : IImmutableRepository<Schedule>
    {
    }
    public interface IMyClassRepository : IImmutableRepository<MyClass>
    {
        Task<IEnumerable<MyClass>?> GetAllByUserRefIdAsync(Guid userRefId);
    }
    public interface IUserManagerRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByRefIdAsync(Guid userRefId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User userEntity);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetDefaultRoleAsync();
        Task<Role?> GetRoleByNameAsync(string name);
        Task<bool> AddRoleToUserAsync(User userEntity, Role roleEntity);
        Task<bool> RemoveRoleFromUserAsync(User userEntity, Role roleEntity);
        Task<UserClaim> SetClaimForUserAsync(User userEntity, string claimType, string claimValue);
        Task<bool> RemoveClaimFromUserAsync(User userEntity, string claimType);
        Task<bool> SetActiveForUserAsync(Guid userRefId, bool isActive);
    }
    public interface IInvoiceManagerRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<IEnumerable<Invoice>?> GetAllByUserRefIdAsync(Guid userRefId);
        Task<Invoice?> GetByRefIdAsync(Guid invoiceRefId);
        Task<Invoice> CreateAsync(Invoice entity); // for checkout
        Task<IEnumerable<InvoiceDetail>?> GetDetailsByRefIdAsync(Guid invoiceRefId);
        Task<InvoiceDetail> CreateDetailAsync(InvoiceDetail entity); // for checkout
    }
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<IEnumerable<CartItem>?> GetAllByUserRefIdAsync(Guid userRefId);
        Task<IEnumerable<CartItem>> GetAllByRefIdsAsync(List<Guid> refIds); // for checkout
        Task<CartItem?> GetByRefIdAsync(Guid refId); // for delete
        Task<CartItem> CreateAsync(CartItem entity);
        Task<bool> DeleteAsync(Guid refId);
        Task DeleteEntitiesAsync(List<CartItem> entities); // for checkout
    }
}
