#Creating the calculatedInfo Table
create table calculatedInfo (
  bid char(22),
  numCheckins integer,
  reviewCount integer,
  avgRating float
);

#Puting numcheckins to temp1
select sum(checkin.count) as numCheckin, bid
into temp1
from checkin
where bid=bid
group by bid
order by bid

#Puting numRevies, avgReviewRating to temp2
select count(review.review_id) as numReviews, avg(review.review_stars) as avgReviewRating, bid
into temp2
from review where bid=bid
group by bid
order by bid

#Puting all the information to calculatedInfo Table
insert into calculatedInfo (bid, numcheckins, reviewcount, avgrating)
select temp1.bid, temp1.numCheckin, temp2.numReviews, temp2.avgReviewRating
from temp1, temp2
where temp1.bid=temp2.bid

#Deleting temporary tables
drop table temp1
drop table temp2

--Makes the business table have the rating that is calculated
update business
set review_rating = calculatedInfo.avgrating
from calculatedInfo
where calculatedInfo.bid = business.business_id
