using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseGrenade : Item
{
    public float explosionForce = 1000f; // Fuerza de la explosi�n
    public float explosionRadius = 10f; // Radio de la explosi�n
    public float upwardModifier = 1f; // Modificador de la fuerza hacia arriba
    public LayerMask affectedLayers; // Capas afectadas por la explosi�n

    public override void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // Encuentra todos los objetos en el radio de la explosi�n
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, affectedLayers);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aplica la fuerza de explosi�n
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
        }

        // Efectos visuales y sonoros aqu�

        // Destruye la granada despu�s de la explosi�n
        Destroy(this);
    }
}

