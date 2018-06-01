using Microsoft.Xrm.Sdk;

namespace Rappen.XTB.RRA
{
    public static class Extensions
    {
        public static string Name(this Entity record, EntityMetadataProxy entity)
        {
            return record.Contains(entity.Metadata.PrimaryNameAttribute) ? record[entity.Metadata.PrimaryNameAttribute].ToString() : record.Id.ToString();
        }

    }
}
