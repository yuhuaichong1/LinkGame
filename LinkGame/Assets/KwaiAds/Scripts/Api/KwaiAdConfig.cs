namespace KwaiAds.Scripts.Api
{
    public class KwaiAdConfig
    {
        // 必填
        internal string AppId { get; }

        // 必填
        internal string Token { get; }

        // 选填
        internal string AppName { get; }

        // 选填
        internal string AppDomain { get; }

        // 选填
        internal string AppStoreUrl { get; }

        // 选填， 打印debug日志使用，注意上线前需要关闭
        internal bool DebugLog { get; }

        private KwaiAdConfig(KwaiAdConfig.Builder builder)
        {
            AppId = builder.AppId;
            Token = builder.Token;
            AppName = builder.AppName;
            AppDomain = builder.AppDomain;
            AppStoreUrl = builder.AppStoreUrl;
            DebugLog = builder.DebugLog;
        }

        public class Builder
        {
            internal string AppId;

            internal string Token;

            internal string AppName;

            internal string AppDomain;

            internal string AppStoreUrl;

            internal bool DebugLog;

            public Builder SetAppId(string appid)
            {
                this.AppId = appid;
                return this;
            }

            public Builder SetToken(string token)
            {
                this.Token = token;
                return this;
            }

            public Builder SetAppName(string appName)
            {
                this.AppName = appName;
                return this;
            }

            public Builder SetAppDomain(string appDomain)
            {
                this.AppDomain = appDomain;
                return this;
            }

            public Builder SetAppStoreUrl(string appStoreUrl)
            {
                this.AppStoreUrl = appStoreUrl;
                return this;
            }

            public Builder SetDebugLog(bool debugLog)
            {
                this.DebugLog = debugLog;
                return this;
            }

            public KwaiAdConfig Build()
            {
                return new KwaiAdConfig(this);
            }
        }
    }

}