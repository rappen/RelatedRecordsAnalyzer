using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace Rappen.XTB.RRA
{
    public class QueryInfo
    {
        public Entity ParentEntity;
        public EntityMetadataProxy EntityInfo;
        public QueryBase Query;
        public string AttributesSignature;
        public EntityCollection Results;
        public RelationshipMetadataBase Relationship;

        public override string ToString()
        {
            return $"{EntityInfo.Metadata.DisplayName.UserLocalizedLabel.Label} ({Results?.Entities?.Count})";
        }

        public string CollectionDisplayName
        {
            get
            {
                var result = EntityInfo.Metadata.DisplayCollectionName.UserLocalizedLabel.Label;
                if (Relationship is OneToManyRelationshipMetadata rel1m)
                {
                    switch (rel1m.AssociatedMenuConfiguration.Behavior)
                    {
                        case AssociatedMenuBehavior.DoNotDisplay:
                            result += " (invisible)";
                            break;
                        case AssociatedMenuBehavior.UseLabel:
                            result = rel1m.AssociatedMenuConfiguration.Label.UserLocalizedLabel.Label;
                            break;
                    }
                }
                return result;
            }
        }
    }
}
