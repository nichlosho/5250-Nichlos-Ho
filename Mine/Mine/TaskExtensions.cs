using System;
using System.Threading.Tasks;

namespace Mine
{
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task task, bool returnToCallingContext, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            
            //if provided action is not null then catch will throw exception
            catch (Exception e) when (onException != null)
            {
                onException(e);
            }
        }
    }
}
