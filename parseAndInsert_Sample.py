import json
import psycopg2

password = ""

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2ReviewTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_review.json','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the review
            # TODO: The below INSERT statement is based on a simple (and incomplete) review schema. Update the statement based on your own table schema and
            # include values for all review attributes
            sql_str = "INSERT INTO review (review_id, uid, bid, review_stars, date, text, useful_vote, funny_vote, cool_vote) " \
                      "VALUES ('" + cleanStr4SQL(data['review_id']) + "','" + cleanStr4SQL(data["user_id"]) + "','" + cleanStr4SQL(data["business_id"]) + "','" + \
                      str(data["stars"]) + "','" + cleanStr4SQL(data["date"]) + "','" + cleanStr4SQL(data["text"]) + "'," + str(data["useful"]) + "," + \
                      str(data["funny"]) + "," + str(data["cool"]) + ");"
            try:
                cur.execute(sql_str)
            except:
                print(sql_str)
                #print("Insert to Review failed!")
                
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()    

def insert2UserTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_user.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)        
            # user_id, name, yelping_since is + cleanStr4SQL(data[""]) + "','"
            # everything else is + str(data[""]) + ","            
            sql_str = "INSERT INTO user1 (USERID, average_stars, cool, funny, useful, fans, uname, review_count, user_latitude, user_longitude, yelping_since) " \
                      "VALUES ('" + cleanStr4SQL(data['user_id']) + "'," + str(data['average_stars']) + "," + str(data['compliment_cool']) + "," + \
                    str(data['compliment_funny']) + "," + str(data['useful']) + "," + str(data['fans']) + ",'" +  \
                      cleanStr4SQL(data['name']) + "'," + str(data['review_count']) + "," + str(0) + "," + str(0) + ",'" + cleanStr4SQL(data['yelping_since']) + "');"
            try:
                cur.execute(sql_str)
            except:
                print(sql_str)
                print()
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()  

def insert2FriendTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_user.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)    
            for friends in data["friends"] :
                # user_id, name, yelping_since is + cleanStr4SQL(data[""]) + "','"
                # everything else is + str(data[""]) + ","            
                sql_str = "INSERT INTO friends (friendid, userid) " \
                          "VALUES ('" + cleanStr4SQL(friends) + "','" + cleanStr4SQL(data['user_id']) + "');"
                try:
                    cur.execute(sql_str)
                except:
                    print(sql_str)
                    print()
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()

        
def insert2BusinessTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)        
            # user_id, name, yelping_since is + cleanStr4SQL(data[""]) + "','"
            # everything else is + str(data[""]) + ","            
            sql_str = "INSERT INTO business (business_id, bname, address, city, bstate, zipcode, latitude, longitude, stars, review_count, is_open, review_rating, num_checkins) " \
                      "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(data['name']) + "','" + cleanStr4SQL(data['address']) + "','" \
                                    + cleanStr4SQL(data['city']) + "','" + cleanStr4SQL(data['state']) + "','" + cleanStr4SQL(data['postal_code']) + "','" + str(data['latitude']) + "','" + str(data['longitude']) + "'," \
                                    + str(data['stars']) + "," + str(data['review_count']) + "," + str(data['is_open']) + "," + str(0) + "," + str(0) + ");"
            try:
                cur.execute(sql_str)
            except:
                print(sql_str)
                print()
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()  

    
def insert2BusinessHourTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)        
            # user_id, name, yelping_since is + cleanStr4SQL(data[""]) + "','"
            # everything else is + str(data[""]) + ","  
            businessHour = data['hours']
            if bool(businessHour) : 
                for day in businessHour : 
                    hours = data["hours"][day]
                    indexOfDash = hours.find('-')
                    openTime = hours[:indexOfDash]
                    indexOfDash += 1
                    closeTime = hours[indexOfDash:]
                    sql_str = "INSERT INTO Hour (Bid, Day, close, open) VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(day) +\
                    "','" + cleanStr4SQL(closeTime) + "','" + cleanStr4SQL(openTime) + "');"

                    try:
                        cur.execute(sql_str)
                    except:
                        print(sql_str)
                        print()
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()  

def insert2CheckInTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_checkin.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)        
            bid = data["business_id"]
            #print(data)
            for day in data["time"] :
                #print(day)
                if isinstance(data["time"][day], dict) :
                    #pass
                    for hour in data["time"][day] :
                        #print(bid)
                        #print(day)
                        #print(hour)
                        #print(data["time"][day][hour])
                        sql_str = "INSERT INTO checkin (Bid, Day, Hour, count) VALUES ('" + cleanStr4SQL(bid) + "','" + \
                        cleanStr4SQL(day) + "','" + cleanStr4SQL(hour) + "'," + str(data["time"][day][hour]) + ");"
                        #print(sql_str)
                        try:
                            cur.execute(sql_str)
                        except:
                            #pass
                            #return
                            print(sql_str)           
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()  

def insert2BusinessCategoryTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)        
            bid = data["business_id"]
            for cat in data["categories"] :
                sql_str = "INSERT INTO businesscategory (Bid, categoryName) VALUES ('" + cleanStr4SQL(bid) + "','" + cleanStr4SQL(cat) + "');"
                try:
                    cur.execute(sql_str)
                except:
                    print(sql_str)           
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()  

def insert2BusinessAttributesTable():
    #reading the JSON file
    with open('Yelp-CptS451-2019/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='project_final' user='postgres' host='localhost' password=" + password)
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)        
            # user_id, name, yelping_since is + cleanStr4SQL(data[""]) + "','"
            # everything else is + str(data[""]) + ","            
            #str([item for item in data['categories']])
            for att in data['attributes']:
                if isinstance(data['attributes'][att], dict) :
                    #pass
                    for att2 in data['attributes'][att]:
                        #print(att2)
                        #print(str(data['attributes'][att][att2]))
                        sql_str = "INSERT INTO businessattributes (bid, attributename, attributevalue) " \
                         "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(att2) + "','" + cleanStr4SQL(str(data['attributes'][att][att2])) + "');"
                        try: 
                             cur.execute(sql_str)
                        except:
                            print(sql_str)
                            print()
                        
                else :
                    #print(att)
                    #print(str(data['attributes'][att]))
                    sql_str = "INSERT INTO businessattributes (bid, attributename, attributevalue) " \
                         "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(att) + "','" + str(data['attributes'][att]) + "');"
                    try:
                        cur.execute(sql_str)
                    except:
                        print(sql_str)
                        print()
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()
    
#insert2UserTable()
#insert2BusinessTable()
#insert2ReviewTable()
#insert2BusinessAttributesTable()
#insert2BusinessCategoryTable()
insert2CheckInTable()
#insert2BusinessHourTable()
#insert2FriendTable()

