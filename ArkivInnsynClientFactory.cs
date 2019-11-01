using ServiceReference;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GeoIntegrationClient
{
	public class ArkivInnsynClientFactory
	{
		public static ArkivInnsynPortClient CreateArkivInnsynPortClient()
		{
			var client = new ArkivInnsynPortClient
			{
				Endpoint =
				  {
					Address = new EndpointAddress(new Uri(Configuration.ArkivInnsynUrl)),
					Binding = GetBinding(),
				  }
			};
			client.Endpoint.EndpointBehaviors.Add(new AuthenticationHeaderBehavior(Configuration.UserName, Configuration.Password));
			return client;
		}

		private static Binding GetBinding()
		{
			bool https = Configuration.ArkivInnsynUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase);
			if (https)
			{
				throw new NotSupportedException();
			}
			else
			{
				return new BasicHttpBinding
				{
					Namespace = "http://rep.geointegrasjon.no/Arkiv/Oppdatering/xml.wsdl/2012.01.31",
				};
			}
		}
	}
}