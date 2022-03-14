using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

#if NET6_0_OR_GREATER
using System.Text.Json;
#endif

namespace GeoIntegrationClient.GiArkivInnsyn
{
	[TestClass]
	public class FinnSaksmappeTest
	{
		[TestMethod]
		[DataRow("2022", "10", "")]
		[DataRow("", "", "Eiendomsskatt Husebyveien")]
		public async Task HentSaksmappe(string year, string sequenceNumber, string title)
		{
			var criterias = new List<Soekskriterie>();
			if(!string.IsNullOrEmpty(year))
				criterias.Add(GetAndCriteria("Saksmappe.saksnr.saksaar", year));

			if (!string.IsNullOrEmpty(sequenceNumber))
				criterias.Add(GetAndCriteria("Saksmappe.saksnr.sakssekvensnummer", sequenceNumber));

			if (!string.IsNullOrEmpty(title))
				criterias.Add(GetAndCriteria("Saksmappe.tittel", title));

			var response = await FinnSaksmapperAsync(criterias.ToArray());
			Assert.IsNotNull(response, "Response should not be null");
			Assert.IsNotNull(response.@return, "Response.return should not be null");

#if NET6_0_OR_GREATER
			foreach(var item in response.@return)
			{
				Console.WriteLine(JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true}));
			}
#endif
		}

		static Soekskriterie GetAndCriteria(string field, string value)
		{
			return new Soekskriterie
			{
				@operator = SoekeOperatorEnum.EQ,
				Kriterie = new Soekefelt
				{
					feltnavn = field,
					feltverdi = value
				}
			};
		}


		private async Task<FinnSaksmapperResponse> FinnSaksmapperAsync(Soekskriterie[] sok, bool returnerMerknad = false, bool returnerTilleggsinformasjon = false, bool returnerSakspart = false, bool returnerKlasse = false)
		{
			using (var client = ArkivInnsynClientFactory.CreateArkivInnsynPortClient())
			{
				var kontekst = new ArkivKontekst { klientnavn = Configuration.ClientName };
				return await client.FinnSaksmapperAsync(sok, returnerMerknad, returnerTilleggsinformasjon, returnerSakspart, returnerKlasse, kontekst);
			}
		}

	}

}
