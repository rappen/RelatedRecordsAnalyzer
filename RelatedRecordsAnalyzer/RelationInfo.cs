using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Rappen.XTB.RRA
{
    struct RelationInfo
    {
        public EntityMetadataProxy Parent;
        public bool Hidden;
        public bool OnlyData;
        public bool M2M;
        public List<CascadeType> AssignTypes;
        public List<CascadeType> ShareTypes;
        public List<CascadeType> DeleteTypes;

        public RelationInfo(EntityMetadataProxy parent)
        {
            Parent = parent;
            Hidden = false;
            OnlyData = false;
            M2M = false;
            AssignTypes = new List<CascadeType>();
            ShareTypes = new List<CascadeType>();
            DeleteTypes = new List<CascadeType>();
        }
    }
}
