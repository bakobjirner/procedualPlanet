public class NoiseSettings
{
    public NoiseSettings(float frequency, float strength, float threshold)
    {
        this.frequency = frequency;
        this.strength = strength;
        this.threshold = threshold;
    }

    //frequency of the noise
    public float frequency = 1;
    //strength of the noise
    public float strength = 1;
    //value of the previous noise that has to be present for this noise to take effect, so for example mountains dont get generated under water
    public float threshold = 0;
}
