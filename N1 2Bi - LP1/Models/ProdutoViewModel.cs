namespace N1_2Bi___LP1.Models
{
    public class ProdutoViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int? NumeroAvaliacoes { get; set; }
        public decimal? Avaliacao { get; set; }

        /// <summary>
        /// Imagem recebida do form pelo controller
        /// </summary>
        public IFormFile Imagem { get; set; }
        /// <summary>
        /// Imagem em bytes pronta para ser salva
        /// </summary>
        public byte[] ImagemEmByte { get; set; }
        /// <summary>
        /// Imagem usada para ser enviada ao form no formato para ser exibida
        /// </summary>
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else
                    return string.Empty;
            }
        }

    }
}
