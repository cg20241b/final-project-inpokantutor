public static class GlobalTimeSystem
{
    public static float TimeOfDay { get; private set; }
    
    public static void UpdateTime(float deltaTime)
    {
        TimeOfDay += deltaTime;
        TimeOfDay %= 24;
    }
}
