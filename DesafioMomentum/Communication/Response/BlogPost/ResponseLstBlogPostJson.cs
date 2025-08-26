using DesafioMomentum.Communication.Request;

namespace DesafioMomentum.Communication.Response.BlogPost
{
    public class ResponseLstBlogPostJson
    {
        public List<ResponseBlogPostJson> Posts { get; set; } = new List<ResponseBlogPostJson>();
    }
}
