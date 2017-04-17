mongoimport -d TravelDb -c WikiVoyage --type tsv --file enwikivoyage-20150901-listings.result.tsv --headerline -u admin -p abc123! --authenticationDatabase admin --numInsertionWorkers 4
mongoimport -d TravelDb -c Cities --type tsv --file cities5000.txt --headerline -u admin -p abc123! --authenticationDatabase admin --numInsertionWorkers 4
pause
