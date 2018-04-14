using System.Collections.Generic;
using UnityEngine;

public class ScoreDestructableComponent : MonoBehaviour
{
    public DestructableType type;
    public enum DestructableType { Wall, Roof };

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Floor")
        {
            CanvasScore.instance.AddScore(PlayerScore.PlayerNr.P1, type);
            Destroy(gameObject);
        }
    }
}