using System.Collections.Generic;

namespace Core
{
    public class PauseManager: IPauseHandler
    {
        
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();

        public bool IsPaused { get; private set; }
        
        public void Pause(bool isPaused)
        {
            IsPaused = isPaused;

            foreach (var handler in _handlers)
            {
                handler.Pause(isPaused);
            }
        }

        public void Subscribe(IPauseHandler handler)
        {
            _handlers.Add(handler);
        }

        public void UnSubscribe(IPauseHandler handler)
        {
            _handlers.Remove(handler);
        }
    }
}