using System.Collections.Generic;
using System.Linq;
using EventEaseApp.Models;
using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace EventEaseApp.Services
{
    public class EventService
    {
        private List<Event> _events = new();
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "events";

        public EventService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task InitializeAsync()
        {
            await LoadEvents();
        }

        public List<Event> GetEvents()
        {
            return _events;
        }

        public async Task AddEvent(Event newEvent)
        {
            _events.Add(newEvent);
            await SaveEvents();
        }

        public async Task DeleteEvent(Event eventToDelete)
        {
            _events.Remove(eventToDelete);
            await SaveEvents();
        }

        private async Task SaveEvents()
        {
            await _localStorage.SetItemAsync(StorageKey, _events);
        }

        private async Task LoadEvents()
        {
            _events = await _localStorage.GetItemAsync<List<Event>>(StorageKey) ?? new List<Event>();
        }

        public async Task RegisterUser(Event evt, string userName, string email)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(email)
                && !evt.RegisteredUsers.Any(r => r.UserName == userName && r.Email == email))
            {
                evt.RegisteredUsers.Add(new Registration { UserName = userName, Email = email });
                await SaveEvents();
            }
        }
    }
}
