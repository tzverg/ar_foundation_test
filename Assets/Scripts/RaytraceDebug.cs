using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RaytraceDebug : MonoBehaviour
{
    [SerializeField] private int raysCounter;
    private TextMeshProUGUI raysCounterTextLable;

    private void Awake()
    {
        raysCounterTextLable = GetComponent<TextMeshProUGUI>();
        UpdateCounter();
    }

    public void IncrementRayCounter()
    {
        raysCounter++;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        raysCounterTextLable.text = "rays: " + raysCounter;
    }
}