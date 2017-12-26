using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MMS.Directory.Banks.EntityFramework.Data
{
    internal static class DbContextHelper
    {
        public static string GetTableName<TEntity>(this DbContext context)
            where TEntity : class
        {
            var entitySet = GetEntitySetEdm<TEntity>(context);
            if (entitySet == null)
                throw new Exception($"Unable to find entity set '{typeof(TEntity).Name}' in edm metadata");

            return GetEdmPropertyValue<string>(entitySet, "Schema") + "." + GetEdmPropertyValue<string>(entitySet, "Table");
        }

        private static EntitySet GetEntitySetEdm<TEntity>(DbContext context)
        {
            var entityName = typeof(TEntity).Name;
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var entitySets = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .SingleOrDefault()
                .BaseEntitySets
                .OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type") || s.MetadataProperties["Type"].ToString() == "Tables");

            return entitySets.FirstOrDefault(t => t.Name == entityName);
        }

        private static TValue GetEdmPropertyValue<TValue>(MetadataItem entitySet, string propertyName)
        {
            entitySet.AssertNotNull(nameof(entitySet));

            if (entitySet.MetadataProperties.TryGetValue(propertyName, false, out var property))
            {
                if (property?.Value is TValue str)
                    return str;
            }

            throw new Exception($"Unable to get '{typeof(TValue).Name}' value of EDM property '{propertyName}' of set '{entitySet}'");
        }
    }
}
