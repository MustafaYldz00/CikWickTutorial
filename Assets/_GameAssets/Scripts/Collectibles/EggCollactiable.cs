using UnityEngine;

public class EggCollactiable : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
