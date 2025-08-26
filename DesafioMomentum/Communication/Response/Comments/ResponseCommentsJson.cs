using DesafioMomentum.Communication.Response.BlogPost;

namespace DesafioMomentum.Communication.Response.Comments
{
    public class ResponseCommentsJson
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdPost { get; set; } 

    }
}
