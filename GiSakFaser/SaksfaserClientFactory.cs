using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using GiSakFaser;

namespace GeoIntegrationClient.GiSakFaser
{
	public class SaksfaserClientFactory
	{
		public static SaksfaserPortClient CreateClient()
		{
			var url = Configuration.SakFaserUrl;
			var client = new SaksfaserPortClient
			{
				Endpoint =
				  {
					Address = new EndpointAddress(new Uri(url)),
					Binding = GetBinding(url),
				  }
			};
			client.Endpoint.EndpointBehaviors.Add(new AuthenticationHeaderBehavior(Configuration.UserName, Configuration.Password));
			return client;
		}

		private static Binding GetBinding(string url)
		{
			var binding = new BasicHttpBinding
			{
				Namespace = "http://rep.geointegrasjon.no/Sak/Faser/xml.wsdl/2012.01.31"
			};

			bool https = url.StartsWith("https", StringComparison.OrdinalIgnoreCase);
			if (https)
			{
				binding.Security.Mode = BasicHttpSecurityMode.Transport;
			}

			return binding;
		}
	}
}