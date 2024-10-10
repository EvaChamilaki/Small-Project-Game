using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D defaultCursor, clickCursor;

    public static ChangeCursor instance; //Singletons; only one instance of this class can exist and it can be accessed from anywhere

    private void Awake() //part of the singleton 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        DefaultCursor();
    }

    public void ClickableCursor()
    {
        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
    }

    public void DefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
