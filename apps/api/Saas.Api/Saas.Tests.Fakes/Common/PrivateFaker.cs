using System.Reflection;
using Bogus;

namespace Saas.Tests.Fakes.Common;

// https://github.com/bchavez/Bogus/issues/213#issuecomment-475985821
internal sealed class PrivateFaker<T> : Faker<T> where T : class
{
    public PrivateFaker() { }
    public PrivateFaker(PrivateBinder binder) : base(binder: binder) { }
    
    public PrivateFaker<T> UsePrivateConstructor() =>
        (CustomInstantiator(_ => (Activator.CreateInstance(typeof(T), nonPublic: true) as T)!) as PrivateFaker<T>)!;
}