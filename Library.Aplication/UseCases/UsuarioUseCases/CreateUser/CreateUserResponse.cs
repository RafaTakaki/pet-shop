namespace Library.Aplication.UseCases.UsuarioUseCases.CreateUser
{
    public sealed record CreateUserResponse
    {
        public string Nome { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? Apelido { get; init; }
        public DateTime? DataNascimento { get; init; }

        public CreateUserResponse(string nome, string email, string? apelido, DateTime? dataNascimento)
        {
            Nome = nome;
            Email = email;
            Apelido = apelido;
            DataNascimento = dataNascimento;
        }
    }
}