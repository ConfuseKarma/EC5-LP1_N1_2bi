namespace N1_2Bi___LP1.Models
{
    public class PedidoViewModel : PadraoViewModel
    {
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }
        public int? QuantidadeProdutos { get; set; }
        public decimal? ValorTotal { get; set; }
        public string? NomeUsuario { get; set; }

    }
}
