using UnityEngine;

public class Translator : MonoBehaviour
{
   public int Speed = 1;
   public Vector2 Direction;
   [SerializeField] private Rigidbody2D _rb;
   
   
   private void Start()
   {
      Move();
   }

   public void Move()
   {
      _rb.velocity = Direction * Speed;
   }
}
