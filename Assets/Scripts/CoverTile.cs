using UnityEngine;

public class CoverTile : MonoBehaviour
{
    public Vector2 point;
    public bool isDetected = false;

    public void Clicked()
    {
        if(!isDetected)
        {
            isDetected = true;
            transform.position = new Vector3(1000, -1000, 1000);
        }
    }
}
