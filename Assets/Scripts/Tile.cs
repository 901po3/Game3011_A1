using UnityEngine;

public class Tile : MonoBehaviour
{
    public const float MINIMAL_VALUE = 100;
    public enum Type {Maximum, Half, Quarter, Minimal };
    Type type;
    [SerializeField] Material[] materials;
    public Vector2 point;

    public float value;

    public void SetUpType(int x, int y)
    {
        point.x = x;
        point.y = y;
        value = MINIMAL_VALUE;
        gameObject.GetComponent<Renderer>().material = materials[3];
    }

    public void SetValueByType(float newValue)
    {
        switch(type)
        {
            case Type.Maximum:
                if(value < newValue)
                {
                    value = newValue;
                    gameObject.GetComponent<Renderer>().material = materials[0];
                }
                break;
            case Type.Half:
                if (value < newValue / 2)
                {
                    value = (int)newValue / 2;
                    gameObject.GetComponent<Renderer>().material = materials[1];
                }
                break;
            case Type.Quarter:
                if (value < newValue / 4)
                {
                    value = (int)newValue / 4;
                    gameObject.GetComponent<Renderer>().material = materials[2];
                }
                break;
            case Type.Minimal:
                if(value < newValue / 10)
                {
                    value = (int)newValue / 16;
                    gameObject.GetComponent<Renderer>().material = materials[3];
                }
                break;
            default:
                break;
        }
    }

    public void UpdateTypeChange(Type newType, float newValue)
    {
        type = newType;
        SetValueByType(newValue);
    }
}
