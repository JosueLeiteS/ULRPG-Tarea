using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed;
    public int numberOfBullets = 15;
    public GameObject ps;
    public int dirPS = -90;

    private void OnEnable()
    {
        GameObject parentObject = transform.parent.gameObject;
        Rigidbody2D rbp = parentObject.GetComponent<Rigidbody2D>();
        Vector3 p = parentObject.GetComponent<PlayerMovement>().mLastPosition;
        FireShotgun(rbp, p);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FireShotgun(Rigidbody2D rbp, Vector3 p)
    {
        Debug.Log("BOOM");
        AudioManager.instance.Play("Daño3");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Calcula la dirección de retroceso (en este ejemplo, hacia la izquierda)
        Vector2 recoilDirection = -p;

        // Aplica la fuerza de retroceso al jugador
        rbp.AddForce(recoilDirection * 2000);

        Vector3 prefabScale = projectilePrefab.transform.localScale;
        for (int i = 0; i < numberOfBullets; i++)
        {
            // Cálculo del tamaño aleatorio
            float scaleMultiplier = Random.Range(0.8f, 1.2f); // Rango del 80% al 120% del tamaño original

            // Calcular la dirección de disparo en el ángulo de 30 grados
            float angle = Random.Range(-15f, 15f) - 90; // Rango del 15° al 45° respecto al ángulo central de 30°

            // Obtener la rotación del objeto padre
            Quaternion parentRotation = transform.rotation;

            // Calcular la rotación total considerando la rotación del objeto padre y el ángulo de disparo
            Quaternion totalRotation = Quaternion.Euler(0f, 0f, angle) * parentRotation;

            // Calcular la dirección de disparo en base a la rotación total
            Vector2 shootDirection = totalRotation * Vector2.up;

            // Crear la bala con tamaño escalado y dirección de disparo
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, totalRotation);
            projectile.transform.localScale *= scaleMultiplier;

            GameObject psI = Instantiate(ps, firePoint.position, Quaternion.Euler(-90, 0f, 0f));
            ParticleSystem.ShapeModule shapeModule = psI.GetComponent<ParticleSystem>().shape;
            shapeModule.rotation = new Vector3(0, dirPS, 0);

            // Establecer la velocidad del proyectil
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(shootDirection * projectileSpeed * Random.Range(0.7f, 1.3f), ForceMode2D.Impulse);
        }
    }
}
