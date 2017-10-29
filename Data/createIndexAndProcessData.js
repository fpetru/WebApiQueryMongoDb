db = db.getSiblingDB('TravelDb');

// add indexes
db.WikiVoyage.createIndex( { City: 1, Action: 1 } );
// db.Countries.createIndex( { "country-name": 1 }, { unique: true } );
db.Countries.createIndex( { "alpha-2": 1 }, { unique: true } );

// create a more rich collection for cities
db.Cities.aggregate([
    {
        // join with Countries collection
        $lookup: {from: "Countries", localField: "CountryCode", foreignField: "alpha-2", as: "Matched"}
    },
    {
      // fields you want to fetch 
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
       $out : "CitiesNew" 
   }      
]);



