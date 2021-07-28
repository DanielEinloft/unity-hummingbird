using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Manages a single flower with nectar 
/// </summary>
public class Flower : MonoBehaviour
{
    [Tooltip("The color when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, 0.3f);

    [Tooltip("The color when the flower is empty")]
    public Color emptyFlowerColor = new Color(0.5f, 0f, 1f);

    /// <summary>
    /// The trigger collider representing the nectar
    /// </summary>
    [HideInInspector]
    public Collider nectarCollider;

    // The solide collider representing the flower petals
    private Collider flowerCollider;

    // The flower's material
    private Material flowerMaterial;

    /// <summary>
    /// Vector pointing straight out of the flower
    /// </summary>
    /// <returns></returns>
    public Vector3 FlowerUpVector { get { return nectarCollider.transform.up; } }

    /// <summary>
    /// Center position of the nectar collider
    /// </summary>
    public Vector3 FlowerCenterPosition { get { return nectarCollider.transform.position; } }

    /// <summary>
    /// Amount of nectar remaining in the flower
    /// </summary>
    public float NectarAmount { get; private set; }

    /// <summary>
    /// Whether the flower has any nectar remaining
    /// </summary>
    public bool HasNectar { get { return NectarAmount > 0f; } }

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();
    }

    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount">The amount of nectar to remove</param>
    /// <returns>The actual amount successfuly removed</returns>
    public float Feed(float amount)
    {
        float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);
        NectarAmount -= amount;

        if(NectarAmount <= 0)
        {
            NectarAmount = 0;

            flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);
        }
        return nectarTaken;
    }

    /// <summary>
    /// Resets the flower
    /// </summary>
    public void ResetFlower()
    {
        NectarAmount = 1f;
        flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);

        flowerMaterial.SetColor("_BaseColor", fullFlowerColor);
    }
}
