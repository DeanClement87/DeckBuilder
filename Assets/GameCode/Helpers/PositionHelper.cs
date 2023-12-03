using UnityEngine;

public static class PositionHelper 
{
    private static int bottomY = 540;
    private static int leftX = 960; 
    public static void ChangePositionY(string gameobjectName, int y)
    {
        var gameObject = GameObject.Find(gameobjectName);
        var newVector = new Vector3(gameObject.transform.position.x, bottomY + y, 0);
        gameObject.transform.position = newVector;
    }

    public static void ChangePositionX(string gameobjectName, int x)
    {
        var gameObject = GameObject.Find(gameobjectName);
        var newVector = new Vector3(leftX + x, gameObject.transform.position.y, 0);
        gameObject.transform.position = newVector;
    }

    public static void ChangePositionXY(string gameobjectName, int x, int y)
    {
        var gameObject = GameObject.Find(gameobjectName);
        var newVector = new Vector3(leftX + x, bottomY + y, 0);
        gameObject.transform.position = newVector;
    }

    public static void ChangePositionY(GameObject gameObject, int y)
    {
        var newVector = new Vector3(gameObject.transform.position.x, y + bottomY, 0);
        gameObject.transform.position = newVector;
    }

    public static void ChangePositionX(GameObject gameObject, int x)
    {
        var newVector = new Vector3(leftX + x, gameObject.transform.position.y, 0);
        gameObject.transform.position = newVector;
    }

    public static void ChangePositionXY(GameObject gameObject, int x, int y)
    {
        var newVector = new Vector3(leftX + x, bottomY + y, 0);
        gameObject.transform.position = newVector;
    }
}
