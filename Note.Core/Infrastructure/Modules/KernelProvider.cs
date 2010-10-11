using Ninject;
using Ninject.Activation;

namespace Note.Core.Infrastructure.Modules
{
    public class KernelProvider : Provider<IKernel>
    {
        protected override IKernel CreateInstance(IContext context)
        {
            return context.Kernel;
        }
    }
}