using System;

namespace TestCQRS.Lifecycle
{
    public class ScopedService : IScopedService
    {
        private Guid _id;

        public ScopedService()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetID()
        {
            return _id;
        }
    }
}
