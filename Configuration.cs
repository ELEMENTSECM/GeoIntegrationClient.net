namespace GeoIntegrationClient
{
	static class Configuration
	{
		// The following four values must be set
		public const string ClientName = "<Must be set>";
		public const string UserName = "<Must be set>";
		public const string Password = "<Must be set>";		
		private const string Database = "<Must be set>";


		public const string ArkivInnsynUrl = "http://www.ephorte.com/ephorte5GI-v6/" + Database + "/Services/GeoIntegration/V1.1/ArkivInnsynService.svc";
	}
}
