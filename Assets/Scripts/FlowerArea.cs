using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a collection of flower plants and attached flowers
/// </summary>
public class FlowerArea : MonoBehaviour
{
    // the diameter of the area where the agent and flowers can be
    // used for observing relative distance from agent to flower
    public const float AreaDiameter = 20f;

    //list of all flower plants in the flower area (flower plants have multiple flowers)
    private List<GameObject> flowerPlants;

    // lookup dictionary for looking up a flower from a nectar collider
    private Dictionary<Collider, Flower> nectarFlowerDictionary;

    /// <summary>
    /// List of all flowers in the flower area
    /// </summary>
    public List<Flower> Flowers { get; private set; }

    /// <summary>
    /// Reset flowers and flower plants
    /// </summary>
    public void ResetFlowers()
    {
        foreach(GameObject flowerPlant in flowerPlants)
        {
            float xRotation = UnityEngine.Random.Range(-5f, 5f);
            float yRotation = UnityEngine.Random.Range(-180f, 180f);
            float zRotation = UnityEngine.Random.Range(-5f, 5f);
            flowerPlant.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        foreach(Flower flower in Flowers)
        {
            flower.ResetFlower();
        }
    }

    /// <summary>
    /// Gets the <see cref="Flower"/> that a nectar collider belongs to
    /// </summary>
    /// <param name="collider">The nectar collider</param>
    /// <returns></returns>
    public Flower GetFlowerFromNectar(Collider collider)
    {
        return nectarFlowerDictionary[collider];
    }

    /// <summary>
    /// Called when the area wakes up
    /// </summary>
    private void Awake()
    {
        flowerPlants = new List<GameObject>();
        nectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>();
    }

    private void Start()
    {
        FindChildFlowers(transform);
    }

    /// <summary>
    /// Recursively finds all flowers and flower plants that are children of a parent transform
    /// </summary>
    /// <param name="parent">The parent of the children to check</param>
    private void FindChildFlowers(Transform parent)
    {
        for ( int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag("flower_plant"))
            {
                flowerPlants.Add(child.gameObject);

                FindChildFlowers(child);
            }
            else
            {
                Flower flower = child.GetComponent<Flower>();
                if (flower != null)
                {
                    // Found a flower, add it to the Flowers list
                    Flowers.Add(flower);

                    // Add the nectar collider to lookup dictionary
                    nectarFlowerDictionary.Add(flower.nectarCollider, flower);
                    
                    //there are no flowers that are children of other flowers
                }
                else
                {
                    // Flower component not found, so check children
                    FindChildFlowers(child);
                }
            }
        }
    }
}
