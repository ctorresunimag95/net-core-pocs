using System;

namespace TestCQRS.Lifecycle
{
    public class TransientService : ITransientService
    {
        private Guid _id;

        public TransientService()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetID()
        {
            return _id;
        }
    }
}
