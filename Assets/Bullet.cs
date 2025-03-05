using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPoolManager poolManager;

    public void Initialize(BulletPoolManager manager)
    {
        poolManager = manager;
    }


}
