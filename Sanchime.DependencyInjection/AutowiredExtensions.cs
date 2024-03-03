using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanchime.DependencyInjection;

public static class AutowiredExtensions
{
    public static void Autowired(this IServiceProvider provider, object instance)
    {
        if (provider is not AutowiredServiceProvider)
        {
            using var autowired = new AutowiredServiceProvider(provider);
            autowired.InternalAutowried(instance);
        }   
    }

    public static void Autowired<TInstance>(this IServiceProvider provider, TInstance instance)
    {
        if (provider is not AutowiredServiceProvider)
        {
            using var autowired = new AutowiredServiceProvider(provider);
            autowired.InternalAutowried(instance);
        }
    }
}
