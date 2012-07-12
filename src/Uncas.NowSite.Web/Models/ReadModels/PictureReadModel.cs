using Uncas.NowSite.Web.Models.Infrastructure;

namespace Uncas.NowSite.Web.Models.ReadModels
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