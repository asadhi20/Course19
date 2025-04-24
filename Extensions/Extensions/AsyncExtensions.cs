using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses.Extensions
{
    public static class AsyncExtensions
    {
        public static async void SafeFireAndForget(this System.Threading.Tasks.Task task, 
            bool continueOnCapturedContext = true, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }

    };

}
