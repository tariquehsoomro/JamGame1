using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<EventKeyE, Action> events;
    
    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("EventManager script is not attached to a GameObject in the Scene");
                }
                else
                {
                    eventManager.Init();

                    DontDestroyOnLoad(eventManager);
                }
            }

            return eventManager;
        }
    }

    private void Init()
    {
        if (events == null)
            events = new Dictionary<EventKeyE, Action>();
    }

    public static void Listen(EventKeyE eventKey, Action listener)
    {
        Action newEvent;

        if (Instance.events.TryGetValue(eventKey, out newEvent))
        {
            newEvent += listener;
            Instance.events[eventKey] = newEvent;
        }
        else
        {
            newEvent += listener;
            Instance.events.Add(eventKey, newEvent);
        }
    }

    public static void Unlisten(EventKeyE eventKey, Action listner)
    {
        if (eventManager == null) return;

        Action newEvent;

        if (Instance.events.TryGetValue(eventKey, out newEvent))
        {
            newEvent -= listner;
            Instance.events[eventKey] = newEvent;
        }
    }

    public static void Trigger(EventKeyE eventKey)
    {
        Action thisEvent = null;

        if (Instance.events.TryGetValue(eventKey, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}
