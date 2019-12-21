﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SubSonic.Infrastructure.Schema
{
    public class DbRelationshipMap
        : IDbRelationshipMap
    {
        private readonly string[] foreignKeyNames;

        public DbRelationshipMap(
            DbRelationshipType relationshipType,
            IDbEntityModel lookupModel,
            IDbEntityModel foreignModel, 
            string[] foreignKeyNames)
        {
            if((relationshipType == DbRelationshipType.HasManyWithMany) && (lookupModel is null))
            {
                throw new ArgumentNullException(nameof(lookupModel));
            }

            this.foreignKeyNames = foreignKeyNames ?? throw new ArgumentNullException(nameof(foreignKeyNames));
            RelationshipType = relationshipType;
            LookupModel = lookupModel;
            ForeignModel = foreignModel ?? throw new ArgumentNullException(nameof(foreignModel));
            
        }

        public DbRelationshipType RelationshipType { get; }

        public IDbEntityModel LookupModel { get; }

        public IDbEntityModel ForeignModel { get; }

        public IEnumerable<string> GetForeignKeys()
        {
            return foreignKeyNames;
        }
    }
}
