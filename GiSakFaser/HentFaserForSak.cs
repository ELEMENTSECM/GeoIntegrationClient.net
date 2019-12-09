#define TEST_ALL_AVAILABLE_CODE_LISTS //Undefine this if only one code-list should be tested to optimize test-execution

using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using GiSakFaser;


namespace GeoIntegrationClient.GiSakFaser
{
	[TestClass]
	public class HentFaserForSak
	{
		[TestMethod]
		[DataRow("2019", "0")]
		public async Task ValidCase_ShouldNotReturnNull(string saksaar, string sakssekvensnummer)
		{
			try
			{
				var saksnummer = new Saksnummer() { saksaar = saksaar, sakssekvensnummer = sakssekvensnummer };
				var response = await HentFaserForSakAsync(saksnummer);
				Assert.IsNotNull(response);
			}
			catch (EndpointNotFoundException e)
			{
				Console.WriteLine($"EndpointNotFoundException. Message: {e.Message}");
				throw;
			}
		}

		[TestMethod]
		[DataRow("1900", "0")]
		public async Task InValidCase_ShouldNotReturnNull(string saksaar, string sakssekvensnummer)
		{
			try
			{
				var saksnummer = new Saksnummer() {saksaar = saksaar, sakssekvensnummer = sakssekvensnummer};
				var response = await HentFaserForSakAsync(saksnummer);
				Assert.IsNotNull(response);
			}
			catch (FaultException e)
			{
				Console.WriteLine($"FaultException is expected. Message:{e.Message} Reason:{e.Reason}");
			}
			catch (EndpointNotFoundException e)
			{
				Console.WriteLine($"EndpointNotFoundException. Message: {e.Message}");
				throw;
			}
		}

		private static async Task<HentFaserForSakResponse> HentFaserForSakAsync(Saksnummer saksnummer)
		{
			using (var client = SaksfaserClientFactory.CreateClient())
			{
				return await client.HentFaserForSakAsync(saksnummer);
			}
		}
	}

}
