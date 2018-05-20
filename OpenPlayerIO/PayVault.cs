using System.Linq;
using System.Collections.Generic;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;
using PlayerIOClient.Messages.PayVault;

namespace PlayerIOClient
{
    public class PayVault
    {
        private readonly HttpChannel _channel;

        public PayVaultItem[] Items { get; set; }
        public string Version { get; set; }
        public int Coins { get; set; }

        internal PayVault(HttpChannel channel)
        {
            _channel = channel;
        }

        public PayVaultReadHistoryOutput ReadHistory(uint page, uint pageSize)
        {
            return _channel.Request<PayVaultReadHistoryArgs, PayVaultReadHistoryOutput, PlayerIOError>(160,
                new PayVaultReadHistoryArgs {
                    Page = page,
                    PageSize = pageSize
                });
        }

        public void Give(params PayVaultBuyItemInfo[] items)
        {
            _channel.Request<PayVaultGiveArgs, PayVaultGiveOutput, PlayerIOError>(178, new PayVaultGiveArgs {
                Items = items
            });
        }

        public void Credit(uint amount, string reason = "")
        {
            _channel.Request<PayVaultCreditArgs, PayVaultCreditOutput, PlayerIOError>(169,
                new PayVaultCreditArgs {
                    Amount = amount,
                    Reason = reason
                });
        }

        public void Debit(uint amount, string targetUserId, string reason = "")
        {
            _channel.Request<PayVaultCreditArgs, PayVaultCreditOutput, PlayerIOError>(172,
                new PayVaultCreditArgs {
                    Amount = amount,
                    Reason = reason
                });
        }

        public void Consume(params PayVaultItem[] items)
        {
            _channel.Request<PayVaultConsumeArgs, PayVaultConsumeOutput, PlayerIOError>(166,
                new PayVaultConsumeArgs {
                    Ids = (from item in items select item.Id).ToArray()
                });
        }

        public PayVaultRefreshOutput Refresh()
        {
            var output = _channel.Request<PayVaultRefreshArgs, PayVaultRefreshOutput, PlayerIOError>(163,
                new PayVaultRefreshArgs());

            Version = output.VaultContents.Version;
            Items = output.VaultContents.Items;
            Coins = output.VaultContents.Coins;

            return output;
        }

        public PayVaultPaymentInfoOutput GetBuyDirectInfo(string provider, Dictionary<string, string> purchaseArguments, params PayVaultBuyItemInfo[] items)
        {
            var output = _channel.Request<PayVaultPaymentInfoArgs, PayVaultPaymentInfoOutput, PlayerIOError>(181,
                new PayVaultPaymentInfoArgs {
                    Provider = provider,
                    Items = items,
                    PurchaseArguments = purchaseArguments.ToArray()
                });

            return output;
        }
    }
}