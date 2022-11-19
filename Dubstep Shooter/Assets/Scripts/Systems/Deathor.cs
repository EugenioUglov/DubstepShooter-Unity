using System.Collections;
using UnityEngine;

public class Deathor : MonoBehaviour
{
    [SerializeField] private float _dieTimeout = 3;
        
    void Start()
    {
        StartCoroutine(DestroyOnTimeout(_dieTimeout));
    }
    
    private IEnumerator DestroyOnTimeout(float timeToDestroy = 3)
    {
        float elapsedTime = 0;
        
        float startTime = Time.time;
        
        while (true)
        {
            elapsedTime = Time.time - startTime;
            
            if (elapsedTime >= timeToDestroy)
            {
                Destroy(gameObject);
            }
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
