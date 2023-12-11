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
        // SprawdŸ, czy uprawnienie jest ju¿ udzielone
        if (!Permission.HasUserAuthorizedPermission(WRITE_EXTERNAL_STORAGE_PERMISSION) || !Permission.HasUserAuthorizedPermission(READ_EXTERNAL_STORAGE_PERMISSION))
        {
            // Jeœli nie, poproœ o uprawnienie
            Permission.RequestUserPermission(WRITE_EXTERNAL_STORAGE_PERMISSION);
            Permission.RequestUserPermission(READ_EXTERNAL_STORAGE_PERMISSION);
            // Poczekaj na odpowiedŸ u¿ytkownika
            yield return new WaitForSeconds(1);

            // SprawdŸ ponownie status uprawnienia
            if (Permission.HasUserAuthorizedPermission(WRITE_EXTERNAL_STORAGE_PERMISSION) && Permission.HasUserAuthorizedPermission(READ_EXTERNAL_STORAGE_PERMISSION))
            {
                permissionGranted = true;
                Debug.Log("Uprawnienie do zapisu na zewnêtrznym urz¹dzeniu zosta³o udzielone.");
            }
            else
            {
                Debug.LogWarning("Uprawnienie do zapisu na zewnêtrznym urz¹dzeniu nie zosta³o udzielone.");
            }
        }
        else
        {
            permissionGranted = true;
        }

        // Tutaj mo¿esz dodaæ kod, który wykonuje siê po uzyskaniu uprawnienia
        if (permissionGranted)
        {
            Debug.Log("Mo¿esz teraz korzystaæ z uprawnienia do zapisu na zewnêtrznym urz¹dzeniu.");
        }
    }
}
