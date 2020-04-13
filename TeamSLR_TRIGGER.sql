
create trigger updateReviewCount_AvgRating
after insert on review
for each row
when (new.bid is not null)
execute procedure updateReviewCount_AvgRating()

create trigger business_updateReviewCount_Rating
after insert on review
for each row
when (new.bid is not null)
execute procedure business_updateReviewCount_Rating()

--Before there was nine entries
INSERT INTO review (review_id, uid, bid, review_stars, date, text, useful_vote, funny_vote, cool_vote)
VALUES ('fhfhhf', 'om5ZiponkpRqUNa3pVPiRg', '--ab39IjZR_xUf81WyTyHg', 4, 2019-03-19, 'Some review text', 1, 1, 1)

--ten entries for the given bid now!
select * from calculatedInfo order by bid
