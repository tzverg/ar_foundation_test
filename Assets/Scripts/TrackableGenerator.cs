using ARFTest;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

[RequireComponent(typeof(ARModelPlacer))]
public class TrackableGenerator : MonoBehaviour
{
    private ARModelPlacer modelPlacer;
    [SerializeField] private List<GameObject> trackableList;

    private new UnityEngine.Camera camera = null;

    private void Awake()
    {
        camera = UnityEngine.Camera.main;
        modelPlacer = GetComponent<ARModelPlacer>();
        LeanTouch.OnFingerTap += HandleClick;
    }

    public void OnDestroy()
    {
        LeanTouch.OnFingerTap -= HandleClick;
    }

    public void InstantiateModelToThePosition(Vector2 newModelPosition)
    {
        if (Physics.Raycast(camera.ScreenPointToRay(newModelPosition), out RaycastHit raycastHit))
        {
            if (Application.isEditor
                     && modelPlacer.ModelPrefab != null)
            {
                modelPlacer.InstantiateModel(raycastHit.point, Quaternion.identity);
            }
        }
    }

    private void HandleClick(LeanFinger leanFinger)
    {
        if (leanFinger.IsOverGui)
            return;

        if (Physics.Raycast(camera.ScreenPointToRay(leanFinger.ScreenPosition), out RaycastHit raycastHit))
        {
            GameObject selectedGameObject = raycastHit.collider.gameObject;

            //if (selectedGameObject.CompareTag("AREffect"))
            //    parentController.SelectObject(selectedGameObject);
            if (Application.isEditor
                     //&& selectedGameObject.CompareTag("ARPlane")
                     && modelPlacer.ModelPrefab != null)
                modelPlacer.InstantiateModel(raycastHit.point, Quaternion.identity);
            // Uncomment if we need placing by tab on free space.
            /*else if (selectedGameObject.CompareTag("ARPlane")
                     && LeanTouch.Fingers.Count == 1
                     && LeanTouch.Fingers[0].SwipeScreenDelta.magnitude < 30
                     && arModelPlacer.ModelPrefab != null)
            {
                if (Application.isEditor)
                    arModelPlacer.InstantiateModel(raycastHit.point, Quaternion.identity);
                else
                    arModelPlacer.PlaceModel(leanFinger.ScreenPosition);
            }*/
            //else
            //    parentController.UnselectObject();
        }
        //else parentController.UnselectObject();
    }

    public void CreateRandomTrackableObject()
    {
        int randomID = UnityEngine.Random.Range(0, trackableList.Count);
        modelPlacer.ModelPrefab = trackableList[randomID];
    }
}