using System;
using System.Collections.Generic;
using System.Text;

namespace ActionSchedulerService
{
    public class ActionWrapper
    {
        public Delegate Action { get; }
        public object?[] Parameters { get; }

        public ActionWrapper(Delegate action, params object?[] parameters)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Parameters = parameters;
        }

        public void Execute()
        {
            Action.DynamicInvoke(Parameters);
        }
    }
}
