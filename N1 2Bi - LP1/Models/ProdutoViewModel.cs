namespace N1_2Bi___LP1.Models
{
    public class ProdutoViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }

        public string ImagemEmBase64 { get; set; }
    }
}
