using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject WeaponHand;

    [field: SerializeField] public float Damage { get; private set; } = 10;
    [field: SerializeField] public int TypeAmmo { get; private set; }
    [field: SerializeField] public int CurrentAmmo { get; set; }
    [field: SerializeField] public int MaxAmmo { get; set; }
    [field: SerializeField] public float TimeShot { get; private set; }
    [field: SerializeField] public float TimeReload { get; private set; }
    [field: SerializeField] public bool IsSingleShot { get; set; }

    private void Awake()
    {
        WeaponHand = GameObject.Find("WeaponHand");
    }

    public void PickUp()
    {
        gameObject.transform.SetParent(WeaponHand.transform, true);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }

    public void PickOff()
    {
        gameObject.transform.SetParent(null, true);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1, gameObject.transform.position.z);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
