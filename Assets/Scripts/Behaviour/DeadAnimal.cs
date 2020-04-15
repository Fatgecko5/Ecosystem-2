using UnityEngine;

public class DeadAnimal : EdibleEntity {

    float rot;
    const float rotRate = 0.05f;
    Renderer myRenderer;
    Color origColor;
    public void Init(Animal animal) {
        this.coord = animal.coord;
        amountRemaining = animal.transform.localScale.z;
        species = Species.DeadBody;
        myRenderer = GetComponentInChildren<Renderer>();
        origColor = myRenderer.materials[2].color;
        rot = 0;
        Environment.RegisterNew(this);
        DestroyImmediate(animal);
    }

    void Update() {
        rot = Mathf.Min(1, rot + rotRate * Time.deltaTime);
        myRenderer.materials[2].color = Color.Lerp(origColor, Color.black, rot);
        if (rot >= 1) {
            Die(CauseOfDeath.Rotted);
        }
    }

}