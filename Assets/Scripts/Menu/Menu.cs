using System;
using System.Linq;
using UnityEngine;

public sealed class Menu : MonoBehaviour
{
    private bool isEnabled = true;

    private int selectedItem;
    private int selectedLevel;
    private Level[] levels;

    private PlayerBehaviour player;

    private void Awake()
    {
        levels = FindSceneObjectsOfType(typeof(Level))
            .OfType<Level>()
            .OrderBy(n => n.Number)
            .ToArray();

        player = transform.parent.GetComponentInChildren<PlayerBehaviour>();
    }

    private void Start()
    {
        Enable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isEnabled)
                Disable();
            else
                Enable();
        }

        if (!isEnabled)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            selectedItem -= 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            selectedItem += 1;

        while (selectedItem < 0)
            selectedItem += 3;
        while (selectedItem >= 3)
            selectedItem -= 3;

        if (selectedItem == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                selectedLevel--;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                selectedLevel++;

            while (selectedLevel < 0)
                selectedLevel += levels.Length;
            while (selectedLevel >= levels.Length)
                selectedLevel -= levels.Length;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (selectedItem)
            {
                case 0:
                    Disable();
                    break;
                case 1:
                    Disable();

                    player.Teleport(
                        levels[selectedLevel].transform.position,
                        levels[selectedLevel].transform.rotation);
                    player.BroadcastMessage("Restart", SendMessageOptions.DontRequireReceiver);

                    foreach (var level in levels)
                        level.Restart();

                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
    }

    private void OnGUI()
    {
        if (!isEnabled)
            return;

        GUI.Label(new Rect(50, Screen.height / 2 - 30, Screen.width, 20), "Play");
        GUI.Label(new Rect(50, Screen.height / 2 - 10, Screen.width, 20), "Level: " + levels[selectedLevel].name);
        GUI.Label(new Rect(50, Screen.height / 2 + 10, Screen.width, 20), "Quit");

        GUI.Box(new Rect(0, Screen.height / 2 - 30 + selectedItem * 20, Screen.width, 20), "");
    }
    
    public void Enable()
    {
        isEnabled = true;
        selectedItem = 0;
        selectedLevel = Math.Max(0, Array.IndexOf(levels, player.GetComponent<LevelInventory>().Current));

        Time.timeScale = 0;

        foreach (var child in transform.OfType<Transform>())
            child.BroadcastMessage("Enable");
    }

    public void Disable()
    {
        isEnabled = false;
        Time.timeScale = 1;

        foreach (var child in transform.OfType<Transform>())
            child.BroadcastMessage("Disable");
    }
}
