using Gameplay;
using UnityEditor;
using UnityEngine;

public class CreateZombiePoints : MonoBehaviour {
    public Map map;
    public Transform parentZombiePoints;
}

 [CustomEditor (typeof(CreateZombiePoints))]
 public class CreatePoints: Editor {
     
     private CreateZombiePoints createZombiePoints;
     public override void OnInspectorGUI() {
         DrawDefaultInspector();
         createZombiePoints = (CreateZombiePoints)target;

         if (GUILayout.Button("Create")) {
             Create();
         }
     }

     private void Create() {
         int countChild = createZombiePoints.parentZombiePoints.childCount;
         createZombiePoints.map.zombieSpawnPoints = new Transform[countChild];
         for (int i = 0; i < countChild; i++) {
             createZombiePoints.map.zombieSpawnPoints[i] = createZombiePoints.parentZombiePoints.GetChild(i);
         }
     }
 }

// using UnityEditor;
// using UnityEngine;
// using System.Collections;

// Example script with properties.
// public class MyPlayerAlternative : MonoBehaviour
// {
//  public int damage;
//  public int armor;
//  public GameObject gun;
//
//  // ...other code...
// }

// Custom Editor the "old" way by modifying the script variables directly.
// No handling of multi-object editing, undo, and Prefab overrides!
//[CustomEditor (typeof(MyPlayerAlternative))]
// public class MyPlayerEditorAlternative : Editor
// {

//  public override void OnInspectorGUI()
//  {
//   MyPlayerAlternative mp = (MyPlayerAlternative)target;
//
//   mp.damage = EditorGUILayout.IntSlider ("Damage", mp.damage, 0, 100);
//   ProgressBar (mp.damage / 100.0f, "Damage");
//
//   mp.armor = EditorGUILayout.IntSlider ("Armor", mp.armor, 0, 100);
//   ProgressBar (mp.armor / 100.0f, "Armor");
//
//   bool allowSceneObjects = !EditorUtility.IsPersistent (target);
//   mp.gun = (GameObject)EditorGUILayout.ObjectField ("Gun Object", mp.gun, typeof(GameObject), allowSceneObjects);
//   
//   if (GUILayout.Button("Your ButtonText"))
//          {
//              //add everthing the button would do.
//          }
//  }
//
//  // Custom GUILayout progress bar.
//  void ProgressBar (float value, string label)
//  {
//   // Get a rect for the progress bar using the same margins as a textfield:
//   Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
//   EditorGUI.ProgressBar (rect, value, label);
//   EditorGUILayout.Space ();
//  }
// }