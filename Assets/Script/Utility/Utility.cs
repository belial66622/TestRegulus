
using UnityEngine;

public static class Utility
{
    public static int FPB(int a, int b)
    {
        while (b != 0)
        {
            int sisa = a % b;
            a = b;
            b = sisa;
        }
        return a;
    }

    public static bool DiffBetween(int a,int b, int difference)
    { 
        return Mathf.Abs(a-b) <= difference;
    }

    public static bool DiffBetween(float a, float b, int difference)
    {
        return Mathf.Abs(a - b) <= difference;
    }

    /// <summary>
    /// return step required
    /// </summary>
    /// <param name="ax">First tile row</param>
    /// <param name="ay">Selected tile row</param>
    /// <param name="bx">First tile column</param>
    /// <param name="by">Selected tile column</param>
    /// <returns>step required</returns>
    public static int Step(int ax, int ay , int bx , int by) 
    {
        return Mathf.Abs(ax - bx) + Mathf.Abs(ay - by);
    }

    /// <summary>
    /// return step required
    /// </summary>
    /// <param name="ax">First tile row</param>
    /// <param name="ay">Selected tile row</param>
    /// <param name="bx">First tile column</param>
    /// <param name="by">Selected tile column</param>
    /// <returns>step required</returns>
    public static float Step(float ax, float ay, float bx, float by)
    {
        return Mathf.Abs(ax - bx) + Mathf.Abs(ay - by);
    }

    public static int random(int min, int max)
    { 
        return Random.Range(min, max);
    }
}
