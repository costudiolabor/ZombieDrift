using System;
 
[Serializable]
public class Stage  {
   public Map[] maps;
   public int count => maps.Length;
}
