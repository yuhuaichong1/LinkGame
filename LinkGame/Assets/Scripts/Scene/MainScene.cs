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
            if (Game.Instance.IsAb)
            {
                UIManager.Instance.OpenAsync<UIGamePlay>(EUIType.EUIGamePlay, (BaseUI) =>
                {

                });
            }
            else
            {

                UIManager.Instance.OpenAsync<UIGamePlayBy>(EUIType.EUIGamePlayBy, (BaseUI) =>
                {

                });
            }

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