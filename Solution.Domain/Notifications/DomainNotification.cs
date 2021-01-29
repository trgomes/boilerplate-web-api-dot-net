using Solution.Domain.Core.Events;

namespace Solution.Domain.Notifications
{
    public class DomainNotification : Event
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
