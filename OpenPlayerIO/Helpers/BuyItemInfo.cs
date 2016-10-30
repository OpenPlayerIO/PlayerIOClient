using ProtoBuf;

namespace PlayerIOClient
{
    [ProtoContract]
    public class BuyItemInfo : Messages.PayVault.PayVaultBuyItemInfo
    {
        public BuyItemInfo(string itemKey)
        {
            this.ItemKey = itemKey;
        }
    }
}