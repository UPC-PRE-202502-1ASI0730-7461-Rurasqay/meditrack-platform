using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public partial class Measurement: IEntityWithCreatedUpdatedDate
 {
     [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
     [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
 }