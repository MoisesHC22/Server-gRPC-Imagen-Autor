using gRPC.Autor.Serve.Persistencia;
using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace gRPC.Autor.Serve.Services
{
    public class ImagenService : AutorImg.AutorImgBase
    {
        private readonly IMongoCollection<BsonDocument> _imagenesCollection;

        public ImagenService(MongoDBSetting mongoDBSetting)
        {
            var client = new MongoClient(mongoDBSetting.DefaultConnection);
            var database = client.GetDatabase(mongoDBSetting.Database);
            _imagenesCollection = database.GetCollection<BsonDocument>(mongoDBSetting.Collection);
        }

        public override async Task<Respuesta> GuardarImg(ImgRequest request, ServerCallContext context)
        {
            var respuesta = new Respuesta();

            try 
            {
                var imgData = Convert.FromBase64String(request.Img);

                var imgDocument = new BsonDocument
                {
                    { "id", request.Id },
                    { "img", request.Img }
                };

                await _imagenesCollection.InsertOneAsync(imgDocument);

                respuesta.Mensaje = "La imagen se guardo exitosamente";
            }
            catch(Exception ex) 
            {
                respuesta.Mensaje= $"Ocurrio un error al guardar la imagen: {ex.Message}";
            }

            return respuesta;
        }








    }
}
