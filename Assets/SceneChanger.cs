using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // Obligatoire pour changer de scène

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Porut()
    {
        Debug.Log("Proute");
    }
}
