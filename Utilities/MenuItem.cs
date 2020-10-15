[System.AttributeUsage(System.AttributeTargets.Method)
]
public class MenuItem : System.Attribute
{
    private string name;

    public MenuItem(string name)
    {
        this.name = name;
    }
    public string GetName()
    {
        return name;
    }
}