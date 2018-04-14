using System.Collections.Generic;
using UnityEngine;

public class ScoreDestructableComponent : MonoBehaviour
{
    PlayerScore.PlayerNr owner;
    public DestructableType type;
    public enum DestructableType { Wall, Roof };

    private void Start()
    {
        Transform tr = transform.parent;
        while(tr != null)
        {
            var ownerComponent = tr.GetComponent<HouseComponent>();
            if (ownerComponent != null)
            {
                owner = ownerComponent.houseOwner;
                break;
            }
            else
            {
                tr = tr.parent;
            }
        }
        CanvasScore.instance.Register(owner, type);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Floor")
        {
            CanvasScore.instance.AddScore(owner, type);
            Destroy(this);
        }
    }
}