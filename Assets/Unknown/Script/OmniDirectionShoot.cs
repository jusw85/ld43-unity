using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniDirectionShoot : MonoBehaviour
{
    [Header("Projectile Settings")] public int numberOfProjectiles; //Number of projectiles to shoot.
    public float projectileSpeed; // Speed of projectile.
    public GameObject ProjectilePrefab; // Prefab to spawn.


    [Header("Private Variables")] private Vector3 startPoint; // Starting position of the bullet.
    private const float radius = 1F; // Help us find the move direction.

    public float coolDown = 5;
    public float coolDownTimer;

    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        //if (Input.GetKeyDown (KeyCode.Y))
        if (coolDownTimer == 0)
        {
            startPoint = transform.position;
            SpawnProjectile(numberOfProjectiles);
            coolDownTimer = coolDown;
        }
    }


    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= _numberOfProjectiles - 1; i++)
        {
            //Direction calculations
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity =
                new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);


            angle += angleStep;
        }
    }
}