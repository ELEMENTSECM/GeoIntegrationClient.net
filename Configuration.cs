namespace GeoIntegrationClient
{
	static class Configuration
	{
		public const string ArkivInnsynUrl = BaseUrl + "ArkivInnsynService.svc";
		public const string SakFaserUrl = BaseUrl + "SakFaserService.svc";
		public const string BaseUrl = "https://test01.elementscloud.no/GeoIntegration/" + Database + "/Services/GeoIntegration/V1.1/";

		// The following four values must be set
		public const string ClientName = "<Must be set>";
		public const string UserName = "<Must be set>";
		public const string Password = "<Must be set>";
		private const string Database = "<Must be set>";
	}
}
