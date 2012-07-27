using Uncas.NowSite.Domain.Infrastructure;

namespace Uncas.NowSite.Domain.ReadModels
{
    public class FactReadModel : ReadModel
    {
        public override string ModelName
        {
            get { return "FactReadModel"; }
        }

        public string Name { get; set; }
    }
}