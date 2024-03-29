﻿using System;

namespace ConfigurationManagementSystem.Domain.Entities;

/// <summary>
/// Base class that represents the domain entity.
/// </summary>
public abstract class DomainEntity
{
    public Guid Id { get; protected set; }
    public DateTime Created { get; private set; }
    public DateTime Modified { get; private set; }

    protected object Actual => this;

    public override bool Equals(object obj)
    {
        var other = obj as DomainEntity;

        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (Actual.GetType() != other.Actual.GetType()) return false;
        if (Id == Guid.Empty || other.Id == Guid.Empty) return false;

        return Id == other.Id;
    }

    public static bool operator ==(DomainEntity a, DomainEntity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(DomainEntity a, DomainEntity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (Actual.GetType().ToString() + Id).GetHashCode();
    }

    public void UpdateCreated(DateTime created)
    {
        if (Created != created)
        {
            Created = created;
        }
    }

    public void UpdateModified(DateTime modified)
    {
        if (Modified != modified)
        {
            Modified = modified;
        }
    }
}
