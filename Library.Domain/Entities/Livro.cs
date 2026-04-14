using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Livro
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public string Categoria { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public DateTime DataCadastro { get; set; }

        public Livro(
            string titulo,
            string autor,
            string isbn,
            string categoria,
            int quantidadeEstoque
        )
        {
            Id = Guid.NewGuid().ToString();
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            Categoria = categoria;
            QuantidadeEstoque = quantidadeEstoque;
            QuantidadeDisponivel = quantidadeEstoque;
            DataCadastro = DateTime.UtcNow;
        }
    }
}
