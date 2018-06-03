using Microsoft.Xrm.Sdk;
using System.Linq;

namespace Rappen.XTB.RRA
{
    public static class Extensions
    {
        public static string Name(this Entity record, EntityMetadataProxy entity)
        {
            return record.Contains(entity.Metadata.PrimaryNameAttribute) ? record[entity.Metadata.PrimaryNameAttribute].ToString() : record.Id.ToString();
        }

        public static void Merge(this EntityCollection target, EntityCollection source)
        {
            if (target.EntityName != source.EntityName)
            {
                return;
            }
            foreach (var entity in source.Entities)
            {
                if (!target.Entities.Any(e=> e.Id == entity.Id))
                {
                    target.Entities.Add(entity);
                }
            }
        }
    }
}
