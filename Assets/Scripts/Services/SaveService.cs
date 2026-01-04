using UnityEngine;
using System.IO;

public static class SaveService
{
	
	private static string GetFilePath()
	{
        string path = Path.Combine(Application.persistentDataPath, "Saves");
        Directory.CreateDirectory(path);
        return path = Path.Combine(path, "save.json");
    }

	public static void SaveJSON(SaveData data)
	{
		if (data == null) return;

		string fileContent = JsonUtility.ToJson(data, prettyPrint : true);

		string path = GetFilePath();

		File.WriteAllText(path, fileContent);
	}

	public static SaveData LoadJSON()
	{
		string path = GetFilePath();
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			return JsonUtility.FromJson<SaveData>(json);
		} 
		else
		{
			return new SaveData(1);
		}
	}
}
