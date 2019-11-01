#define TEST_ALL_AVAILABLE_CODE_LISTS //Undefine this if only one code-list should be tested to optimize test-execution

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference;
using System.Threading.Tasks;


namespace GeoIntegrationClient
{
	[TestClass]
	public class HentKodelisteTest
	{
		[TestMethod]
		[DataRow("Variantformat")]
#if TEST_ALL_AVAILABLE_CODE_LISTS
		[DataRow("TilknyttetRegistreringSom")]
		[DataRow("Format")]
		[DataRow("Dokumentstatus")]
		[DataRow("Dokumenttype")]
		//[DataRow("KoordinatsystemKode")]
		[DataRow("Tilgangsrestriksjon")]
		[DataRow("SkjermingsHjemmel")]
		[DataRow("SkjermingOpphorerAksjon")]
		[DataRow("Saksstatus")]
		[DataRow("Mappetype")]
		[DataRow("Korrespondanseparttype")]
		[DataRow("Klassifikasjonssystem")]
		[DataRow("Kassasjonsvedtak")]
		[DataRow("Journalstatus")]
		[DataRow("Journalposttype")]
		[DataRow("Journalenhet")]
		[DataRow("Informasjonstype")]
		[DataRow("Forsendelsesmaate")]
		[DataRow("Dokumentmedium")]
		[DataRow("Avskrivningsmaate")]
		[DataRow("Arkivdel")]
		[DataRow("SakspartRolle")]
		[DataRow("PersonidentifikatorType")]
		//[DataRow("Landkode")]
		[DataRow("EnkelAdressetype")]
#endif
		public async Task HentKodeliste(string kodelistenavn)
		{
			Assert.IsNotNull(await HentKodelisteAsync(kodelistenavn));
		}

		private async Task<HentKodelisteResponse> HentKodelisteAsync(string kodelistenavn)
		{
			using (var client = ArkivInnsynClientFactory.CreateArkivInnsynPortClient())
			{
				var kontekst = new ArkivKontekst { klientnavn = Configuration.ClientName };
				return await client.HentKodelisteAsync(kodelistenavn, kontekst);
			}
		}
	}

}
