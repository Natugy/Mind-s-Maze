using System.Collections;
using UnityEngine;

public class MultiplierCube : MonoBehaviour
{
    public GameObject objectToSpawn; 
    static public int maxObjet = 3000;   
    [HideInInspector]static public int currentGeneration = 0;

    private bool hasCollided = false; 
    private bool canCollide =false;


    void Start()
    {
        // IMPORTANT : Chaque balle (l'originale et les clones) 
        // doit lancer son propre compte à rebours en apparaissant.
        StartCoroutine(ActivateCollider());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 1. On vérifie d'abord si le cube a le droit de collisionner (le délai de 3s)
        if (!canCollide || hasCollided) return;

        // 2. On vérifie si on touche le sol OU un autre cube
        // (Assure-toi que tes cubes ont aussi le tag "Cube" ou utilise le nom)
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            if (currentGeneration < maxObjet)
            {
                hasCollided = true; 
                Multiply();
            }
        }
    }

    void Multiply()
    {
        
        for (int i = 0; i < 2; i++)
        {
            
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0.5f, Random.Range(-0.2f, 0.2f));
            
            GameObject newObj = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
            
            
            MultiplierCube script = newObj.GetComponent<MultiplierCube>();
            if (script != null)
            {
                currentGeneration = currentGeneration + 1;
                
                // Optionnel : donner une petite impulsion pour les séparer
                Rigidbody rb = newObj.GetComponent<Rigidbody>();
                if(rb != null) {
                    rb.AddForce(Vector3.up * 8f, ForceMode.Impulse);
                }
                // StartCoroutine(script.ActivateCollider());
                
            }
        }
    }

    public IEnumerator ActivateCollider()
    {
        // Attendre 3 secondes
        yield return new WaitForSeconds(0.5f);

        // Passer la variable à true
        canCollide = true;
        
        // Debug.Log("La multiplication est maintenant activée !");
    }
}
