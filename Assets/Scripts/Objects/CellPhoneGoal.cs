using UnityEngine;
using UnityEngine.Events;

public class CellPhoneGoal : MonoBehaviour
{
    public bool collected = false;

    [Header("Events")]
    [Space]
    public UnityEvent OnCollectEvent;

    private void Awake()
    {
        if (OnCollectEvent == null)
            OnCollectEvent = new UnityEvent();
    }
    
    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        collected = true;

        OnCollectEvent.Invoke();
    }

}
