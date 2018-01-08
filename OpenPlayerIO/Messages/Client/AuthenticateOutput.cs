using OpenPIO.PlayerIOClient.Enums;
using PlayerIOClient.Helpers;
using ProtoBuf;
using System.Collections.Generic;

namespace PlayerIOClient.Messages.Client
{
	[ProtoContract]
	internal class AuthenticateOutput
	{
		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public string UserId { get; set; }

		[ProtoMember(3)]
		public bool ShowBranding { get; set; }

		[ProtoMember(4)]
		public string GameFSRedirectMap { get; set; }

		//[ProtoMember(5)]
		//public Message PlayerInsightState { get; set; }

		[ProtoMember(6)]
		public AuthenticateStartDialog[] StartDialogs { get; set; }

		[ProtoMember(7)]
		public bool IsSocialNetworkUser { get; set; }

		[ProtoMember(8)]
		public string NewPlayCodes { get; set; }

		[ProtoMember(9)]
		public string NotificationClickPayload { get; set; }

		[ProtoMember(10)]
		public bool IsInstalledByPublishingNetwork { get; set; }

		[ProtoMember(11)]
		public string Deprecated1 { get; set; }

		[ProtoMember(12)]
		public ServerAPISecurity ApiSecurity { get; set; }

		[ProtoMember(13)]
		public List<string> ApiServerHosts { get; set; }
	}
}
