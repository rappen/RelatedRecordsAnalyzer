using Microsoft.Xrm.Sdk.Metadata;

namespace Rappen.XTB.RRA
{
    public class MetaNodeInfo
    {
        public EntityMetadataProxy Entity;
        public RelationshipMetadataBase Relationship;

        public override string ToString()
        {
            return CollectionDisplayName;
        }

        public string CollectionDisplayName
        {
            get
            {
                var result = Entity.Metadata.DisplayCollectionName.UserLocalizedLabel.Label;
                if (Relationship is OneToManyRelationshipMetadata rel1m)
                {
                    switch (rel1m.AssociatedMenuConfiguration.Behavior)
                    {
                        case AssociatedMenuBehavior.UseLabel:
                            result = rel1m.AssociatedMenuConfiguration.Label.UserLocalizedLabel.Label;
                            break;
                    }
                    result += $" ({rel1m.ReferencingAttribute})";
                }
                else if (Relationship is ManyToManyRelationshipMetadata relmm)
                {
                    result = $"M:M {result} ({relmm.IntersectEntityName})";
                }
                return result;
            }
        }
    }
}
