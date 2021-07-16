using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARFTest
{
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARGroundManager : MonoBehaviour
    {
        private ARPlaneManager planeManager;

        public bool AnyPlaneTracked => planeManager.trackables.count > 0;

        private void Awake()
        {
            planeManager = GetComponent<ARPlaneManager>();
            planeManager.planesChanged += ToggleChangedPlanes;
        }

        // Used to display component activation checkbox in Inspector.
        private void Start()
        {
        }

        public void ToggleTracking(bool isOn)
        {
            planeManager.enabled = isOn;
        }

        public void TogglePlanes(bool isOn)
        {
            foreach (ARPlane plane in planeManager.trackables)
                plane.gameObject.SetActive(isOn);

            enabled = isOn;
        }

        public void TogglePlanes()
        {
            foreach (ARPlane plane in planeManager.trackables)
                plane.gameObject.SetActive(!enabled);

            enabled = !enabled;
        }

        private void ToggleChangedPlanes(ARPlanesChangedEventArgs args)
        {
            foreach (ARPlane plane in args.added)
                plane.gameObject.SetActive(enabled);

            foreach (ARPlane plane in args.updated.Where(plane => plane.gameObject.activeSelf != enabled))
                plane.gameObject.SetActive(enabled);
        }
    }
}