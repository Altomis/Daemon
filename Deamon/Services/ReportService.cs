using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Deamon.Models;

namespace Deamon.Services
{
    public static class ReportService
    {
        static HttpClient client = new HttpClient();

        public static async Task<Uri> RegisterReportAsync(Report report)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/finishedjobs", report);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public static async Task RunAsync(int clientid, string backuptype, bool error, Exception exception)
        {
            client.BaseAddress = new Uri("http://localhost:49497/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/finishedjobs");

            List<Report> reports = await response.Content.ReadAsAsync<List<Report>>();

            Uri url;

            for (int i = 0; i < reports.Count; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", "ID: " + reports[i].Id, "BackupType: " + reports[i].BackupType, "Error: " + reports[i].IsError, "ErrorMsg: " + reports[i].ErrorMsg);
            }

            Report newReport = new Report
            {
                IdGroups = clientid,
                BackupType = backuptype,
                IsError = error,
                ErrorMsg = Convert.ToString(exception)
            };

            url = await RegisterReportAsync(newReport);
            Console.WriteLine("Report byl úspěšně odeslán");
        }
    }
}