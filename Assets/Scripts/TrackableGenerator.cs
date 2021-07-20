using ARFTest;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

[RequireComponent(typeof(ARModelPlacer))]
public class TrackableGenerator : MonoBehaviour
{
    private ARModelPlacer modelPlacer;

    [SerializeField] private List<GameObject> trackableList;
    [SerializeField] private TrackableSelector parentController;

    private new UnityEngine.Camera camera = null;
    private Vector2 screenCenter;

    private void Awake()
    {
        camera = UnityEngine.Camera.main;
        modelPlacer = GetComponent<ARModelPlacer>();
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleClick;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerTap -= HandleClick;
    }

    public void InstantiateModelToTheCenterOfTheScreen()
    {
        CreateRandomTrackableObject();

        if (modelPlacer.ModelPrefab != null)
        {
            if (Application.isEditor)
            {
                screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

                if (Physics.Raycast(camera.ScreenPointToRay(screenCenter), out RaycastHit raycastHit))
                {
                    modelPlacer.InstantiateModel(raycastHit.point, Quaternion.identity);
                }
            }
            else
            {
                modelPlacer.PlaceModel();
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

            CreateRandomTrackableObject();

            if (selectedGameObject.GetComponent<TrackableObjectController>() != null)
            {
                if (!parentController.selected)
                {
                    parentController.SelectObject(selectedGameObject);
                }
                else
                {
                    parentController.DeselectObject();
                }
            }
            else if (Application.isEditor
                     //&& selectedGameObject.CompareTag("ARPlane")
                     && modelPlacer.ModelPrefab != null)
            {
                modelPlacer.InstantiateModel(raycastHit.point, Quaternion.identity);
            }
        }
        else parentController.DeselectObject();
    }

    public void CreateRandomTrackableObject()
    {
        int randomID = UnityEngine.Random.Range(0, trackableList.Count);
        modelPlacer.ModelPrefab = trackableList[randomID];
    }
}