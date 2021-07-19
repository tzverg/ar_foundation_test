using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ARFTest
{
    [RequireComponent(typeof(ARGroundManager), typeof(ARRaycastManager))]
    public class ARModelPlacer : MonoBehaviour
    {
        public GameObject ModelPrefab;
        public bool MultiplePlacing = true;
        public bool DisablePlanesAfterPlacing = true;
        public bool DisableTrackingAfterPlacing = false;
        public bool RotateModelToCamera = true;
        public Vector3 Offset;

        [SerializeField]
        private TrackableType trackableTypes = TrackableType.Planes;
        [SerializeField]
        private HitIndex hitIndex = HitIndex.Last;
        [SerializeField] private RaytraceDebug raytraceDebug;


        private readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private ARGroundManager planes;
        private ARRaycastManager raycastManager;
        private new Camera camera;
        private bool isPlaced;

        public event Action<GameObject> Placed;

        private enum HitIndex
        {
            First,
            Last
        }

        private void Awake()
        {
            planes = GetComponent<ARGroundManager>();
            raycastManager = GetComponent<ARRaycastManager>();
            camera = Camera.main;
        }

        public bool PlaceModel()
        {
            return PlaceModel(new Vector2(Screen.width / 2f, Screen.height / 2f));
        }

        public bool PlaceModel(Vector2 screenPosition)
        {
            if (!MultiplePlacing && isPlaced)
                return false;

            if (raycastManager.Raycast(screenPosition, hits, trackableTypes))
            {
                raytraceDebug.IncrementRayCounter();

                InstantiateModel(hits[GetHitIndex()].pose.position + Offset, Quaternion.identity);

                if (DisableTrackingAfterPlacing)
                    planes.ToggleTracking(false);

                if (DisablePlanesAfterPlacing)
                    planes.TogglePlanes(false);

                return isPlaced = true;
            }
            else return false;
        }

        public void InstantiateModel(Vector3 position, Quaternion rotation)
        {
            GameObject model = Instantiate(ModelPrefab, position + Offset, rotation);

            if (RotateModelToCamera)
                model.transform.LookAt(new Vector3(
                    camera.transform.position.x,
                    model.transform.position.y,
                    camera.transform.position.z));

            Placed?.Invoke(model);
        }

        private int GetHitIndex()
        {
            switch (hitIndex)
            {
                case HitIndex.First:
                    return 0;
                case HitIndex.Last:
                    return hits.Count > 0 ? hits.Count - 1 : 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}