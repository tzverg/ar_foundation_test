using UnityEngine;
using UnityEngine.EventSystems;

public class MyButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressed { get; private set; }
    public void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("OnPointerDown");
        IsPressed = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        //Debug.Log("OnPointerUp");
        IsPressed = false;
    }
}