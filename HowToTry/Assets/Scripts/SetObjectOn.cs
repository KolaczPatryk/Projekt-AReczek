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
    private List<GameObject> prefabs;
    private int prefabID;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> rayHits = new List<ARRaycastHit>();
    private GameObject modelControl;
    private ChangeTexture textureScript;

    GameObject gmObj;
    GameObject deleteButton;

    private void Awake()
    {
        modelControl = GameObject.Find("ModelControl");
        deleteButton = GameObject.Find("ButtonDelete");
        textureScript = GameObject.Find("TextureManager").GetComponent<ChangeTexture>();
        deleteButton.SetActive(false);
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        prefabID = holdID.id;

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
        if (finger.currentTouch.tapCount == 0)
        {
            if (raycastManager.Raycast(finger.currentTouch.screenPosition, rayHits, TrackableType.PlaneWithinPolygon))
            {
                foreach (ARRaycastHit rayHit in rayHits)
                {
                    if (GameObject.FindGameObjectWithTag("Model")==null)
                    {
                        Pose rayPose = rayHit.pose;
                        gmObj = Instantiate(prefabs[prefabID], rayPose.position, rayPose.rotation);// <-- Tu inicjalizuje obiekt. Testowa³em na sztywnym Cube.
                                                                                         // Przy sklejaniu apk bêdzie trzeba to podmieniæ na inicjalizacje naszego wybranego obiektu, ale pozostawiæ pozycjê raycasta,
                                                                                         // aby obiekt pozosta³ w miejscu w którym klikneliœmy
                        //If odpowiadaj¹cy za obrócenia Eulerowskie maj¹ce na celu ustawienie obiektu wzglêdem Kamery
                        if (planeManager.GetPlane(rayHit.trackableId).alignment == PlaneAlignment.HorizontalUp)
                        {
                            Vector3 position = gmObj.transform.position;
                            Vector3 cameraPosition = Camera.main.transform.position;
                            Vector3 direction = cameraPosition - position;
                            Vector3 targetRotationEuler = Quaternion.LookRotation(direction).eulerAngles;
                            Vector3 scaledEuler = Vector3.Scale(targetRotationEuler, gmObj.transform.up.normalized);
                            Quaternion targetRotation = Quaternion.LookRotation(scaledEuler);
                            gmObj.transform.rotation = gmObj.transform.rotation * targetRotation;
                            gmObj.tag = "Model";
                            modelControl.transform.position = gmObj.transform.position;
                            modelControl.GetComponent<MakeChildScript>().MakeChild();
                            deleteButton.SetActive(true);
                            textureScript.AssingModel();
                            OnDisable();
                        }
                        //Proszê daæ Tag kamery na "MainCamera", poniewa¿ tak mi znajduje kamerê w tym skrypcie

                    }

                }
            }
        }else if(finger.currentTouch.tapCount == 1)
        {
            //Debug.Log("3D Object destroyed");
            //if(GameObject.FindGameObjectWithTag("Model")!=null)
            //{
            //    Destroy(GameObject.FindGameObjectWithTag("Model"));
            //}
        }
    }

    public void DeleteObject()
    {
        if (GameObject.FindGameObjectsWithTag("Model") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Model"));
            Debug.Log("INFO: 3D Object destroyed");
            deleteButton.SetActive(false);
            OnEnable();
        }
    }

    /*private void DoubleTapTap(EnhanedTouch.Finger finger)
    {

    }*/
}
