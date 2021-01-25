using System;

namespace ConfigurationService.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

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
}
