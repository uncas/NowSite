using Uncas.NowSite.Domain.Infrastructure;

namespace Uncas.NowSite.Domain.ReadModels
{
    public class PictureReadModel : ReadModel
    {
        public string PictureUrl { get; set; }

        public override string ModelName
        {
            get { return "PictureReadModel"; }
        }
    }
}