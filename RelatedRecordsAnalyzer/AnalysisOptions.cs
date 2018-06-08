using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Rappen.XTB.RRA
{
    public class AnalysisOptions
    {
        internal EntityMetadataProxy Parent;
        public bool Hidden { get; set; } = true;
        public bool OnlyData { get; set; } = true;
        public bool M2M { get; set; } = true;
        public List<CascadeType> AssignTypes { get; set; } = new List<CascadeType>();
        public List<CascadeType> ShareTypes { get; set; } = new List<CascadeType>();
        public List<CascadeType> DeleteTypes { get; set; } = new List<CascadeType>();

        public AnalysisOptions() { }

        public AnalysisOptions(EntityMetadataProxy parent)
        {
            Parent = parent;
        }
    }
}
