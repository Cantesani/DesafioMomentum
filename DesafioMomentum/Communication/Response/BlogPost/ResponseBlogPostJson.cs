using DesafioMomentum.Communication.Response.Comments;

namespace DesafioMomentum.Communication.Response.BlogPost
{
    public class ResponseBlogPostJson
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public int QuantidadeComments { get; set; }
        public List<ResponseCommentsJson> Comments { get; set; } = new List<ResponseCommentsJson>();

    }
}
