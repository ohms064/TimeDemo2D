using UnityEngine;
using System.Collections;

public static class VectorUtils {

    public static Vector2 Transpose( this Vector2 v ) {
        float aux = v.x;
        v.x = v.y;
        v.y = aux;
        return v;
    }

    public static Vector2 CounterClockWisePerpendicular( this Vector2 v ) {
        float aux = v.x;
        v.x = -v.y;
        v.y = aux;
        return v;
    }

    public static Vector2 ClockWisePerpendicular( this Vector2 v ) {
        float aux = -v.x;
        v.x = v.y;
        v.y = aux;
        return v;
    }

}
