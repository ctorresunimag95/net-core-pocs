using System;

namespace TestCQRS.Lifecycle
{
    public interface ITransientService
    {
        Guid GetID();
    }

    public interface IScopedService
    {
        Guid GetID();
    }

    public interface ISingletonService
    {
        Guid GetID();
    }
}
