using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ActionSchedulerService
{
    public class ActionSchedulerService : IActionSchedulerService
    {
        private readonly Queue<ActionWrapper> _actions = new Queue<ActionWrapper>();
        private Timer? _timer;
        private bool _isRunning = false;
        //Optional
        private readonly ILogger<ActionSchedulerService> _logger;
        public void RegisterAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (_actions)
            {
                _actions.Enqueue(new ActionWrapper(action));
            }
        }

        public void RegisterAction<T>(Action<T> action, T arg)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (_actions)
            {
                _actions.Enqueue(new ActionWrapper(action, arg));
            }
        }

        public void RegisterAction<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (_actions)
            {
                _actions.Enqueue(new ActionWrapper(action, arg1, arg2));
            }
        }

        public void RegisterAction<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (_actions)
            {
                _actions.Enqueue(new ActionWrapper(action, arg1, arg2, arg3));
            }
        }

        public ActionSchedulerService(ILogger<ActionSchedulerService> logger)
        {
            _logger = logger;
        }

        public void Start(int intervalMilliseconds)
        {
            if (_isRunning)
                throw new InvalidOperationException("The service is already launched.");

            _isRunning = true;
            _timer = new Timer(ExecuteActions, null, 0, intervalMilliseconds);
        }

        public void Stop()
        {
            if (!_isRunning)
                throw new InvalidOperationException("The service is not running.");

            _timer?.Change(Timeout.Infinite, 0);
            _isRunning = false;
        }

        private void ExecuteActions(object? state)
        {
            lock (_actions)
            {
                while (_actions.Count > 0)
                {
                    var actionWrapper = _actions.Dequeue();
                    try
                    {
                        actionWrapper.Execute();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error while executing action: {ex.Message}");
                    }
                }
            }
        }
    }
}
