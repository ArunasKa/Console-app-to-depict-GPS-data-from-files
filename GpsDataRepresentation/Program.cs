





using GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces;
using GpsDataRepresentation.GpsDataRepresentation.BL.Services;

IDataRepresentationService dataRepresentationService = new DataRepresentationService();
//JSON
var fileJson = dataRepresentationService.ReadJSON();
var SateliteList = dataRepresentationService.SortOutSatelites(fileJson);
dataRepresentationService.MakeSateliteHistogram(SateliteList);
//CSV
var fileCsv = dataRepresentationService.ReadCsv();
var SpeedList = dataRepresentationService.SortOutSpeed(fileCsv);
dataRepresentationService.MakeSpeedHistogram(SpeedList);

