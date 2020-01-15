using UnityEngine;

public class Tile : MonoBehaviour
{
    const float MINIMAL_VALUE = 200;
    public enum Type {Maximum, Half, Quarter, Minimal };
    Type type = Type.Minimal;
    [SerializeField] Material[] materials;

    public float value;

    private void Start()
    {
        SetValueByType(MINIMAL_VALUE);
    }

    public void TypeChanger(Type newtype)
    {
        type = newtype;
    }

    public void SetValueByType(float newValue)
    {
        switch(type)
        {
            case Type.Maximum:
                value = newValue;
                gameObject.GetComponent<Renderer>().material = materials[0];
                break;
            case Type.Half:
                value = newValue / 2;
                gameObject.GetComponent<Renderer>().material = materials[1];
                break;
            case Type.Quarter:
                value = newValue / 4;
                gameObject.GetComponent<Renderer>().material = materials[2];
                break;
            case Type.Minimal:
                value = MINIMAL_VALUE;
                gameObject.GetComponent<Renderer>().material = materials[3];
                break;
            default:
                break;
        }
    }
}
