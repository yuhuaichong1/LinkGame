using System;

public static class FacadeGuide
{
    public static Action<int> PlayGuide;
    public static Action<bool> NextStep;
    public static Action<int> SetGuide;
    public static Func<GuideItem> GetCurGuideItems;
}
