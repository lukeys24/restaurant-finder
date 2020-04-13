
create or replace function updateCheckinCount()
returns trigger as '
begin
update business
set num_checkins = num_checkins + 1
where business.business_id = business.business_id;
return new;
end '
language plpgsql

create trigger updateCheckinCount
after insert on checkin
for each row
when (new.bid is not null)
execute procedure updateCheckinCount()

INSERT INTO checkin (Bid, Time, Day, count) VALUES ('duHFBe87uNSXImQmvBh87Q','17:30','Friday','3')

select * from business where business_id = 'duHFBe87uNSXImQmvBh87Q'

