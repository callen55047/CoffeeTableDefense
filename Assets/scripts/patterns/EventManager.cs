using UnityEngine;
using System;

public static class EventManager
{
    public static event Action OnMainGameState;
    public static event Action OnSettingsGameState;
    
    public static void MainGameState()
    {
        OnMainGameState?.Invoke();
    }

    public static void SettingsGameState()
    {
        OnSettingsGameState?.Invoke();
    }
}