CREATE TABLE [dbo].[Flight] (
    [FlightId]      INT           NOT NULL,
    [RouteId]       INT           NULL,
    [DepartureTime] DATETIME2 (7) NOT NULL,
    [ArrivalTime]   DATETIME2 (7) NOT NULL,
    [AirlineId]     INT           NULL,
    [Status]        VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([FlightId] ASC),
    FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Route] ([RouteId])
);


GO
CREATE NONCLUSTERED INDEX [idx_flight_departuretime]
    ON [dbo].[Flight]([DepartureTime] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_flight_arrivaltime]
    ON [dbo].[Flight]([ArrivalTime] ASC);

