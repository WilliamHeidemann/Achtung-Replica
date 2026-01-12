using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3[] To3D(this List<Vector2> points)
    {
        var array = new Vector3[points.Count];
        for (var i = 0; i < points.Count; i++)
        {
            var vector2 = points[i];
            array[i] = new Vector3(vector2.x, vector2.y, 0);
        }
        return array;
    }
}