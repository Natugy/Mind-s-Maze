using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleporterRoom : MonoBehaviour
{
    [Header("Configuration")]
    public string sceneToLoad; 
    public string playerTag = "MainCamera"; 

    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Chargement de la scène : " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
