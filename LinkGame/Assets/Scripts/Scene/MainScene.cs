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
            UIManager.Instance.OpenAsync<UIGamePlay>(EUIType.EUIGamePlay, (BaseUI) =>
            {
                
            });
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