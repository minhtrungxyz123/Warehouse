namespace Warehouse.Model.CreatedBy
{
    public class CreatedByModel
    {
        public string? Id { get; set; }
        public string AccountName { get; set; }
        public string? Password { get; set; }
        public string? Avarta { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateRegister { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}