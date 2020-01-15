using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 originPos;
    float maxDis = 5.0f;
    [SerializeField] float speed;

    private void Start()
    {
        originPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            if(transform.position.x < originPos.x + maxDis)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, originPos.y, transform.position.z);
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > originPos.x - maxDis)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, originPos.y, transform.position.z);
            }
        }
        else if(Input.GetKey(KeyCode.W))
        {
            if (transform.position.z < originPos.z + maxDis)
            {
                transform.position = new Vector3(transform.position.x, originPos.y, transform.position.z + speed * Time.deltaTime);
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (transform.position.z > originPos.z - maxDis)
            {
                transform.position = new Vector3(transform.position.x, originPos.y, transform.position.z - speed * Time.deltaTime);
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, originPos.x - maxDis, originPos.x + maxDis), originPos.y,
            Mathf.Clamp(transform.position.z, originPos.z - maxDis, originPos.z + maxDis));
    }

}
