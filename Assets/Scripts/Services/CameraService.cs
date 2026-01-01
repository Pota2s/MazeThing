using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Transform cameraTarget;
    public static CameraService instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Transform newTarget) 
    {
        transform.position = newTarget.position;
        cameraTarget = newTarget;
    }

    void Update()
    {
        if (cameraTarget == null)
        {
            return;
        }

        float newX = Mathf.Lerp(_camera.transform.position.x, cameraTarget.position.x, 0.1f);
        float newY = Mathf.Lerp(_camera.transform.position.y, cameraTarget.position.y, 0.1f);

        _camera.transform.position = new Vector3(newX, newY, _camera.transform.position.z);
    }
}
