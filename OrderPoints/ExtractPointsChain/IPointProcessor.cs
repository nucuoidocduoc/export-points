using OrderPoints.ExtractPointsChain.Data;

namespace OrderPoints.ExtractPointsChain
{
    public interface IPointProcessor
    {
        void Implement(PointProcessContext processContext);

        IPointProcessor SetNext(IPointProcessor nextProcessor);
    }

    public abstract class PointProcessorBase : IPointProcessor
    {
        protected IPointProcessor _next;

        public abstract void Implement(PointProcessContext processContext);

        public IPointProcessor SetNext(IPointProcessor nextProcessor)
        {
            _next = nextProcessor;
            return nextProcessor;
        }
    }
}