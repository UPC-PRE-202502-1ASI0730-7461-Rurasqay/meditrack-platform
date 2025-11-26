using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace MediTrackPlatform.API.Organization.Domain.Model.Aggregates;

public partial class SeniorCitizen : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}