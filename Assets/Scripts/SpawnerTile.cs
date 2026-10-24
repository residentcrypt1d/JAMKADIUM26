using UnityEngine;

public class SpawnerTile : WalkableTile
{
    //public enum CatType {CharcoalCat, InkCat, StampCat, Size};
    //[SerializeField] CatType spawnType;
    [SerializeField] GameObject catPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject catInstance = Instantiate(catPrefab, transform.position, Quaternion.identity);
        
        Cat catComponent = catInstance.GetComponent<Cat>();
        /*switch(spawnType)
        {
            case CatType.CharcoalCat:
                catComponent = catInstance.AddComponent<CharcoalCat>();
                break;
            case CatType.InkCat:
                catComponent = catInstance.AddComponent<InkCat>();
                break;
            case CatType.StampCat:
                catComponent = catInstance.AddComponent<StampCat>();
                break;
            default:
                Debug.LogError("Unrecognized cat type.");
                return;
        }*/
        occupants.Add(catComponent);
        catComponent.currTile = this;
        catComponent.gridPos = gridPosition;
    }
}
