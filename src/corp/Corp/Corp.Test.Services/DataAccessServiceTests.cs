using Corp.Services.DataContracts;
using System;
using Xunit;

namespace Corp.Test.Services
{
    public class DataAccessServiceTests
    {
        [Fact]
        public void CanDownloadData()
        {
            // Arrange:
            string url = "https://kystatlas.kyst.dk/public2/data/vandstand/response.aspx?ident=20201&startdate=20180825&enddate=20180826&format=csv";
            Uri uri = new(url);
            DownloadDataRequest request = new() { Uri = uri };

            // Act:
            

            // Assert:
        }
    }
}
