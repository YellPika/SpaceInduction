using UnityEngine;
using System.Collections;

public class PlayerEditor : MonoBehaviour {
	public static string name = "";
    private string message = "";
	public string level = "Level";
	
    private bool register = false;
	void Start ()
	{
		
	}
	
	private void OnGUI()
	{
	if (message != "" && !register)
            GUILayout.Box(message);

        
            
            GUILayout.Label("Name");
            name = GUILayout.TextField(name);
            

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Back"))
                register = false;

            if (GUILayout.Button("Register"))
            {
                message = "";

                if (name == "")
                    message += "Please enter all the fields \n";
                else
                {
                   register = true;
                }
            }

            GUILayout.EndHorizontal();
	
	if (register)
		{
			GUILayout.Box (message);
			GUILayout.Label ("Levels: ");
			GUILayout.BeginHorizontal();
			for (int i = 1; i < 10; i ++)
			{
				Debug.Log (level + " " + i.ToString());
				PlayerPrefs.SetString(level, level + " " + i.ToString());
				
				
				Debug.Log (PlayerPrefs.GetString (level));
				if (GUILayout.Button(level + " " + i.ToString()))
            	{
					PlayerPrefs.SetString ("levelChoice", level + " " + i.ToString());
					Application.LoadLevel ("Game");
				}
					
			}
			GUILayout.EndHorizontal();
			
			
		}
	
	}
	
}
