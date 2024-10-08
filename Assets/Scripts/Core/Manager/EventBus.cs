using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.Manager
{
    public enum EventType
    {
        StartReception, FinishReception, Recruit, CompleteRecruit, OnDie
    }

    public static class EventBus
    {
        static readonly Dictionary<EventType, UnityEvent> events = new Dictionary<EventType, UnityEvent>();
	
        public static void EventSubscribe(EventType eventType, UnityAction listner)
        {
            UnityEvent e;
	
            if (events.TryGetValue(eventType, out e))
                e.AddListener(listner);
            else
            {
                e = new();
                e.AddListener(listner);
                events.Add(eventType, e);
            }
        }
	
        public static void EventUnsubscribe(EventType eventType, UnityAction listner)
        {
            UnityEvent e;
	
            if(events.TryGetValue(eventType, out e))
                e.RemoveListener(listner);
        }
	
        public static void Publish(EventType eventType)
        {
            UnityEvent e;
	
            if (events.TryGetValue(eventType, out e))
                e.Invoke();
        }
    }
}