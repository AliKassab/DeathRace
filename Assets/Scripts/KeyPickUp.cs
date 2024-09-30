using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] private int keysNum;
    private Inv inv;

    private void Start()
    {
        inv = FindObjectOfType<Inv>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inv.keys += keysNum;
            Destroy(gameObject);
        }
    }
}
