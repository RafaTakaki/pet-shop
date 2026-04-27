namespace Library.Aplication.UseCases.PetUseCases.CadastrarServico;

public record CadastrarServicoDTO(
    string TipoServico,
    DateTime DataServico,
    string Observacao
);
