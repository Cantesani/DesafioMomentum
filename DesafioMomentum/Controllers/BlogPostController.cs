using DesafioMomentum.Communication.Request.BlogPost;
using DesafioMomentum.Communication.Request.Comments;
using DesafioMomentum.Communication.Response.BlogPost;
using DesafioMomentum.Communication.Response.Comments;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMomentum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseBlogPostJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] RequestBlogPostJson request)
        {
            var response = new ResponseBlogPostJson
            {
                Titulo = request.Titulo,
                Conteudo = request.Conteudo 
            };

            /*
                Aqui eu chamaria o useCase para inserir a entity no banco de dados
                provavelmente usando um autoMapper para converter de request para entity
                exemplo:
                await _dbContext.BlogPost.AddAsync(BlogPost)
                await _UnitOfWork.Commit()

                dessa forma, diferente de como esta no cenario acima, retornaria ID que foi criado, etc.
            */

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseLstBlogPostJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllPosts()
        {
            //aqui eu faço um exemplo trazendo 2 posts com poucos comentarios em cada, com dados chumbados, tendo em vista que nao estou trazendo dados reais do banco.
            var lstPosts = new ResponseLstBlogPostJson
            {
                Posts = new List<ResponseBlogPostJson>
                {
                    new ResponseBlogPostJson { Id = 1,
                                               Titulo = "Post 1",
                                               Conteudo = "primeiro post",
                                               Comments = new List<ResponseCommentsJson> {
                                                    new ResponseCommentsJson { Id = 1, Descricao = "primeiro comentario primeiro post"},
                                                    new ResponseCommentsJson { Id = 2, Descricao = "segundo comentario primeiro post"}
                                               },
                                               //aqui eu traria a quantidade atraves de um COUNT, de forma dinamica.
                                               QuantidadeComments = 2,
                    },
                    new ResponseBlogPostJson { Id = 2,
                                               Titulo = "Post 2",
                                               Conteudo = "segundo post",
                                               QuantidadeComments = 3,
                                               Comments = new List<ResponseCommentsJson> {
                                                    new ResponseCommentsJson { Id = 1,  Descricao = "primeiro comentario segundo post"},
                                                    new ResponseCommentsJson { Id = 2, Descricao = "segundo comentario segundo post"},
                                                    new ResponseCommentsJson { Id = 3, Descricao = "terceiro comentario segundo post"}
                                               }
                    },
                }
            };

            if (lstPosts.Posts.Count > 0)
                return Ok(lstPosts);

            return NoContent();
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResponseBlogPostJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPostById([FromRoute] int Id)
        {
            /*
                aqui, novamente eu faço com os dados chumbados, trazendo de acordo com o parametro Id:

                return await _dbContext.Posts.AsNoTracking().Include(post => post.Comments).FirstOrDefaultAsync(x => x.Id == Id);
             */

            var response = new ResponseBlogPostJson
            {
                Id = Id,
                Titulo = $"Post {Id}",
                Conteudo = "conteudo do post",
                //assim como o metodo anterior, eu tambem traria a quantidade de comentarios de forma dinamica, com um COUNT;
                QuantidadeComments = 2,
                Comments = new List<ResponseCommentsJson> {
                                new ResponseCommentsJson { Id = 1, Descricao = "primeiro comentario do post"},
                                new ResponseCommentsJson { Id = 2, Descricao = "segundo comentario do post"}
                            }
            };

            return Ok(response);
        }


        //endpoint para criar o comentario vinculado ao post
        [HttpPost]
        [Route("{BlogPostId}/comments")]
        [ProducesResponseType(typeof(ResponseCommentsJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateComment([FromRoute] int blogPostId, [FromBody] RequestCommentJson request)
        {
            /*
               Aqui eu chamaria o useCase para inserir a entity Comments no banco de dados
               provavelmente tambem usando um autoMapper para converter de request para entity
               exemplo:
           
               var post = await _dbContext.BlogPost.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == blogPostId);
               await _dbContext.Comments.AddAsync(BlogPost)
             */

            //vamos supor que o post de retorno fosse o seguinte:
            var post = new ResponseBlogPostJson
            {
                Id = 1,
                Titulo = "Post 1",
                Conteudo = "conteudo do post"
            };

            var response = new ResponseCommentsJson
            {
                Descricao = request.Descricao,
                IdPost = post.Id
            };

            /*
               await _dbContext.Comments.AddAsync(response);
               await _unitOfWork.Commit();
            */

            return Created(string.Empty, response);
        }

    }
}
