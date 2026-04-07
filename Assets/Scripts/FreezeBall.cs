using UnityEngine;

public class FreezeBall : MonoBehaviour
{
    private float timeBeforeFreeze ;
    [ColorUsage(true, true)] // Force l'utilisation d'une couleur HDR dans l'inspecteur
    public Color glowColor = Color.white * 2.0f; // Couleur de brillance (intensifiée par 2)
    
    private Rigidbody rb;
    private Renderer rend; // Pour accéder au matériau

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>(); // Récupère le Renderer de la balle
        timeBeforeFreeze = Random.Range(0.4f,1.2f);
        // Lance le compte à rebours dès que la balle apparaît
        Invoke("FreezeNow", timeBeforeFreeze);
    }

    void FreezeNow()
    {
        // 1. On coupe la physique
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true; // La balle ne bouge plus

        // 2. On active la brillance
        // En URP, la propriété standard pour l'émission est "_EmissionColor"
        rend.material.SetColor("_EmissionColor", glowColor);
        
        // Active explicitement le keyword d'émission au cas où
        rend.material.EnableKeyword("_EMISSION");

        Debug.Log("La balle est figée et brille !");
    }
}