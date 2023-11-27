using UnityEngine;
using UnityEngine.Android;
using System.Collections;

public class PermissionHandler : MonoBehaviour
{
    private const string WRITE_EXTERNAL_STORAGE_PERMISSION = "android.permission.WRITE_EXTERNAL_STORAGE";
    private const string READ_EXTERNAL_STORAGE_PERMISSION = "android.permission.READ_EXTERNAL_STORAGE";
    private bool permissionGranted = false;

    void Start()
    {
        StartCoroutine(RequestPermission());
    }

    IEnumerator RequestPermission()
    {
        // Sprawd�, czy uprawnienie jest ju� udzielone
        if (!Permission.HasUserAuthorizedPermission(WRITE_EXTERNAL_STORAGE_PERMISSION) || !Permission.HasUserAuthorizedPermission(READ_EXTERNAL_STORAGE_PERMISSION))
        {
            // Je�li nie, popro� o uprawnienie
            Permission.RequestUserPermission(WRITE_EXTERNAL_STORAGE_PERMISSION);
            Permission.RequestUserPermission(READ_EXTERNAL_STORAGE_PERMISSION);
            // Poczekaj na odpowied� u�ytkownika
            yield return new WaitForSeconds(1);

            // Sprawd� ponownie status uprawnienia
            if (Permission.HasUserAuthorizedPermission(WRITE_EXTERNAL_STORAGE_PERMISSION) && Permission.HasUserAuthorizedPermission(READ_EXTERNAL_STORAGE_PERMISSION))
            {
                permissionGranted = true;
                Debug.Log("Uprawnienie do zapisu na zewn�trznym urz�dzeniu zosta�o udzielone.");
            }
            else
            {
                Debug.LogWarning("Uprawnienie do zapisu na zewn�trznym urz�dzeniu nie zosta�o udzielone.");
            }
        }
        else
        {
            permissionGranted = true;
        }

        // Tutaj mo�esz doda� kod, kt�ry wykonuje si� po uzyskaniu uprawnienia
        if (permissionGranted)
        {
            Debug.Log("Mo�esz teraz korzysta� z uprawnienia do zapisu na zewn�trznym urz�dzeniu.");
        }
    }
}
