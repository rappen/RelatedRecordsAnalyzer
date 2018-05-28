using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace Rappen.XTB.RRA
{
    public class QueryInfo
    {
        public EntityMetadataProxy EntityInfo;
        public QueryBase Query;
        public string AttributesSignature;
        public EntityCollection Results;
        public RelationshipMetadataBase Relationship;
    }
}
