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
                return;
            }
            else
            {
                tr = tr.parent;
            }
        }
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