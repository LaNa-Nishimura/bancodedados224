using System.ComponentModel.DataAnnotations.Schema;

namespace Atividade060924.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Autor { get; set; }
        public string? Editora { get; set; }
        public string? Descrição { get; set; }
        public int NroPaginas { get; set; }
        
        public long CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }
}
