using UnityEngine;

public static class VectorConvert
{
  public static Vector3 ConvertToVector3(float x, float y, float z)
  {
    return new Vector3(x, y, z);
  }
  
  public static Vector3 ConvertToVector3(float x, float y)
  {
    return new Vector3(x, 0f, y);
  }
  
  public static Vector2 ConvertToVector2(float x, float y)
  {
    return new Vector2(x, y);
  }
}
