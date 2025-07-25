using System;

public static class FacadeGuide
{
    public static Action PlayGuide;
    public static Action NextStep;
    public static Action NextStepHead;
    public static Func<GuideItem> GetCurGuideItems;
    public static Func<int> GetCurStep;
    public static Action CloseGuide;
    public static Func<bool> GetWithdrawableUIcheck;
    public static Action<bool> SetWithdrawableUIcheck;
    public static Action CheckWithdrawableUI;
}
