using UnityEngine;

public class Ammo : MonoBehaviour
{
    [field: SerializeField] public int Ammunition { get; private set; } = 30;
    [field: SerializeField] public int TypeAmmo { get; private set; } = 0;
}
