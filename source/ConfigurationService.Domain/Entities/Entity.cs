using System;

namespace ConfigurationService.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }
    }
}
