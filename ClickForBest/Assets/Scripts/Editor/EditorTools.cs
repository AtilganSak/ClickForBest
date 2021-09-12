using UnityEditor;
public class EditorTools
{
    [MenuItem("Tools/Delete Database")]
    public static void DeleteDatabase()
    {
        EasyJson.DeleteDataBase();
    }
}