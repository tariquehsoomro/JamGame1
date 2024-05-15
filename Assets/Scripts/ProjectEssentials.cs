using UnityEngine;

public class ProjectEssentials : MonoBehaviour
{
    public int GetLayerNumber(LayerMask mask)
    {
        int bitmask = mask.value;
        int result = bitmask > 0 ? 0 : 31;

        while (bitmask > 1)
        {
            bitmask = bitmask >> 1;
            result++;
        }

        return result;
    }
}
