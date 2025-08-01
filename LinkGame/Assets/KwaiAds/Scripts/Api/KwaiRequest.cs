using System;
using System.Collections.Generic;

namespace KwaiAds.Scripts.Api
{
    public class KwaiRequest
    {
        public readonly string TagId;

        public Dictionary<string, string> ExtParams = new Dictionary<string, string>();

        public KwaiRequest(string tagId)
        {
            this.TagId = tagId ?? throw new ArgumentNullException(nameof(tagId));
            InitExtParams();
        }

        protected void InitExtParams()
        {
            ExtParams[Constants.Request.BID_FLOOR_PRICE] = "0";
            ExtParams[Constants.Request.BID_FLOOR_CURRENCY] = Constants.Currency.USD;
            ExtParams[Constants.Request.MEDIATION_TYPE] = "4";
        }
    }
}