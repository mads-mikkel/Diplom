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
                csvText = csvText.Remove(0, csvText.IndexOf('\n') + 1);
            }
            int[] keepColumns = request.KeepColumns;
            string filteredCsv = String.Empty;

            await Task.Run(() =>
            {
                string[] lines = csvText.Split('\n');
                int lineCount = lines.Length;
                for(int lineIndex = 0; lineIndex < lineCount; lineIndex++)
                {
                    string[] filteredLineValues = new string[keepColumns.Length];
                    string[] lineValues = lines[lineIndex].Split(',');
                    int lineValuesCount = lineValues.Length;
                    for(int lineValueIndex = 0; lineValueIndex < lineValuesCount; lineValueIndex++)
                    {
                        int keepColumnsCount = keepColumns.Length;
                        for(int keepColumnIndex = 0; keepColumnIndex < keepColumnsCount; keepColumnIndex++)
                        {
                            int keepColumnValue = keepColumns[keepColumnIndex];
                            if(keepColumnValue == lineValueIndex)
                            {
                                filteredLineValues[keepColumnIndex] = lineValues[lineValueIndex];
                            }
                        }
                    }
                    
                    for(int filteredLineIndex = 0; filteredLineIndex < filteredLineValues.Length; filteredLineIndex++)
                    {
                        filteredCsv += filteredLineValues[filteredLineIndex];
                        if(filteredLineValues.Length > 1)
                        {
                            filteredCsv += ",";
                        }
                    }
                    if(lineIndex < lineCount - 2)
                    {
                        filteredCsv += "\n";
                    }
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