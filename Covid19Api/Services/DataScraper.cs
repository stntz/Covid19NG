using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Covid19NG.Models;
using HtmlAgilityPack;

namespace Covid19NG.Services
{
    public class DataScraper
    {
        public string URL { get; private set; }
        public DataScraper()
        {
            URL = "https://covid19.ncdc.gov.ng/report/";
        }


        public async Task<List<CovidData>> ScrapeData()
        {
            using var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(URL);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(result);

            var stateHtml = htmlDoc.DocumentNode.Descendants("tbody").ToList();

            var stateList = stateHtml[0].Descendants("tr").ToList();

            var listState = new List<CovidData>();

            if(stateList != null && stateList.Any())
            {
                foreach(var state in stateList)
                {
                    
                    var covData = new CovidData
                    {
                        State = state.SelectSingleNode("td[1]").InnerHtml?.Trim(),
                        AdmittedCases = state.SelectSingleNode("td[3]").InnerHtml?.Trim(),
                        ConfirmedCases = state.SelectSingleNode("td[2]").InnerHtml?.Trim(),
                        DeathNumber = state.SelectSingleNode("td[5]").InnerHtml?.Trim(),
                        DischargedNumber = state.SelectSingleNode("td[4]").InnerHtml?.Trim()
                    };
                    listState.Add(covData);
                }
            }

            return listState;
        }
    }
}