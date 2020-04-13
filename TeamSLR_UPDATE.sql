
-- Update function for incrementing reviewCount and updating average rating for calculatedInfo table
create or replace function updateReviewCount_AvgRating()
returns trigger as '
begin
update calculatedInfo
set reviewCount = reviewCount + 1, avgrating = ((avgrating * (reviewCount - 1)) + new.review_stars) / reviewCount
where calculatedInfo.bid = calculatedInfo.bid;
return new;
end '
language plpgsql

-- Update function for incrementing review_count and review_rating for business table
create or replace function business_updateReviewCount_Rating()
returns trigger as '
begin
update business
set review_count = review_count + 1, review_rating = ((review_rating * (review_count - 1)) + new.review_stars) / review_count
where business.business_id = business.business_id;
return new;
end '
language plpgsql
