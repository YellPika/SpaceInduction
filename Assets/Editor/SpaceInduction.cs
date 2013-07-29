using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class SpaceInduction
{
    [MenuItem("Space Induction/Create Level")]
    public static void CreateLevel()
    {
        var level = LoadPrefab("Levels/Level");

        CreateEntrance(level);
        CreateExit(level);

        var map = new GameObject("Map");
        map.transform.parent = level.transform;

        var rods = new GameObject("Rods");
        rods.transform.parent = level.transform;
    }

    private static void CreateEntrance(GameObject level)
    {
        var entrance = new GameObject("Entrance");
        entrance.transform.parent = level.transform;

        var number = LoadPrefab("Levels/Number");
        number.transform.parent = entrance.transform;

        var hall = LoadPrefab("Tiles/Hall");
        hall.transform.parent = entrance.transform;
        hall.AddComponent<SelfPowerSource>();
        hall.GetComponent<ColorProperty>().Value = Color.blue;
        hall.GetComponent<ColliderPowerSource>().enabled = true;

        var door = LoadPrefab("Tiles/Door");
        door.transform.parent = entrance.transform;
        door.transform.Translate(0, 0, 2);
        door.GetComponent<ColorProperty>().Value = Color.blue;

        var openTrigger = LoadPrefab("Triggers/Open Door Trigger");
        openTrigger.transform.parent = entrance.transform;
        openTrigger.GetComponent<OpenDoorTrigger>().Target = door.GetComponent<DoorBehaviour>();

        var closeTrigger = LoadPrefab("Triggers/Pass Through Door Trigger");
        closeTrigger.transform.parent = entrance.transform;
        closeTrigger.transform.Translate(0, 0, 2);
        closeTrigger.GetComponent<PassThroughDoorTrigger>().Target = door.GetComponent<DoorBehaviour>();

        var respawnPoint = LoadPrefab("Items/Respawn Point");
        respawnPoint.transform.parent = entrance.transform;
    }

    private static void CreateExit(GameObject level)
    {
        var exit = new GameObject("Exit");
        exit.transform.parent = level.transform;

        var door = LoadPrefab("Tiles/Door");
        door.transform.parent = exit.transform;
        door.transform.Translate(0, 0, 2);
        door.GetComponent<ColorProperty>().Value = Color.blue;

        var closeTrigger = LoadPrefab("Triggers/Pass Through Door Trigger");
        closeTrigger.transform.parent = exit.transform;
        closeTrigger.transform.Translate(0, 0, 2);
        closeTrigger.GetComponent<PassThroughDoorTrigger>().Target = door.GetComponent<DoorBehaviour>();
    }

    [MenuItem("Space Induction/Generate Level Prefab")]
    public static void GenerateLevelPrefab()
    {
        var sceneName = EditorApplication.currentScene;
        sceneName = Path.GetFileNameWithoutExtension(sceneName);

        var level = GameObject.Find(sceneName);
        if (level == null)
        {
            EditorUtility.DisplayDialog("Generate Level Prefab", "There is no game object named '" + sceneName + "'.", "OK");
            return;
        }

        var path = "Assets/Prefabs/Levels/" + level.name + ".prefab";

        var existing = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        if (existing == null)
            existing = PrefabUtility.CreateEmptyPrefab(path);
        
        PrefabUtility.ReplacePrefab(level, existing, ReplacePrefabOptions.ReplaceNameBased);
    }

    private static GameObject LoadPrefab(string name)
    {
        return PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + name + ".prefab", typeof(GameObject))) as GameObject;
    }
}
