using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerObject : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CameraService.instance.Initialize(this.transform);
    }
    private void FixedUpdate()
    {
        Vector2 input = InputService.Instance.ConsumeMove();
        rb2d.MovePosition(rb2d.position + speed * input * Time.fixedDeltaTime);
    }
}
