using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public static void SetBorderWidth(this VisualElement element, int borderWidth)
    {
        element.style.borderBottomWidth = borderWidth;
        element.style.borderTopWidth = borderWidth;
        element.style.borderLeftWidth = borderWidth;
        element.style.borderRightWidth = borderWidth;
    }

    public static void SetBorderColor(this VisualElement element, Color color)
    {
        element.style.borderBottomColor = color;
        element.style.borderLeftColor = color;
        element.style.borderRightColor = color;
        element.style.borderTopColor = color;
    }
}