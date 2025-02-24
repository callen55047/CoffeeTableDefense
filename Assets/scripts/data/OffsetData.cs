using Unity;

[System.Serializable]
public class OffsetData
{
    public float rotation;
    public float height;
    public float scale;

    public OffsetData(float rotation = 0f, float height = 0f, float scale = 0f)
    {
        this.rotation = rotation;
        this.height = height;
        this.scale = scale;
    }
}