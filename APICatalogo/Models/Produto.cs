﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{

    [Table("Produtos")]
    public class Produto
    {


        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage ="Nome deve ser obrigatório!")]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,1000, ErrorMessage = "O valor deve ser entre 1 e 1000")]
        public decimal preco { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria? Categoria { get; set; }    

    }
}
