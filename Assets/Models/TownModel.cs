public class TownModel
{
    public int BaseHealth { get; set; } = 40;
    public int Health { get; set; }
    public int Mood { get; set; }

    public int Fear()
    {
        if (Mood < 0)
            return -Mood;

        return 0;
    }

    public int Hope()
    {
        if (Mood > 0)
            return Mood;

        return 0;
    }
}
