using MediatR;

namespace Warehouse.Data.EF
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, WarehouseDbContext ctx)
        {
        }
    }
}