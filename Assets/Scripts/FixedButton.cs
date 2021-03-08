using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool Jump;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click Button!");
        Jump = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Free Button!");
        Jump = false;
    }

    public bool sholudJump() => Jump;
}
