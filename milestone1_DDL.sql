
CREATE TABLE User1 (
  USERID char(22) PRIMARY KEY, 
  average_stars FLOAT,
  cool integer,
  funny integer,
  useful integer,
  fans integer,
  uname varchar(250) NOT NULL,
  review_count integer DEFAULT 0,
  user_latitude FLOAT,
  user_longitude FLOAT,
  yelping_since char(10)
);

CREATE TABLE friends (
    FriendID char(22), 
    USERID char(22),
    FOREIGN KEY (FriendID) REFERENCES User1(USERID),
    PRIMARY KEY (FriendID, USERID)
);

CREATE TABLE business (
  business_id char(22) Primary Key,
  bname	varchar(250) NOT NULL,
  address varchar(250) NOT NULL,
  city	varchar(250) NOT NULL,
  bstate   char(2) NOT NULL,
  zipcode varchar(5) NOT NULL,
  latitude float,
  longitude float,
  stars integer,
  review_count integer,
  is_open integer,
  review_rating FLOAT,
  num_checkins integer
);

CREATE TABLE review (
  review_id char(22) primary key,
  uid char(22),
  bid char(22),
  review_stars integer,
  date char(10),
  text varchar(10000),
  useful_vote integer,
  funny_vote integer,
  cool_vote integer,
  FOREIGN KEY (uid) REFERENCES User1(USERID),
  FOREIGN KEY (bid) REFERENCES business(business_id)
);


CREATE TABLE BusinessAttributes (
  Bid CHAR(22) ,
  attributeName VARCHAR(250),
  attributeValue VARCHAR(250),
  primary key (attributeName, Bid),
  FOREIGN KEY (Bid) REFERENCES business(business_id)
);

CREATE TABLE BusinessCategory (
  Bid CHAR(22),
  categoryName VARCHAR(250),
  primary key (categoryName, Bid),
  FOREIGN KEY (Bid) REFERENCES business(business_id)
);

CREATE TABLE Checkin (
  Bid CHAR(22),
  Day VARCHAR(20),
  Hour VARCHAR(20),
  count integer,
  primary key (Bid, Day, Hour),
  FOREIGN KEY (Bid) REFERENCES business(business_id)
);

CREATE TABLE Hour (
Bid CHAR(22),
Day VARCHAR(20),
close VARCHAR(5),
open VARCHAR(5),
primary key (Day, Bid),
FOREIGN KEY (Bid) REFERENCES business(business_id)
);




