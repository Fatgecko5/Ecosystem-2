using UnityEngine;

public class LivingEntity : MonoBehaviour {

    public int colourMaterialIndex;
    public Species species;
    public Material material;

    public Coord coord;
    //
    [HideInInspector]
    public int mapIndex;
    [HideInInspector]
    public Coord mapCoord;

    protected bool dead;

    // override these for each species
    protected float adultSize = 1F;
    protected float growthRate = 0.1F;

    protected float babySize {
        get {
            return adultSize / 10;
        }
    }


    public virtual void Init (Coord coord, Age age) {
        this.coord = coord;
        transform.position = Environment.tileCentres[coord.x, coord.y];
        if (age == Age.Adult) {
            transform.localScale = Vector3.one * (adultSize);
        }
        else {
            transform.localScale = Vector3.one * babySize;
        }
        // Set material to the instance material
        var meshRenderer = transform.GetComponentInChildren<MeshRenderer> ();
        for (int i = 0; i < meshRenderer.sharedMaterials.Length; i++)
        {
            if (meshRenderer.sharedMaterials[i] == material) {
                material = meshRenderer.materials[i];
                break;
            }
        }
    }

    protected virtual void Die (CauseOfDeath cause) {
        if (!dead) {
            dead = true;
            Debug.Log(species.ToString() + " died, " + cause.ToString());
            Environment.RegisterDeath (this);
            Destroy (gameObject);
        }
    }
}