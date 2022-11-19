using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        Debug.DrawRay(firePoint.position, firePoint.up * -1 * 5000, Color.green);
    }
    
    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Laser shoot");
        
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up * -1);
        
        if (hitInfo)
        {
            // On hit.
            
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
            else
            {
                FindObjectOfType<Score>().DecreaseScore();
            }
        }
        else
        {
            FindObjectOfType<Score>().DecreaseScore();
        }
    }
}