using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed = 50;
    public Vector3 Direction;
    
    
    private void Update()
    {
        transform.Rotate(Direction * Speed * Time.deltaTime);
    }
}
