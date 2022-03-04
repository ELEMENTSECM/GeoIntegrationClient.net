#define TEST_ALL_AVAILABLE_CODE_LISTS //Undefine this if only one code-list should be tested to optimize test-execution

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference;
using System;
using System.Threading.Tasks;


namespace GeoIntegrationClient.GiArkivInnsyn
{
	[TestClass]
	public class FinnJournalposterTest
	{
		[TestMethod]
		[DataRow()]
		public async Task HentSak()
		{
			var sok = new Soekskriterie[] 
			{ 
				new Soekskriterie
				{ 
					Kriterie = new Soekefelt { feltnavn = "Journalpost.tittel", feltverdi = "Eiendomsskatt" },
					@operator = SoekeOperatorEnum.EQ
				} 
			};
			var result = await FinnJournalposterAsync(sok);
			Console.WriteLine(result);
		}

		private async Task<FinnJournalposterResponse> FinnJournalposterAsync(Soekskriterie[] sok, bool returnerMerknad = false, bool returnerTilleggsinformasjon = false, bool returnerKorrespondansepart = false, bool returnerAvskrivning = false)
		{
			using (var client = ArkivInnsynClientFactory.CreateArkivInnsynPortClient())
			{
				var kontekst = new ArkivKontekst { klientnavn = Configuration.ClientName };
				return await client.FinnJournalposterAsync(sok, returnerMerknad, returnerTilleggsinformasjon, returnerKorrespondansepart, returnerAvskrivning, kontekst);
			}
		}

	}

}
