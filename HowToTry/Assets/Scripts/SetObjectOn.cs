using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhanedTouch = UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class SetObjectOn : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabs;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> rayHits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

    }

    //Dzia³anie OnClick
    private void OnEnable()
    {
        EnhanedTouch.TouchSimulation.Enable();
        EnhanedTouch.EnhancedTouchSupport.Enable();
        EnhanedTouch.Touch.onFingerDown += TapTap;
    }

    private void OnDisable()
    {
        EnhanedTouch.TouchSimulation.Disable();
        EnhanedTouch.EnhancedTouchSupport.Disable();
        EnhanedTouch.Touch.onFingerDown -= TapTap;
    }
    //END
    private void TapTap(EnhanedTouch.Finger finger)
    {
        if(finger.index != 0)
        {
            return;
        }

        if (raycastManager.Raycast(finger.currentTouch.screenPosition, rayHits, TrackableType.PlaneWithinPolygon))
        {
            foreach(ARRaycastHit rayHit in rayHits)
            {
                Pose rayPose = rayHit.pose;
                GameObject gmObj = Instantiate(prefabs, rayPose.position, rayPose.rotation);// <-- Tu inicjalizuje obiekt. Testowa³em na sztywnym Cube.
                                                                                            // Przy sklejaniu apk bêdzie trzeba to podmieniæ na inicjalizacje naszego wybranego obiektu, ale pozostawiæ pozycjê raycasta,
                                                                                            // aby obiekt pozosta³ w miejscu w którym klikneliœmy
                
                //If odpowiadaj¹cy za obrócenia Eulerowskie maj¹ce na celu ustawienie obiektu wzglêdem Kamery
                if (planeManager.GetPlane(rayHit.trackableId).alignment == PlaneAlignment.HorizontalUp)
                {
                    Vector3 position = gmObj.transform.position;
                    Vector3 cameraPosition = Camera.main.transform.position;
                    Vector3 direction = cameraPosition - position;
                    Vector3 targetRotationEuler = Quaternion.LookRotation(direction).eulerAngles;
                    Vector3 scaledEuler = Vector3.Scale(targetRotationEuler,gmObj.transform.up.normalized);
                    Quaternion targetRotation = Quaternion.LookRotation(scaledEuler);
                    gmObj.transform.rotation = gmObj.transform.rotation * targetRotation;
                }
                //Proszê daæ Tag kamery na "MainCamera", poniewa¿ tak mi znajduje kamerê w tym skrypcie
            }
        }
    }
}
