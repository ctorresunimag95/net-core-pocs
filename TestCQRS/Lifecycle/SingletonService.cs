using System;

namespace TestCQRS.Lifecycle
{
    public class SingletonService : ISingletonService
    {
        private Guid _id;

        public SingletonService()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetID()
        {
            return _id;
        }
    }
}
