db = db.getSiblingDB('TravelDb');
db.WikiVoyage.createIndex( { City: 1, Action: 1 } );