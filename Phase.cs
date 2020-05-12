using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTool
{
    public static class Phase
    {
        public static TResult Run<TResult>(string phase, Func<TResult> method)
        {
			try { return method(); }
            catch (CancelTaskException)
            {
                throw;
            }
			catch (Exception e)
			{
                throw new Exception($"Exception thrown at phase '{phase}', see InnerException for details", e);
			}
        }

        public static void Run(string phase, Action method)
        {
            Run(phase, () => { method(); return 0; });
        }
    }
}
