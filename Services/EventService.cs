using System.Collections.Generic;
using System.Linq;
using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    public class EventService
    {
        private readonly List<Event> _events = new();

        public IEnumerable<Event> GetEvents()
        {
            return _events;
        }

        public void AddEvent(Event newEvent)
        {
            _events.Add(newEvent);
        }

        public void DeleteEvent(Event eventToDelete)
        {
            _events.Remove(eventToDelete);
        }

        public void RegisterUser(Event evt, string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !evt.RegisteredUsers.Contains(userName))
            {
                evt.RegisteredUsers.Add(userName);
            }
        }
    }
}
