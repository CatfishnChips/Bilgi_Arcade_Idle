using UnityEngine;
using UnityEngine.Events;

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

    public UnityAction<Vector2> onInputDragged = delegate { };
    public UnityAction onInputReleased = delegate { };

    //public event UnityAction onInputReleased;
    // Alternative way of doing events.

    // Never do Static events, even tho the object is destoryed they remain in the project until it is closed. Only do Singleton pattern for events.
}
