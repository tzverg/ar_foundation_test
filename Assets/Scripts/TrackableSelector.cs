using UnityEngine;

public class TrackableSelector : MonoBehaviour
{
    [SerializeField] private Transform modifierParentTR;
    public GameObject SelectedObject { get; private set; }
    public bool selected;

    public void SelectObject(GameObject target)
    {
        target.transform.SetParent(modifierParentTR);
        SelectedObject = target;
        selected = true;
    }

    public void DeselectObject()
    {
        SelectedObject.transform.SetParent(gameObject.transform);
        SelectedObject = null;
        selected = false;
    }
}