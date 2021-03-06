I am traveling next month to the beautiful country of Dagonia, where I am going to rent a car. 
As I have been making my plans, I have learned four interesting facts about the Dagonian highway system:

- The highways lead from city to city; they do not intersect anywhere else.
- Each highway is for travel in one direction only.
- The highway system is acyclic; once you drive away from a city, there is no way to legally drive back.
- Every city charges a toll, payable when you enter the city via a highway.
- There are a number of driving trips I would like to take while in Dagonia, 
  and for each I need to determine whether the trip is possible and, if so, the minimum amount I will need to pay in tolls.

For example, If I want to drive from Sourceville to SinkCity, the minimum toll will be $25 ($15 for entering Weston 
and $10 for entering SinkCity). If I want to drive from Easton to Easton, the minimum toll is $0 (since I am already there). 
If I want to drive from SinkCity to Weston, I�m out of luck.

Given a map of a portion of the Dagonian highway system and a list of trips I would like to take, your job is to tell me, 
for each trip, what the minimum toll would be (if the trip is possible) or �NO� (otherwise).

Input
All numbers in the input are integers.

The first line contains the number of cities n on the map (1<=n<=2000).

The next n lines identify cities. Each line contains a city name (maximum length 20, no embedded white space) 
and that city�s toll (minimum toll 0, maximum toll 10000. The city names are unique.

The next line contains the number of highways h on the map (0<=h<=10000).

The next h lines identify highways. Each line contains two city names (that appear in the list of cities). 
There is a highway from the first city to the second city. All highways are unique.

The next line contains the number of trips t I am interested in taking (1<=t<=8000).

The next t lines identify proposed trips. Each line contains two city names (that appear in the list of cities).

Output
For each proposed trip, if there is no way to drive from the first city to the second city, 
output a line containing �NO�. Otherwise, output a line containing the minimum toll required to make the drive.