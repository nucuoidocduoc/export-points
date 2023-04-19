using System.Collections.Generic;

namespace OrderPoints.ExtractPointsChain.Data
{
    public class Classification<TPath, TElement>
    {
        public TPath Path { get; set; }

        public List<TElement> Elements { get; set; }
    }
}