using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PinSharp
{
    internal static class TaskExtensions
    {
        public static ConfiguredTaskAwaitable Configured(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<T> Configured<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}
