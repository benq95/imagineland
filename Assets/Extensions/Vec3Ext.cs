using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vec3Ext
{
    public static Vector2 ToVec2(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }
}
