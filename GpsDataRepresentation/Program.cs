using GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces;
using GpsDataRepresentation.GpsDataRepresentation.BL.Services;

IDataRepresentationService dataRepresentationService = new DataRepresentationService();
//JSON
var fileJson = dataRepresentationService.ReadJSON();
var SateliteListJson = dataRepresentationService.SortOutSatelites(fileJson);
var SpeedListJson = dataRepresentationService.SortOutSpeed(fileJson);
dataRepresentationService.MakeSateliteHistogram(SateliteListJson);
dataRepresentationService.MakeSpeedHistogram(SpeedListJson);
//CSV
var fileCsv = dataRepresentationService.ReadCsv();
var SateliteListJCsv = dataRepresentationService.SortOutSatelites(fileCsv);
var SpeedListCsv = dataRepresentationService.SortOutSpeed(fileCsv);
dataRepresentationService.MakeSateliteHistogram(SateliteListJCsv);
dataRepresentationService.MakeSpeedHistogram(SpeedListCsv);
//Bin

var fileBin = dataRepresentationService.ReadBin();
var SateliteListJBin = dataRepresentationService.SortOutSatelites(fileBin);
var SpeedListBin = dataRepresentationService.SortOutSpeed(fileBin);
dataRepresentationService.MakeSateliteHistogram(SateliteListJBin);
dataRepresentationService.MakeSpeedHistogram(SpeedListBin);


//Road Section
dataRepresentationService.GetRoadSection(fileJson, fileCsv);
