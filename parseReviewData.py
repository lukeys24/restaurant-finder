def parseReviewData():
  #read the JSON file
    with open('yelp-CptS451-2019/yelp_review.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('review.txt', 'w')
        line = f.readline()
        count_line = 0
        outfile.write('review_id' + ', ')
        outfile.write('user_id' + ', ')
        outfile.write('business_id' + ', ')
        outfile.write('stars' + ', ')
        outfile.write('date' + ', ')
        outfile.write('text' + ', ')
        outfile.write('useful' + ', ')
        outfile.write('funny' + ', ')
        outfile.write('cool' + ',' + '\n')
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['review_id'])+ ', ') 
            outfile.write(cleanStr4SQL(data['user_id'])+ ', ') 
            outfile.write(cleanStr4SQL(data['business_id'])+ ', ') 
            outfile.write(str(data['stars']))
            outfile.write(', ')
            outfile.write(cleanStr4SQL(data['date'])+ ', ' ) 
            outfile.write(cleanStr4SQL(data['text'])+ ', ' ) 
            outfile.write(str(data['useful'])) 
            outfile.write(', ')
            outfile.write(str(data['funny']))
            outfile.write(', ')
            outfile.write(str(data['cool']))
            outfile.write(', ')
            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()
