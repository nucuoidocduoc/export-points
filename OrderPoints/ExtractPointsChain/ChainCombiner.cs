namespace OrderPoints.ExtractPointsChain
{
    public class ChainCombiner
    {
        public static IPointProcessor ImplementCombine()
        {
            var collectProcessor = new CollectProcessor();
            collectProcessor.SetNext(new ClassificationProcessor())
                .SetNext(new ExportProcessor());
            return collectProcessor;
        }
    }
}