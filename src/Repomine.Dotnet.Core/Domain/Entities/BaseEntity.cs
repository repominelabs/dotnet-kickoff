﻿namespace Repomine.Dotnet.Core.Domain.Entities;

public abstract class BaseEntity
{
    public virtual int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}