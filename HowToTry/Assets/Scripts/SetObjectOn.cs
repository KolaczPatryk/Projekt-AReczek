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

    GameObject gmObj;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

    }

    //Dzia�anie OnClick
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
        if (finger.currentTouch.tapCount == 0)
        {
            if (raycastManager.Raycast(finger.currentTouch.screenPosition, rayHits, TrackableType.PlaneWithinPolygon))
            {
                foreach (ARRaycastHit rayHit in rayHits)
                {
                    if (GameObject.FindGameObjectWithTag("3DObject")==null)
                    {
                        Pose rayPose = rayHit.pose;
                        gmObj = Instantiate(prefabs, rayPose.position, rayPose.rotation);// <-- Tu inicjalizuje obiekt. Testowa�em na sztywnym Cube.
                                                                                         // Przy sklejaniu apk b�dzie trzeba to podmieni� na inicjalizacje naszego wybranego obiektu, ale pozostawi� pozycj� raycasta,
                                                                                         // aby obiekt pozosta� w miejscu w kt�rym klikneli�my
                        //If odpowiadaj�cy za obr�cenia Eulerowskie maj�ce na celu ustawienie obiektu wzgl�dem Kamery
                        if (planeManager.GetPlane(rayHit.trackableId).alignment == PlaneAlignment.HorizontalUp)
                        {
                            Vector3 position = gmObj.transform.position;
                            Vector3 cameraPosition = Camera.main.transform.position;
                            Vector3 direction = cameraPosition - position;
                            Vector3 targetRotationEuler = Quaternion.LookRotation(direction).eulerAngles;
                            Vector3 scaledEuler = Vector3.Scale(targetRotationEuler, gmObj.transform.up.normalized);
                            Quaternion targetRotation = Quaternion.LookRotation(scaledEuler);
                            gmObj.transform.rotation = gmObj.transform.rotation * targetRotation;
                            gmObj.tag = "3DObject";
                        }
                        //Prosz� da� Tag kamery na "MainCamera", poniewa� tak mi znajduje kamer� w tym skrypcie

                    }

                }
            }
        }else if(finger.currentTouch.tapCount == 1)
        {
            Debug.Log("KUTAAAASSS!!");
            if(GameObject.FindGameObjectWithTag("3DObject")!=null)
            {
                Destroy(GameObject.FindGameObjectWithTag("3DObject"));
            }
        }
    }
    /*private void DoubleTapTap(EnhanedTouch.Finger finger)
    {

    }*/
}
