

CREATE TABLE Route (
    Id int   IDENTITY (1,1),
    RouteId INT PRIMARY KEY,
    OriginCityId INT NULL,
    DestinationCityId INT NULL,
    Departure_date DATETIME2 NOT NULL,
);


create TABLE Flight (
    FlightId INT PRIMARY KEY,
    RouteId INT FOREIGN KEY REFERENCES Route(RouteId),
    DepartureTime DATETIME2 NOT NULL,
    ArrivalTime DATETIME2 NOT NULL,
    AirlineId INT ,
    Status VARCHAR(50)  NULL
);


CREATE TABLE Subscription (
    Id  INT IDENTITY(1,1),
    AgencyId INT ,
    OriginCityId INT ,
    DestinationCityId INT 
);

CREATE INDEX idx_flight_departuretime ON Flight(DepartureTime);
CREATE INDEX idx_flight_arrivaltime ON Flight(ArrivalTime);
CREATE INDEX idx_subscription_agencyid ON Subscription(AgencyId);
CREATE INDEX idx_subscription_originCityId ON Subscription(DestinationCityId);
CREATE INDEX idx_subscription_destinationCityId ON Subscription(DestinationCityId);

