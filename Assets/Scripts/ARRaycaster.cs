using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ARFTest
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARRaycaster : MonoBehaviour
    {
        [SerializeField]
        private TrackableType TrackableTypes = TrackableType.All;

        private readonly List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        private ARRaycastManager raycastManager;
        private TrackableGenerator trackableGenerator;
        private Vector2 screenCenter;

        [SerializeField] private MyButtonScript raycastButton;
        [SerializeField] private RaytraceDebug raytraceDebug;

        private void Awake()
        {
            raycastManager = GetComponent<ARRaycastManager>();
            trackableGenerator = GetComponent<TrackableGenerator>();
        }

        private void Update()
        {
            //if (raycastButton.IsPressed)
            //{
            //    if (RaycastFromCenter())
            //    {
            //        raytraceDebug.IncrementRayCounter();
            //        trackableGenerator.InstantiateModelToThePosition(screenCenter);
            //    }
            //}
        }

        private bool RaycastFromCenter()
        {
            screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            return raycastManager.Raycast(screenCenter, _hits, TrackableTypes);
        }
    }
}