using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDelivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColour = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColour = new Color32(1, 1, 1, 1);
    [SerializeField] float delayUntilDestroyPackage = 1.0f;
    public bool hasPackage { get; private set; }
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hasPackage = false;

        EventManager.onStartNewLevel += ResetCarState;
        EventManager.onStartNewGame += ResetCarState;
    }

    void OnDestroy()
    {
        EventManager.onStartNewLevel -= ResetCarState;
        EventManager.onStartNewGame -= ResetCarState;
    }

    public void ResetCarState()
    {
        spriteRenderer.color = noPackageColour;
        hasPackage = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            hasPackage = true;
            spriteRenderer.color = hasPackageColour;

            Destroy(other.gameObject, delayUntilDestroyPackage);
        }
        if (other.tag == "Customer" && hasPackage)
        {
            ResetCarState();

            EventManager.Instance.packageDelivered();
        }
    }
}
