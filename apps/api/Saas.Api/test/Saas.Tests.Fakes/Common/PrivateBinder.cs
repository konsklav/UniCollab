using System.Reflection;
using System.Runtime.CompilerServices;

namespace Saas.Tests.Fakes.Common;

public class PrivateBinder : Bogus.Binder
{
    private const BindingFlags PrivateBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

    public override Dictionary<string, MemberInfo> GetMembers(Type t)
    {
        var members = base.GetMembers(t);
      
        var allPrivateMembers = t.GetMembers(PrivateBindingFlags)
            .OfType<FieldInfo>()
            .Where( fi => fi.IsPrivate )
            .Where( fi => !fi.GetCustomAttributes<CompilerGeneratedAttribute>().Any() )
            .ToArray();
                               
        foreach( var privateField in allPrivateMembers ){
            members.TryAdd(privateField.Name, privateField);
        }
        return members;
    }
}