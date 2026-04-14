using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarGatosCadastrados;

public sealed record ListarGatosCadastradosRequest() : IRequest<ListarGatosCadastradosResponse>;
