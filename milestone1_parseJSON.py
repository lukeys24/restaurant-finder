import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def parseBusinessData():
    #read the JSON file
    with open('yelp-CptS451-2019/yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business id
            outfile.write(cleanStr4SQL(data['name'])+'\t') #name
            outfile.write(cleanStr4SQL(data['address'])+'\t') #full_address
            outfile.write(cleanStr4SQL(data['state'])+'\t') #state
            outfile.write(cleanStr4SQL(data['city'])+'\t') #city
            outfile.write(cleanStr4SQL(data['postal_code']) + '\t')  #zipcode
            outfile.write(str(data['latitude'])+'\t') #latitude
            outfile.write(str(data['longitude'])+'\t') #longitude
            outfile.write(str(data['stars'])+'\t') #stars
            outfile.write(str(data['review_count'])+'\t') #reviewcount
            outfile.write(str(data['is_open'])+'\t') #openstatus
            outfile.write(str([item for item in  data['categories']])+'\t') #category list
            outfile.write(str([])) # write your own code to process attributes
            outfile.write(str([])) # write your own code to process hours
            outfile.write('\n');

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

def parseUserData():
    with open('yelp-CptS451-2019/yelp_user.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('user.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        outfile.write("user_id, name, yelping_since, review_count, fans, average_star, funny, useful, cool, friends\n")
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['user_id'])+',\t') #business id
            outfile.write(cleanStr4SQL(data['name'])+',\t') #name
            outfile.write(cleanStr4SQL(data['yelping_since'])+',\t') #full_address
            outfile.write(str(data['review_count'])+',\t') #state
            outfile.write(str(data['fans'])+',\t') #city
            outfile.write(str(data['average_stars']) + ',\t')  #zipcode
            outfile.write(str(data['funny'])+',\t') #latitude
            outfile.write(str(data['useful'])+',\t') #longitude
            outfile.write(str(data['cool'])+',\t') #stars
            outfile.write(str([friends for friends in  data['friends']])+'\t') #category list
            outfile.write('\n');

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()    

def parseCheckinData():
    # read the JSON file
    with open('yelp-CptS451-2019/yelp_checkin.JSON',
              'r') as f:  # Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile = open('checkin.txt', 'w')
        outfile.write(
            "business_id, dayofweek, timeofday, numberofchecksin\n")
        line = f.readline()
        count_line = 0
        # read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id']) + '\n')  # business id
            for item in data['time']:
                outfile.write(cleanStr4SQL("\t" + item + "\n"))
                for time, checkins in data['time'][item].items():
                    outfile.write("\n\t\t" + time + ", " + str(checkins))
                outfile.write("\n")

            outfile.write('\n');

            line = f.readline()
            count_line += 1
    print(count_line)
    outfile.close()
    f.close()


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

parseBusinessData()
parseUserData()
parseCheckinData()
parseReviewData()
