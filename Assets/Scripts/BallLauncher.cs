using UnityEngine;
using UnityEngine.InputSystem; // Nécessaire pour les nouveaux inputs VR

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;    // Ton prefab de sphere avec Rigidbody
    public Transform launchPoint;    // Le bout de ta main/manette
    public float throwForce = 10f;   // La puissance du tir
    public InputActionProperty shootAction; // L'input du bouton

    void Update()
    {
        // On vérifie si on vient d'appuyer sur le bouton (Trigger)
        if (shootAction.action.WasPressedThisFrame())
        {
            // Debug.Log("tire");
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        // 1. Créer la balle au point de lancement
        GameObject ball = Instantiate(ballPrefab, launchPoint.position, launchPoint.rotation);
        
        // 2. Récupérer son Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        
        // 3. Lui donner une impulsion vers l'avant (Vector3.forward par rapport au launchPoint)
        rb.AddForce(launchPoint.forward * throwForce, ForceMode.Impulse);

        Color randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
        
        MeshRenderer ballMesh = ball.GetComponent<MeshRenderer>();

        // On change la couleur de base
        ballMesh.material.SetColor("_BaseColor", randomColor);
        
        // Optionnel : Si tu veux qu'elle brille de la même couleur au freeze plus tard
        // On peut stocker cette couleur dans le script FreezeBall de la nouvelle balle
        FreezeBall freezeScript = ball.GetComponent<FreezeBall>();
        if (freezeScript != null)
        {
            freezeScript.glowColor = randomColor * 6.0f; // On prépare la brillance HDR
        }

        // 4. Optionnel : Détruire la balle après 5 secondes pour ne pas ralentir le jeu
        // Destroy(ball, 5f);
    }
}