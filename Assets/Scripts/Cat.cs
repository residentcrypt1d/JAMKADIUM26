using UnityEngine;

public class Cat : MonoBehaviour
{
    public Vector3Int gridPos;

    public bool CanMove(Vector3Int targPos){
        return true;
    }
}