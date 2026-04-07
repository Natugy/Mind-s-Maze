using System;
using UnityEngine;

public class PoissonMovment : MonoBehaviour
{
    public Transform target;      // Glisse ta Caméra ici
    private float distance; // Distance entre le poisson et toi
    public float speed = 20.0f;    // Vitesse de rotation
    private float angle;

    void Awake()
    {
        distance = UnityEngine.Random.Range(6.0f, 9.0f);
    }
    void Update()
    {
        if (target == null) return;

        // Calcul de la position sur le cercle
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        // Mise à jour de la position
        transform.position = target.position + new Vector3(x, transform.position.y, z);

        // Optionnel : Pour que le poisson regarde vers l'avant (tangente)
        transform.LookAt(transform.position + new Vector3(-z, 0, -x));
    }
}
