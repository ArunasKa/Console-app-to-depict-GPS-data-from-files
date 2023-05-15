using GpsDataRepresentation.Models;

namespace GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces
{
    public interface IDataRepresentationService
    {
        List<GpsData> ReadJSON();
        List<GpsData> ReadCsv();
        List<GpsData> ReadBin();
        void MakeSateliteHistogram(List<int> SateliteList);
        void MakeSpeedHistogram(List<int> SpeedList);
        List<int> SortOutSatelites(List<GpsData> GpsData);
        List<int> SortOutSpeed(List<GpsData> GpsData);
        void GetRoadSection(List<GpsData> fileJson, List<GpsData> fileCsv, List<GpsData> fileBin);
    }
}