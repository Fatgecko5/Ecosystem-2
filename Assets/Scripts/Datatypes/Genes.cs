using static System.Math;

public class Genes {

    private enum type {
        speed,
        size,
    }

    const float mutationChance = .2f;
    const float maxMutationAmount = .3f;
    static readonly System.Random prng = new System.Random ();

    public readonly bool isMale;
    private float[] values;

    public Genes (float[] values) {
        isMale = RandomValue () < 0.5f;
        this.values = values;
    }

    public float size {
        get {
            return values[(int)type.size];
        }
    }

    public float speed {
        get {
            return values[(int)type.speed];
        }
    }

    public static Genes RandomGenes() {
        int num = type.GetNames(typeof(type)).Length;
        float[] values = new float[num];
        for (int i = 0; i < num; i++) {
            values[i] = RandomValue ();
        }
        return new Genes (values);
    }

    public static Genes InheritedGenes (Genes mother, Genes father) {
        float[] values = new float[type.GetNames(typeof(type)).Length];
        for (int i = 0; i < values.Length; i++) {
            float inherit = (RandomValue () < 0.5f) ? mother.values[i] : father.values[i];
            bool willMutate = RandomValue () < mutationChance;
            if (willMutate) {
                float mutation = (float)prng.NextDouble() * (maxMutationAmount - maxMutationAmount * 2) + maxMutationAmount*-2;
                inherit += mutation;
            }
            values[i] = inherit;
        }
        Genes genes = new Genes (values);
        return genes;
    }

    public static Genes DefaultRabbit() {
        var values = new float[type.GetNames(typeof(type)).Length];
        values[(int)type.size] = 1f;
        values[(int)type.speed] = 2f;
        return new Genes(values);
    }

    static float RandomValue () {
        return (float) prng.NextDouble ();
    }

    static float RandomGaussian () {
        double u1 = 1 - prng.NextDouble ();
        double u2 = 1 - prng.NextDouble ();
        double randStdNormal = Sqrt (-2 * Log (u1)) * Sin (2 * PI * u2);
        return (float) randStdNormal;
    }
}