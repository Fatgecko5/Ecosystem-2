using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal {
    public static readonly string[] GeneNames = { "A", "B" };

    public override void Init(Coord coord, Age age) {
        if (age == Age.Adult) {
            setGenes(Genes.DefaultRabbit());
        }
        Debug.Log(coord);
        base.Init(coord, age);
    }
}