using Corp.Services.DataContracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Corp.Resources.Infrastructure.Endpoints.Services;
using System.Linq;

namespace Corp.Services.Contracts
{
    [ServiceContract]
    public interface ITextFilterService
    {
        [OperationContract]
        Task<string> FilterCsvColumns(CsvFilterRequest request);

        //[OperationContract]
        //Task<string> FilterCsvColumns(byte[] bytes, int[] keepColumns);
    }

    public class TextFilterService: ITextFilterService
    {
        public async virtual Task<string> FilterCsvColumns(CsvFilterRequest request)
        {
            string csvText = request.Csv;
            if(request.RemoveHeader)
            {
                csvText = csvText.Remove(0, csvText.IndexOf('\n'));
            }
            int[] keepColumns = request.KeepColumns;
            string filteredCsv = String.Empty;

            await Task.Run(() =>
            {
                string[] lines = csvText.Split('\n');
                int i = lines[0] == String.Empty ? 1 : 0;
                int n = lines[lines.Length - 1] == String.Empty ? lines.Length - 1 : lines.Length;
                while(i < n)
                {
                    string[] filteredLineValues = new string[keepColumns.Length];
                    string line = lines[i];
                    string[] lineValues = line.Split(',');
                    int l = 0;
                    int k = 0;
                    for(int j = 0; j < lineValues.Length; j++)
                    {
                        while(k < keepColumns.Length)
                        {
                            if(j == k)
                            {
                                filteredLineValues[l] = lineValues[j];
                                break;
                            }
                            k++; l++;
                        }
                        
                        //if(j == keepColumns[j])
                        //{
                        //    filteredLineValues[l] = lineValues[j];
                        //    l++;
                        //}
                    }
                    for(int j = 0; j < filteredLineValues.Length - 1; j++)
                    {
                        filteredCsv += String.Concat(filteredLineValues[j], ",");
                    }
                    if(i < lines.Length - 2)
                    {
                        filteredCsv += "\n";
                    }
                    i++;
                }
            });
            return filteredCsv;
        }

        //public async virtual Task<string> FilterCsvColumns(byte[] bytes, int[] keepColumns)
        //{
        //    string s = Encoding.Default.GetString(bytes);
        //    return await FilterCsvColumns(s, keepColumns);
        //}
    }
}