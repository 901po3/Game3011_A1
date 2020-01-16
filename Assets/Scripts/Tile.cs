using UnityEngine;

public class Tile : MonoBehaviour
{
    public const float MINIMAL_VALUE = 100;
    public enum Type {Maximum, Half, Quarter, Minimal };
    public Type type;
    public Material[] materials;
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
                    value = newValue / 2;
                    gameObject.GetComponent<Renderer>().material = materials[1];
                }
                break;
            case Type.Quarter:
                if (value < newValue / 4)
                {
                    value = newValue / 4;
                    gameObject.GetComponent<Renderer>().material = materials[2];
                }
                break;
            case Type.Minimal:
                if(value < newValue / 8)
                {
                    value = newValue / 16;
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

    public void DecreaseLevel()
    {
        if(type != Type.Minimal)
        {
            type += 1;
            value /= 2;
            GetComponent<Renderer>().material = materials[(int)type];
        }
    }

    public void ChangeValue(Type prevType)
    {
        switch(prevType)
        {
            case Type.Maximum:
                value = value / 8;
                break;
            case Type.Half:
                value = value / 4;
                break;
            case Type.Quarter:
                value = value / 2;
                break;
            default:
                break;
        }

    }
}
