using System.Collections.Generic;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class Part
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Retail { get; set; }
        public object Model { get; set; }
        public object Width { get; set; }
        public object Ratio { get; set; }
        public object Diameter { get; set; }
        public object ConstructionType { get; set; }
        public object LoadIndex { get; set; }
        public object SpeedRating { get; set; }
        public PartType PartType { get; set; }
        public PartStatus PartStatus { get; set; }
        public List<object> DotNumbers { get; set; }
    }



}
