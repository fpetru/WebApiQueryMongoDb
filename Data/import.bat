mongoimport --db TravelDb ^
            --collection WikiVoyage ^
            --type tsv ^
            --fieldFile enwikivoyage-fields.txt^
            --file enwikivoyage-20150901-listings.result.tsv^
            --columnsHaveTypes^
            --username admin ^
            --password abc123! ^
            --authenticationDatabase admin ^
            --numInsertionWorkers 4

mongoimport --db TravelDb ^
            --collection Cities ^
            --type tsv ^
            --fieldFile cities5000-fields.txt^
            --file cities5000.txt ^
            --columnsHaveTypes^
            --username admin ^
            --password abc123! ^
            --authenticationDatabase admin ^
            --numInsertionWorkers 4

mongo localhost:27017/admin -u admin -p abc123! createIndex.js

pause
