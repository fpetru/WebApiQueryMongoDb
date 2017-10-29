db = db.getSiblingDB('TravelDb');

// drop database & all users
db.dropDatabase();
db.runCommand( { dropAllUsersFromDatabase: 1 } );