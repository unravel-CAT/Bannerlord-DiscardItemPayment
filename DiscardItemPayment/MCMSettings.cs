using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem.Extensions;

namespace DiscardItemPayment
{
    public class MCMSettings : AttributeGlobalSettings<MCMSettings>
    {
        public override string Id 
        {
            get
            {
                return "DiscardItemPayment_v1";
            }
        }

        public override string DisplayName
        {
            get
            {
                return "Discard Item Payment";
            }
        }

        public override string FolderName
        {
            get
            {
                return "DIP";
            }
        }

        public override string FormatType
        {
            get
            {
                return "json2";
            }
        }

        [SettingPropertyFloatingInteger("Discard Item Price Multiplier", 0f, 10f, "#0%", HintText = "Default = 30%", Order = 1, RequireRestart = false)]
        public float DiscardItemPriceMultiplier { get; set; } = 0.3f;
    }
}
