namespace XrCode
{
    //主场景
    public class MainScene : BaseScene
    {
        //protected BaseActor hero;
        /// 主场景进入
        protected override void OnLoad()
        {
            //UIManager.Instance.OpenAsync<UIMainCity>(EUIType.EUIMainCity);
            if (!GameDefines.ifIAA)
                UIManager.Instance.OpenAsync<UIGamePlay>(EUIType.EUIGamePlay, _ => { });
            else
                UIManager.Instance.OpenAsync<UIGamePlayBy>(EUIType.EUIGamePlayBy, _ => { });
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}