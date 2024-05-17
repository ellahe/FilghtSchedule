Description
You are creating an application, to be used by airline agencies, for detecting cases of airlines introducing new or discontinuing existing regular flight schedules. For example, Lufthansa is offering a flight from Berlin to Vienna every Monday at 7:30. In the case that Lufthansa decides to stop offering that flight, we need to be able to detect that, because that opens a business opportunity for flight agencies to offer alternative flights. The same applies to newly introduced flight schedules.

Change detection algorithm
New flights
A flight with departure time T is considered to be a new flight if no corresponding flight from same airline exists with departure time T’ = T - 7 days (+/- 30 minutes tolerance).

Discontinued flights
A flight with departure time T is considered to be discontinued if no corresponding flight from same airline exists with departure time T’ = T + 7 days (+/- 30 minutes tolerance).
