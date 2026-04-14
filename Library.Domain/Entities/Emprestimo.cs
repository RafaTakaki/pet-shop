using Library.Domain.Enums;

namespace Library.Domain.Entities;

public class Emprestimo
{
    public string Id { get; set; }
    public string IdUsuario { get; set; }
    public string IdLivro { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucaoPrevista { get; set; }
    public DateTime? DataDevolucaoReal { get; set; }
    public StatusEmprestimo Status { get; set; }

    public Emprestimo(string id,
                      string idUsuario,
                      string idLivro,
                      DateTime dataEmprestimo,
                      DateTime dataDevolucaoPrevista,
                      DateTime? dataDevolucaoReal,
                      StatusEmprestimo status)
    {
        Id = id;
        IdUsuario = idUsuario;
        IdLivro = idLivro;
        DataEmprestimo = dataEmprestimo;
        DataDevolucaoPrevista = dataDevolucaoPrevista;
        DataDevolucaoReal = dataDevolucaoReal;
        Status = status;
    }
}