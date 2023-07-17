using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, MiniMap
{

    public float Move_Speed;

    public float Rotate_Speed;


    float angle;
    public float Angle { get => angle; set => angle = value; }

    [SerializeField]
    Sprite image;
    public Sprite Image { get => image; set => image = value; }



    private void OnEnable()
    {
        GameManager.Instance.Entities.Add(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.Entities.Remove(this);
    }
}

public interface Injured
{
    public void Injured();
}

public interface Attack
{
    public void Attack();
}

public interface Skill
{
    public void Skill();
}

public interface MiniMap
{
    public float Angle { get; set; }

    public Sprite Image { get; set; }

}