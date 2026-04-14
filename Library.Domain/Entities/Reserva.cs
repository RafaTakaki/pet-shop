using Library.Domain.Enums;

namespace Library.Domain.Entities;

public class Reserva
{
    public string Id { get; set; }
    public string IdUsuario { get; set; }
    public string EmailUsuario { get; set; }
    public string IdLivro { get; set; }
    public string TituloLivro { get; set; }
    public DateTime DataReserva { get; set; }
    public DateTime? DataExpiracaoReserva { get; set; }
    public StatusReserva Status { get; set; }

    public Reserva(string id,
                   string idUsuario,
                   string email,
                   string idLivro,
                   string tituloLivro,
                   DateTime dataReserva,
                   DateTime? dataExpiracaoReserva,
                   StatusReserva status)
    {
        Id = id;
        IdUsuario = idUsuario;
        EmailUsuario = email;
        IdLivro = idLivro;
        TituloLivro = tituloLivro;
        DataReserva = dataReserva;
        DataExpiracaoReserva = dataExpiracaoReserva;
        Status = status;
    }
}