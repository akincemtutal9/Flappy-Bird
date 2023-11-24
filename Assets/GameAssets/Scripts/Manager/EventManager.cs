using System;
using System.Collections.Generic;

 public enum GameEvent{
    OnDie,
    OnJump,
    OnScore,
    OnHit,
    OnCoin,
    OnUpdateUI,
    
}
public static class EventManager 
{
    private static Dictionary<GameEvent, Action> eventDictionary = new Dictionary<GameEvent, Action>();

    public static void AddHandler(GameEvent gameEvent, Action action){
        if(!eventDictionary.ContainsKey(gameEvent)){
            eventDictionary.Add(gameEvent, null);
        }
        eventDictionary[gameEvent] += action;
    }   

    public static void RemoveHandler(GameEvent gameEvent, Action action){
        if(eventDictionary.ContainsKey(gameEvent)){
            eventDictionary[gameEvent] -= action;
        }
    }

    public static void TriggerEvent(GameEvent gameEvent){
        if(eventDictionary.ContainsKey(gameEvent)){
            eventDictionary[gameEvent]?.Invoke();
        }
    }
}
