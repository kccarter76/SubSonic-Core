﻿using SubSonic.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubSonic.Extensions.Test.Models
{
    [Table(nameof(RealEstateProperty))]
    [DbCommandQuery(DbQueryType.Delete, typeof(DeleteRealEstateProperty))]
    [DbCommandQuery(DbQueryType.Update, typeof(UpdateRealEstateProperty))]
    [DbCommandQuery(DbQueryType.Insert, typeof(InsertRealEstateProperty))]
    public class RealEstateProperty
    {
        public RealEstateProperty()
        {
            Units = new HashSet<Unit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int StatusID { get; set; }

        [ForeignKey(nameof(StatusID))]
        public virtual Status Status { get; set; }

        public virtual bool? HasParallelPowerGeneration { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public virtual ICollection<Unit> Units { get; set; }
    }
}
