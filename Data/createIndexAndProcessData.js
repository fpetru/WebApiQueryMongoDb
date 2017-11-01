db = db.getSiblingDB('TravelDb');

// add indexes
db.WikiVoyage.createIndex( { City: 1, Action: 1 } );
db.Countries.createIndex( { "Name": 1 }, { unique: true } );
db.Countries.createIndex( { "Alpha2": 1 }, { unique: true } );

// create a richer collection for cities
db.Cities.aggregate([
    {
        // join with Countries collection
        $lookup: {from: "Countries", localField: "CountryCode", foreignField: "Alpha2", as: "Matched"}
    },
    {
      // fields to fetch  
      $project: {
        Name: "$AsciiName",  
        AlternateNames: {$split: ["$AlternateNames", ","]},
        Latitude: "$Latitude",
        Longitude: "$Longitude",
        Population: "$Population",
        DigitalElevationModel: "$DigitalElevationModel",
        Country: {$arrayElemAt: [ "$Matched", 0 ]},
        TimeZone: "$Timezone"
      }
    },
    {
      // final fields to retrieve (including also the country fields) 
      $project: {
        Name: 1,  
        AlternateNames: 1,
        Latitude: 1,
        Longitude: 1,
        Population: 1,
        DigitalElevationModel: 1,
        CountryName: "$Country.Name",
        CountryCode: "$Country.Alpha2",     
        Region: "$Country.Region", 
        Subregion: "$Country.Subregion", 
        TimeZone: 1
      }
    },
    { 
        $out : "CitiesExtended" 
    }     
]);

