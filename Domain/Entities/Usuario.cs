namespace Domain.Entities
{
    public class Usuario : EntityBase
    {
        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string SenhaHash { get; set; }

        public string Role { get; set; } = "User";
    }
}
