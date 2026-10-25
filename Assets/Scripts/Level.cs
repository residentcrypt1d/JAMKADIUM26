using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public GameObject levelPrefab;
    public int threeStarThreshold;
    public int twoStarThreshold;
    public int oneStarThreshold;
}
