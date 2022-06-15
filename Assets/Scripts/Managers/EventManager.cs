using System;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Keys;
using Assets.Scripts.Enums;

public class EventManager : MonoBehaviour
{
    #region Singleton Pattern

    public static EventManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion

    #region UnityEvents

    public UnityAction<JoystickInputParams> onInputDragged = delegate { };
    public UnityAction onInputReleased = delegate { };
    public UnityAction onInputTaken;

    public UnityAction onReset = delegate { };
    public UnityAction onPlay = delegate { };

     public UnityAction<int> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction<GameSaveDataParams> onSaveGameData = delegate { };
    public UnityAction<GameStates> onUpdateGameState = delegate { };
    public UnityAction<CollectableTypes, int> onUpdateCollectableType = delegate { };
    public UnityAction<CollectableTypes, int> onUpdateUICollectableType = delegate { };

    public Func<EconomyParams> onGetEconomyParams;

    // ALTERNATIVE, WE DO NOT NEED TO CREATE AN EMPTY DELEGATE
    //public UnityAction<Vector2> onInputDragged = (Vector2 vector2) => { };
    //public UnityAction onInputReleased = () => { };

    // If we are already checking for null whilst invoking the action, I don't we need to set the action to a null delegate.

    // ONE !!! THIS IS A WRONG WAY, BECAUSE EVENTS CANNOT BE INVOKED FROM ANOTHER CLASS !!!
    //public UnityEvent<Vector2> onInputDragged;
    //public UnityEvent onInputReleased;

    // TWO !!! THIS IS A WRONG WAY, BECAUSE EVENTS CANNOT BE INVOKED FROM ANOTHER CLASS !!!
    //public event UnityAction<Vector2> onInputDragged;
    //public event UnityAction onInputReleased;

    #endregion 

    #region C# Events

    // ONE C# WAY OF DOING ACTIONS
    //public Action<Vector2> onInputDragged;
    //public Action onInputReleased;

    // TWO C# WAY OF CREATING MANUAL DELEGATES
    //public delegate void OnInputDragged(Vector2 vector);
    //public OnInputDragged onInputDragged;

    //public delegate void OnInputReleased();
    //public OnInputReleased onInputReleased;

    // THREE !!! THIS IS A WRONG WAY, BECAUSE EVENTS CANNOT BE INVOKED FROM ANOTHER CLASS !!!
    //public delegate void OnInputDragged(Vector2 vector);
    //public event OnInputDragged onInputDragged;

    //public delegate void OnInputReleased();
    //public event OnInputReleased onInputReleased;

    // FOUR !!! THIS IS A WRONG WAY, BECAUSE IT REQUIRES AN OBJECT REFERENCE AND ALSO IS AN EVENT !!! 
    //public EventHandler<Vector2> onInputDragged;
    //public EventHandler onInputReleased;

    // FIVE !!! THIS IS A WRONG WAY, BECAUSE FUNC HAS A RETURN TYPE AND WE DO NOT NEED A RETURN TYPE !!!
    // Func instead of Action

    #endregion 

    // NOTES
    
    // If the object is temporary:
    // Never do Static events, even tho the object is destroyed they remain in the project until it is closed. Only do Singleton pattern for events.

    // To use Events, both EventManager and InputManager should be in one class OR InputManager should have its own input Events.
}
