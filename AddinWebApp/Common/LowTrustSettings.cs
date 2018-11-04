namespace AddinWebApp.Common
{
	public class LowTrustSettings
	{
		public string ClientSecret { get; set; }
		public string Realm { get; set; }
		public string HostedAppHostNameOverride { get; set; }
		public string HostedAppHostName { get; set; }
		public string SecondaryClientSecret { get; set; }
		public string ClientId { get; set; }
	}
}
