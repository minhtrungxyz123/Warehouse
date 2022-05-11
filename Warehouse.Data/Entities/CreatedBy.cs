using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public class CreatedBy : BaseEntity
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string? Avarta { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateRegister { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}