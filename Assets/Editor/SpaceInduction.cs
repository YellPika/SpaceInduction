using UnityEditor;
using UnityEngine;

public static class SpaceInduction
{
    [MenuItem("Space Induction/Create Level")]
    public static void CreateLevel()
    {
        var level = LoadPrefab("Levels/Level");
        var number = LoadPrefab("Levels/Number");
        var hall = LoadPrefab("Tiles/Hall");
        var door = LoadPrefab("Tiles/Door");
        var openTrigger = LoadPrefab("Triggers/Open Door Trigger");
        var closeTrigger = LoadPrefab("Triggers/Pass Through Door Trigger");
        var respawnPoint = LoadPrefab("Items/Respawn Point");

        var entrance = new GameObject("Entrance");
        entrance.transform.parent = level.transform;

        number.transform.parent = entrance.transform;
        hall.transform.parent = entrance.transform;
        door.transform.parent = entrance.transform;
        openTrigger.transform.parent = entrance.transform;
        closeTrigger.transform.parent = entrance.transform;
        respawnPoint.transform.parent = entrance.transform;

        openTrigger.GetComponent<OpenDoorTrigger>().Target = door.GetComponent<DoorBehaviour>();
        closeTrigger.GetComponent<PassThroughDoorTrigger>().Target = door.GetComponent<DoorBehaviour>();

        door.transform.Translate(0, 0, 2);
        closeTrigger.transform.Translate(0, 0, 2);
    }

    private static GameObject LoadPrefab(string name)
    {
        return PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + name + ".prefab", typeof(GameObject))) as GameObject;
    }
}
